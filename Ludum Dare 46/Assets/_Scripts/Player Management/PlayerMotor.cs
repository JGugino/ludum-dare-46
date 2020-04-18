using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerManagement
{
    public class PlayerMotor : MonoBehaviour
    {

        public bool isGrounded { get; private set; } = false;

        public bool isJumping { get; private set; } = false;

        public int remainingJumps { get; private set; }

        public int maxJumps { get; private set; } = 2;

        public float playerWalkSpeed { get; private set; } = 2.5f;

        public float playerJumpForce { get; private set; } = 0.5f;

        private float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;

        private Rigidbody2D pRigidbody2D;

        private PlayerController pController;

        void Start()
        {
            remainingJumps = maxJumps;

            pRigidbody2D = GetComponent<Rigidbody2D>();
            pController = GetComponent<PlayerController>();
        }

        void FixedUpdate()
        {
            if (pRigidbody2D.velocity.y < 0)
            {
                pRigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if(pRigidbody2D.velocity.y > 0 && !Input.GetButtonDown("Jump"))
            {
                pRigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        public void PlayerMove(float _horInput)
        {
            pRigidbody2D.velocity = new Vector2(_horInput, pRigidbody2D.velocity.y) * playerWalkSpeed;
        }

        public void PlayerJump()
        {
            if (remainingJumps > 0)
            {
                isJumping = true;
                pRigidbody2D.velocity = Vector2.up * playerJumpForce;
                remainingJumps--;
                return;
            }
        }

        public void ResetJumps()
        {
            remainingJumps = maxJumps;
            isJumping = false;
        }
    }
}
