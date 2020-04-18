using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class CameraTracker : MonoBehaviour
    {
        public Transform cameraTarget;

        public Vector2 cameraOffset;

        public float moveSpeed = 1f;

        void Update()
        {
            if (cameraTarget != null)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(cameraTarget.position.x + cameraOffset.x, cameraTarget.position.y + cameraOffset.y, transform.position.z), moveSpeed * Time.deltaTime);
            }
        }
    }
}
