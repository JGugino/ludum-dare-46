using System.Collections;
using UnityEngine;
using GameManagement.GUIInterfacing;

namespace GameManagement
{
    public class MainMenuController : MonoBehaviour
    {
        void Start()
        {
            DyingTimer.instance.timerStarted = false;
            DyingTimer.instance.resetTimer();
            GUIInterface.instance.FindMenuUI();
        }

        void Update()
        {
            AudioManager.instance.PlaySound(GameAudioClip.GameClip.MENU_BACKGROUND);

            if (DyingTimer.instance.timerStarted)
            {
                DyingTimer.instance.timerStarted = false;
            }    
        }
    }
}
