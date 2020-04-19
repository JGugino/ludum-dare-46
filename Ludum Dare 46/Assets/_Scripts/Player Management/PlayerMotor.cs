using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement.GUIInterfacing;

namespace PlayerManagement
{
    public class PlayerMotor : MonoBehaviour
    {

        public bool isJumping { get; private set; } = false;

        public int remainingJumps { get; private set; }

        public int maxJumps { get; private set; } = 2;

        public float playerWalkSpeed { get; private set; } = 2.8f;

        public float playerJumpForce { get; private set; } = 7.5f;

        public float warpDashForce { get; private set; } = 4.5f;

        private float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;

        private int warpDashCost = 15, warpJumpCost = 25;

        public bool canWarpDash = true, isWarpDashing = false, canWarpJump = true, isWarpJumping = false;

        public PlayerDirection playerDirection = PlayerDirection.IDLE;

        public Rigidbody2D pRigidbody2D { get; private set; }

        private PlayerController pController;

        private float canWarpDashCurrentDelay, canWarpDashMaxDelay = 2.5f;

        private float canWarpJumpCurrentDelay, canWarpJumpMaxDelay = 3.5f;

        private float stopWarpingCurrentDelay, stopWarpingMaxDelay = 0.5f;

        void Start()
        {
            remainingJumps = maxJumps;

            canWarpDashCurrentDelay = canWarpDashMaxDelay;
            canWarpJumpCurrentDelay = canWarpJumpMaxDelay;
            
            stopWarpingCurrentDelay = stopWarpingMaxDelay;

            pRigidbody2D = GetComponent<Rigidbody2D>();
            pController = GetComponent<PlayerController>();
        }

        void Update()
        {
            if (isWarpDashing)
            {
                if (stopWarpingCurrentDelay <= 0)
                {
                    isWarpDashing = false;
                    stopWarpingCurrentDelay = stopWarpingMaxDelay;
                    return;
                }
                else
                {
                    stopWarpingCurrentDelay -= Time.deltaTime;
                    return;
                }
            }

            if (!canWarpDash)
            {
                if (canWarpDashCurrentDelay <= 0)
                {
                    canWarpDash = true;
                    pController.playerAnimator.ResetTrigger("isWarpDashing");
                    canWarpDashCurrentDelay = canWarpDashMaxDelay;
                    return;
                }
                else
                {
                    canWarpDashCurrentDelay -= Time.deltaTime;
                    return;
                }
            }

            if (!canWarpJump)
            {
                if (canWarpJumpCurrentDelay <= 0)
                {
                    canWarpJump = true;
                    canWarpJumpCurrentDelay = canWarpJumpMaxDelay;
                    GUIControls.instance.changeBeingWarpJumpUI(BeingWarpJumpState.OPEN);
                    return;
                }
                else
                {
                    canWarpJumpCurrentDelay -= Time.deltaTime;
                    return;
                }
            }
        }

        void FixedUpdate()
        {
            //Makes player fall faster for better jumping
            if (pRigidbody2D.velocity.y < 0)
            {
                pRigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if(pRigidbody2D.velocity.y > 0 && !Input.GetButtonDown("Jump"))
            {
                pRigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        //Player Horizontal Walking Movement
        public void PlayerMove(float _horInput)
        {
            if (_horInput < 0)
            {
                transform.position = new Vector3(transform.position.x - playerWalkSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                playerDirection = PlayerDirection.LEFT;
                pController.playerAnimator.SetBool("isWalking", true);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (_horInput > 0)
            {
                transform.position = new Vector3(transform.position.x + playerWalkSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                playerDirection = PlayerDirection.RIGHT;
                pController.playerAnimator.SetBool("isWalking", true);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                playerDirection = PlayerDirection.IDLE;
                pController.playerAnimator.SetBool("isWalking", false) ;
            }
        }

        public void WarpDash(float _horInput)
        {
            if (canWarpDash)
            {
                if (_horInput < 0)
                {
                    //Left
                    if (pController.takeBeingCharge(warpDashCost))
                    {
                        isWarpDashing = true;
                        pRigidbody2D.AddForce(Vector2.left * warpDashForce, ForceMode2D.Impulse);
                        pController.beingDecayedCharge -= 1;
                        canWarpDash = false;
                        GUIControls.instance.changeBeingUI(pController.beingCurrentCharge, pController.beingMaxCharge);
                        pController.playerAnimator.SetTrigger("isWarpDashing");
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (_horInput > 0)
                {
                    //Right
                    if (pController.takeBeingCharge(warpDashCost))
                    {
                        isWarpDashing = true;
                        pRigidbody2D.AddForce(Vector2.right * warpDashForce, ForceMode2D.Impulse);
                        pController.beingDecayedCharge -= 1;
                        canWarpDash = false;
                        GUIControls.instance.changeBeingUI(pController.beingCurrentCharge, pController.beingMaxCharge);
                        pController.playerAnimator.SetTrigger("isWarpDashing");
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public void teleportPlayerToPosition(Vector2 _position)
        {
            if (pController.takeBeingCharge(warpJumpCost))
            {
                canWarpJump = false;
                isWarpJumping = true;
                pController.playerSpriteRenderer.gameObject.SetActive(false);
                transform.position = _position;
                pController.playerSpriteRenderer.gameObject.SetActive(true);
                isWarpJumping = false;
                pController.beingDecayedCharge -= 2;
                pController.deactivatePortal();
                GUIControls.instance.changeBeingUI(pController.beingCurrentCharge, pController.beingMaxCharge);
                pController.playerState = PlayerStates.NORMAL;
            }
        }

        //Player Jump
        public void PlayerJump()
        {
            if (remainingJumps > 0)
            {
                isJumping = true;
                pRigidbody2D.velocity = Vector2.up * playerJumpForce;
                pController.playerAnimator.SetTrigger("isJumping");
                remainingJumps--;
            }
        }

        //Resets players remaining jumps and set isJumping equal to false
        public void ResetJumps()
        {
            remainingJumps = maxJumps;
            isJumping = false;
        }
    }

    public enum PlayerDirection
    {
        IDLE,LEFT,RIGHT
    }
}
