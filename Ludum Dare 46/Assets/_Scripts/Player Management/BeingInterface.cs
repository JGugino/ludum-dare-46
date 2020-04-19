﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerManagement {
    public class BeingInterface : MonoBehaviour
    {
        public Image beingEyes, beingEyesDead;
        public Image beingHeadFull, beingHeadQuater;
        public Image beingWarpJumpClosed, beingWarpJumpOpen;
        public Image beingTailFull, beingTailQuater, beingTailHalf, beingTailAlmostDead;

        public Sprite[] possibleEyeSprites;

        public void setBeingHead(BeingHeadState _headState)
        {
            switch (_headState)
            {
                case BeingHeadState.FULL:
                    beingHeadFull.gameObject.SetActive(true);
                    beingHeadQuater.gameObject.SetActive(false);
                    break;

                case BeingHeadState.QUATER:
                    beingHeadFull.gameObject.SetActive(false);
                    beingHeadQuater.gameObject.SetActive(true);
                    break;
            }
        }

        public void setBeingEyes(BeingEyeState _eyeState)
        {
            switch (_eyeState)
            {
                case BeingEyeState.FULL:
                    beingEyes.gameObject.SetActive(true);
                    beingEyes.sprite = possibleEyeSprites[0];
                    beingEyesDead.gameObject.SetActive(false);
                    break;
                case BeingEyeState.QUATER:
                    beingEyes.gameObject.SetActive(true);
                    beingEyes.sprite = possibleEyeSprites[1];
                    beingEyesDead.gameObject.SetActive(false);
                    break;
                case BeingEyeState.HALF:
                    beingEyes.gameObject.SetActive(true);
                    beingEyes.sprite = possibleEyeSprites[2];
                    beingEyesDead.gameObject.SetActive(false);
                    break;
                case BeingEyeState.DEAD:
                    beingEyes.gameObject.SetActive(false);
                    beingEyesDead.gameObject.SetActive(true);
                    break;
            }
        }

        public void setBeingTail(BeingTailState _tailState)
        {
            switch (_tailState)
            {
                case BeingTailState.FULL:
                    beingTailFull.gameObject.SetActive(true);
                    beingTailQuater.gameObject.SetActive(false);
                    beingTailHalf.gameObject.SetActive(false);
                    beingTailAlmostDead.gameObject.SetActive(false);
                    break;
                case BeingTailState.QUATER:
                    beingTailFull.gameObject.SetActive(false);
                    beingTailQuater.gameObject.SetActive(true);
                    beingTailHalf.gameObject.SetActive(false);
                    beingTailAlmostDead.gameObject.SetActive(false);
                    break;
                case BeingTailState.HALF:
                    beingTailFull.gameObject.SetActive(false);
                    beingTailQuater.gameObject.SetActive(false);
                    beingTailHalf.gameObject.SetActive(true);
                    beingTailAlmostDead.gameObject.SetActive(false);
                    break;
                case BeingTailState.ALMOST_DEAD:
                    beingTailFull.gameObject.SetActive(false);
                    beingTailQuater.gameObject.SetActive(false);
                    beingTailHalf.gameObject.SetActive(false);
                    beingTailAlmostDead.gameObject.SetActive(true);
                    break;
            }
        }

        public void setBeingWarpJump(BeingWarpJumpState _warpJumpState)
        {
            switch (_warpJumpState)
            {
                case BeingWarpJumpState.OPEN:
                    beingWarpJumpClosed.gameObject.SetActive(false);
                    beingWarpJumpOpen.gameObject.SetActive(true);
                    break;

                case BeingWarpJumpState.CLOSED:
                    beingWarpJumpClosed.gameObject.SetActive(true);
                    beingWarpJumpOpen.gameObject.SetActive(false);
                    break;
            }
        }
    }
    public enum BeingHeadState
    {
        FULL,QUATER
    }
    public enum BeingEyeState
    {
        FULL, QUATER, HALF, DEAD
    }
    public enum BeingTailState
    {
        FULL, QUATER, HALF, ALMOST_DEAD
    }
    public enum BeingWarpJumpState
    {
        OPEN, CLOSED
    }
}