using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerManagement
{
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerMotor pMotor { get; private set; }
        public PlayerInput pInput { get; private set; }

        void Start()
        {
            pMotor = GetComponent<PlayerMotor>();
            pInput = GetComponent<PlayerInput>();
        }

        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground-Tiles"))
            {
                Debug.Log("ground");
                pMotor.ResetJumps();
            }    
        }
    }
}
