using PlayerManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement.GUIInterfacing {
    public class GUIControls : MonoBehaviour
    {
        public static GUIControls instance;

        void Awake()
        {
            instance = this;
        }
        
        public void updateDyingTimerText(float[] _time)
        {
            GUIInterface.instance.dyingTimerText.text = _time[1].ToString("00");
        }

        public void changeBeingUI(int _beingCharge, int _beingMaxCharge)
        {
            int _being75Percent = _beingMaxCharge - (_beingMaxCharge/4);
            int _being50Percent = _beingMaxCharge / 2;
            int _being25Percent = _beingMaxCharge / 4;

            if (_beingCharge >= _being75Percent)
            {
                GUIInterface.instance.beingUI.setBeingHead(BeingHeadState.FULL);
                GUIInterface.instance.beingUI.setBeingEyes(BeingEyeState.FULL);
                GUIInterface.instance.beingUI.setBeingTail(BeingTailState.FULL);
            }
            else if (_beingCharge < _being75Percent && _beingCharge >= _being50Percent)
            {
                GUIInterface.instance.beingUI.setBeingHead(BeingHeadState.QUATER);
                GUIInterface.instance.beingUI.setBeingEyes(BeingEyeState.QUATER);
                GUIInterface.instance.beingUI.setBeingTail(BeingTailState.QUATER);
            }
            else if (_beingCharge < _being50Percent && _beingCharge >= _being25Percent)
            {
                GUIInterface.instance.beingUI.setBeingHead(BeingHeadState.QUATER);
                GUIInterface.instance.beingUI.setBeingEyes(BeingEyeState.HALF);
                GUIInterface.instance.beingUI.setBeingTail(BeingTailState.HALF);
            }
            else if (_beingCharge < _being25Percent && _beingCharge > 0)
            {
                GUIInterface.instance.beingUI.setBeingHead(BeingHeadState.QUATER);
                GUIInterface.instance.beingUI.setBeingEyes(BeingEyeState.HALF);
                GUIInterface.instance.beingUI.setBeingTail(BeingTailState.ALMOST_DEAD);
            }
            else
            {
                GUIInterface.instance.beingUI.setBeingHead(BeingHeadState.QUATER);
                GUIInterface.instance.beingUI.setBeingEyes(BeingEyeState.DEAD);
                GUIInterface.instance.beingUI.setBeingTail(BeingTailState.ALMOST_DEAD);
            }
        }

        public void changeBeingWarpJumpUI(BeingWarpJumpState _state)
        {
            GUIInterface.instance.beingUI.setBeingWarpJump(_state);
        }

        public void changeBeingWarpDashUI(BeingWarpJumpState _state)
        {
            GUIInterface.instance.beingUI.setBeingWarpJump(_state);
        }

        public void enableLevelSelectScreen()
        {
            GUIInterface.instance.mainMenuScreen.SetActive(false);
            GUIInterface.instance.levelSelectScreen.SetActive(true);
            GUIInterface.instance.controlsScreen.SetActive(false);
            GUIInterface.instance.moreInfoScreen.SetActive(false);
        }

        public void disableLevelSelectScreen()
        {
            GUIInterface.instance.mainMenuScreen.SetActive(true);
            GUIInterface.instance.levelSelectScreen.SetActive(false);
            GUIInterface.instance.controlsScreen.SetActive(false);
            GUIInterface.instance.moreInfoScreen.SetActive(false);
        }

        public void enableControlsScreen()
        {
            GUIInterface.instance.mainMenuScreen.SetActive(false);
            GUIInterface.instance.levelSelectScreen.SetActive(false);
            GUIInterface.instance.controlsScreen.SetActive(true);
            GUIInterface.instance.moreInfoScreen.SetActive(false);
        }

        public void disableControlsScreen()
        {
            GUIInterface.instance.mainMenuScreen.SetActive(true);
            GUIInterface.instance.levelSelectScreen.SetActive(false);
            GUIInterface.instance.controlsScreen.SetActive(false);
            GUIInterface.instance.moreInfoScreen.SetActive(false);
        }

        public void enableMoreInfoScreen()
        {
            GUIInterface.instance.mainMenuScreen.SetActive(false);
            GUIInterface.instance.levelSelectScreen.SetActive(false);
            GUIInterface.instance.controlsScreen.SetActive(false);
            GUIInterface.instance.moreInfoScreen.SetActive(true);
        }

        public void disableMoreInfoScreen()
        {
            GUIInterface.instance.mainMenuScreen.SetActive(false);
            GUIInterface.instance.levelSelectScreen.SetActive(false);
            GUIInterface.instance.controlsScreen.SetActive(true);
            GUIInterface.instance.moreInfoScreen.SetActive(false);
        }

    }
}
