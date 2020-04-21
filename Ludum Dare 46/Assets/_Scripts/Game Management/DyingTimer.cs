using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement.GUIInterfacing;

namespace GameManagement
{
    public class DyingTimer : MonoBehaviour
    {
        public static DyingTimer instance;

        //Time in seconds
        public float maxTimerLength;
        public float timerLength;

        public bool timerStarted = false;

        public bool timerDone = false;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            timerLength = maxTimerLength;
        }

        void Update()
        {
                if (timerStarted && !timerDone)
                {
                    Debug.Log("timer running");

                    timerLength -= Time.deltaTime;
                    float[] _minSeconds = DetermineMinutesAndSeconds();

                    if (GUIInterface.instance.dyingTimerText != null)
                    {
                        GUIControls.instance.updateDyingTimerText(_minSeconds);
                    }

                    if (timerLength <= 0)
                    {
                        timerDone = true;
                        return;
                    }
                }
        }

        public float[] DetermineMinutesAndSeconds()
        {
            float _seconds = (timerLength % 60);
            float _minutes = ((int)(timerLength / 60) % 60);

            return new float[] { _minutes, _seconds };
        }

        public void resetTimer()
        {
            timerStarted = false;
            timerDone = false;
            timerLength = maxTimerLength;
        }
    }
}
