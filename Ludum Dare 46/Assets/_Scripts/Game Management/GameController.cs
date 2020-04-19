using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement.GUIInterfacing;
using PlayerManagement;

namespace GameManagement
{
    [RequireComponent(typeof(GUIInterface))]
    [RequireComponent(typeof(GUIControls))]
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        public bool isPaused = false, levelStarted = true;

        void Awake()
        {
            if (instance != null) {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public IEnumerator LoadLevel(int _level)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_level);
            yield return new WaitForSeconds(0.5f);
            GameObject _loadingScreen = GameObject.FindGameObjectWithTag("Loading-Screen");
            if (_loadingScreen != null)
            {
                _loadingScreen.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
            GUIInterface.instance.FindLevelUI();
            PlayerSpawner.instance.spawnPlayer();
            yield return new WaitForSeconds(0.5f);
            if (PlayerSpawner.instance.spawnedPlayer.pMotor.canWarpJump)
            {
                GUIControls.instance.changeBeingWarpJumpUI(BeingWarpJumpState.OPEN);
            }
            else if (!PlayerSpawner.instance.spawnedPlayer.pMotor.canWarpJump)
            {
                GUIControls.instance.changeBeingWarpJumpUI(BeingWarpJumpState.CLOSED);
            }
            yield return new WaitForSeconds(0.5f);
            if (_loadingScreen != null)
            {
                _loadingScreen.SetActive(false);
            }
        }
    }
}
