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
        public float timerLength { get; private set; }

        public bool timerStarted = false;

        public bool timerDone { get; private set; } = false;

        void Awake()
        {
            instance = this;    
        }

        void Start()
        {
            timerLength = maxTimerLength;
        }

        void Update()
        {
            if (!GameController.instance.isPaused)
            {
                if (timerStarted && !timerDone)
                {
                    timerLength -= Time.deltaTime;
                    float[] _minSeconds = DetermineMinutesAndSeconds();

                    GUIControls.instance.updateDyingTimerText(_minSeconds);

                    if (timerLength <= 0)
                    {
                        timerDone = true;
                        return;
                    }
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
