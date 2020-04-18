using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (Input.GetButtonDown("Jump"))
            {
                pController.pMotor.PlayerJump();
            }
        }

        void FixedUpdate()
        {
            float _horInput = Input.GetAxisRaw("Horizontal");

            pController.pMotor.PlayerMove(_horInput);
        }
    }
}
