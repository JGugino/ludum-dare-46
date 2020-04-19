using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement.GUIInterfacing;

namespace PlayerManagement
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController pController;

        void Start()
        {
            pController = GetComponent<PlayerController>();
        }

        void Update()
        {
            if (pController.playerState == PlayerStates.NORMAL && !pController.isDead)
            {
                float _horInput = Input.GetAxisRaw("Horizontal");

                if (Input.GetButtonDown("Jump"))
                {
                    pController.pMotor.PlayerJump();
                }
                if (Input.GetButtonUp("Jump")) {
                    pController.playerAnimator.ResetTrigger("isJumping");
                }

                if (Input.GetButtonDown("Warp Dash") && (_horInput > 0 || _horInput < 0))
                {
                    pController.pMotor.WarpDash(_horInput);
                }

                pController.pMotor.PlayerMove(_horInput);
            }

            if (pController.playerState == PlayerStates.WARP_FOCUS && !pController.isDead)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (pController.canWarpJumpHere)
                    {
                        if (pController.pMotor.canWarpJump)
                        {
                            Vector3 _pos = pController.portalTransform.position;

                            pController.pMotor.teleportPlayerToPosition(_pos);

                            if (pController.pMotor.canWarpJump)
                            {
                                GUIControls.instance.changeBeingWarpJumpUI(BeingWarpJumpState.OPEN);
                            }
                            else if (!pController.pMotor.canWarpJump)
                            {
                                GUIControls.instance.changeBeingWarpJumpUI(BeingWarpJumpState.CLOSED);
                            }
                        }
                    }
                }
            }

            if (Input.GetButtonDown("Warp Focus"))
            {
                if (pController.pMotor.canWarpJump)
                {
                    switch (pController.playerState)
                    {
                        case PlayerStates.NORMAL:
                            pController.playerState = PlayerStates.WARP_FOCUS;
                            pController.activatePortal();
                            break;

                        case PlayerStates.WARP_FOCUS:
                            pController.playerState = PlayerStates.NORMAL;
                            pController.deactivatePortal();
                            break;
                    }
                }
            }
        }
    }
}
