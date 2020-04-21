using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class CameraTracker : MonoBehaviour
    {
        public static CameraTracker instance;

        [HideInInspector]
        public Transform cameraTarget;

        public Vector2 cameraOffset;

        private float moveSpeed = 5f;

        public float fov = 3.65f;

        private Camera cameraComp;

        void Awake()
        {
            instance = this;    
        }

        void Start()
        {
            cameraComp = GetComponent<Camera>();

            cameraComp.orthographicSize = fov;
            cameraOffset = new Vector2(0,1.75f);
        }

        void Update()
        {
            AudioManager.instance.PlaySound(GameAudioClip.GameClip.LEVEL_BACKGROUND);

            if (cameraTarget != null)
            {
                if (fov != cameraComp.fieldOfView)
                {
                    cameraComp.orthographicSize = fov;
                }

                transform.position = Vector3.Lerp(transform.position, new Vector3(cameraTarget.position.x + cameraOffset.x, cameraTarget.position.y + cameraOffset.y, transform.position.z), moveSpeed * Time.deltaTime);
            }
        }
    }
}
