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

        private GameObject _loadingScreen;

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

        public void StartLevelLoad(int _level)
        {
            StartCoroutine(LoadLevel(_level));
        }

        public IEnumerator LoadLevel(int _level)
        {
            yield return new WaitForSeconds(0.2f);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_level);
            yield return new WaitForSeconds(0.5f);
            _loadingScreen = GameObject.FindGameObjectWithTag("Loading-Screen");
            if (_loadingScreen != null)
            {
                _loadingScreen.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
            GUIInterface.instance.FindLevelUI();
            PlayerSpawner.instance.spawnPlayer();
            yield return new WaitForSeconds(0.5f);

            GUIInterface.instance.beingUI.setBeingHead(BeingHeadState.FULL);
            GUIInterface.instance.beingUI.setBeingEyes(BeingEyeState.FULL);
            GUIInterface.instance.beingUI.setBeingTail(BeingTailState.FULL);

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

        public void startLevelRestart()
        {
            StartCoroutine(restartLevel());
        }

        public void startExitToMenu()
        {
            StartCoroutine(exitToMenu());
        }

        public IEnumerator restartLevel()
        {
            int _sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneIndex);
            yield return new WaitForSeconds(0.5f);
            _loadingScreen = GameObject.FindGameObjectWithTag("Loading-Screen");
            GUIInterface.instance.FindLevelUI();
            PlayerSpawner.instance.spawnPlayer();
            GUIInterface.instance.deathScreen.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            if (PlayerSpawner.instance.spawnedPlayer.pMotor.canWarpJump)
            {
                GUIControls.instance.changeBeingWarpJumpUI(BeingWarpJumpState.OPEN);
            }
            else if (!PlayerSpawner.instance.spawnedPlayer.pMotor.canWarpJump)
            {
                GUIControls.instance.changeBeingWarpJumpUI(BeingWarpJumpState.CLOSED);
            }
            yield return new WaitForSeconds(0.8f);

            isPaused = false;

            if (_loadingScreen != null)
            {
                _loadingScreen.SetActive(false);
            }
        }

        public IEnumerator exitToMenu()
        {
            isPaused = false;

            if (_loadingScreen != null)
            {
                _loadingScreen.SetActive(true);
            }

            yield return new WaitForSeconds(0.8f);

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

            StopAllCoroutines();
        }
    }
}
