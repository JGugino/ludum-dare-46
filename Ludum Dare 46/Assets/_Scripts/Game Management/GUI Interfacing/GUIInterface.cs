using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerManagement;
using TMPro;

namespace GameManagement.GUIInterfacing
{
    public class GUIInterface : MonoBehaviour
    {
        public static GUIInterface instance;

        //Main Menu / Level Select UI
        [HideInInspector]
        public GameObject mainMenuScreen, levelSelectScreen, controlsScreen, moreInfoScreen;

        [HideInInspector]
        public Button mainMenuPlayButton, mainMenuControlsButton, mainMenuExitButton;

        [HideInInspector]
        public Button controlsMoreInfoButton, controlsExitButton, moreInfoExitButton;

        [HideInInspector]
        public Button levelSelectExitButton, levelOneButton, levelTwoButton, levelThreeButton, levelFourButton;

        //Level UI
        [HideInInspector]
        public BeingInterface beingUI;

        [HideInInspector]
        public GameObject deathScreen, winScreen;

        [HideInInspector]
        public TextMeshProUGUI dyingTimerText;

        [HideInInspector]
        public Button deathScreenRestartButton, deathScreenExitButton, winScreenRestartButton, winScreenExitButton;

        void Awake()
        {
            instance = this;
        }

        public void FindMenuUI()
        {
            mainMenuScreen = GameObject.FindGameObjectWithTag("Main-Menu-Screen");

            controlsScreen = GameObject.FindGameObjectWithTag("Controls-Screen");
            moreInfoScreen = GameObject.FindGameObjectWithTag("More-Info-Screen");

            mainMenuPlayButton = GameObject.FindGameObjectWithTag("Main-Menu-Play-Button").GetComponent<Button>();
            mainMenuControlsButton = GameObject.FindGameObjectWithTag("Main-Menu-Controls-Button").GetComponent<Button>();
            mainMenuExitButton = GameObject.FindGameObjectWithTag("Main-Menu-Exit-Button").GetComponent<Button>();

            controlsMoreInfoButton = GameObject.FindGameObjectWithTag("Controls-More-Info-Button").GetComponent<Button>();
            controlsExitButton = GameObject.FindGameObjectWithTag("Controls-Exit-Button").GetComponent<Button>();

            moreInfoExitButton = GameObject.FindGameObjectWithTag("More-Info-Exit-Button").GetComponent<Button>();

            levelSelectScreen = GameObject.FindGameObjectWithTag("Level-Select-Screen");
            
            levelOneButton = GameObject.FindGameObjectWithTag("Level-One-Button").GetComponent<Button>();
            levelTwoButton = GameObject.FindGameObjectWithTag("Level-Two-Button").GetComponent<Button>();
            levelThreeButton = GameObject.FindGameObjectWithTag("Level-Three-Button").GetComponent<Button>();
            levelFourButton = GameObject.FindGameObjectWithTag("Level-Four-Button").GetComponent<Button>();

            levelSelectExitButton = GameObject.FindGameObjectWithTag("Level-Select-Exit-Button").GetComponent<Button>();

            AssignMenuButtonListeners();
        }

        public void AssignMenuButtonListeners()
        {

            mainMenuPlayButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GUIControls.instance.enableLevelSelectScreen();
            });
            mainMenuControlsButton.onClick.AddListener(delegate { 
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GUIControls.instance.enableControlsScreen();
            });
            mainMenuExitButton.onClick.AddListener(delegate { 
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                Application.Quit();
            });

            controlsMoreInfoButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GUIControls.instance.enableMoreInfoScreen();
            });

            controlsExitButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GUIControls.instance.disableControlsScreen();
            });

            moreInfoExitButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GUIControls.instance.disableMoreInfoScreen();
            });

            //Level Buttons
            levelOneButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.StartLevelLoad(1);
            });
            levelTwoButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.StartLevelLoad(2);
            });
            levelThreeButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.StartLevelLoad(3); 
            });
            levelFourButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.StartLevelLoad(4); 
            });

            levelSelectExitButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GUIControls.instance.disableLevelSelectScreen();
            });

            controlsScreen.SetActive(false);
            moreInfoScreen.SetActive(false);
            levelSelectScreen.SetActive(false);
        }

        public void FindLevelUI()
        {
            beingUI = GameObject.FindGameObjectWithTag("Being-UI").GetComponent<BeingInterface>();

            dyingTimerText = GameObject.FindGameObjectWithTag("Dying-Timer-Text").GetComponent<TextMeshProUGUI>();

            deathScreen = GameObject.FindGameObjectWithTag("Death-Screen");
            winScreen = GameObject.FindGameObjectWithTag("Win-Screen");

            deathScreenRestartButton = GameObject.FindGameObjectWithTag("Death-Screen-Restart-Button").GetComponent<Button>();
            deathScreenExitButton = GameObject.FindGameObjectWithTag("Death-Screen-Exit-Button").GetComponent<Button>();

            winScreenRestartButton = GameObject.FindGameObjectWithTag("Win-Screen-Restart-Button").GetComponent<Button>();
            winScreenExitButton = GameObject.FindGameObjectWithTag("Win-Screen-Exit-Button").GetComponent<Button>();

            dyingTimerText.gameObject.SetActive(false);

            winScreen.SetActive(false);

            deathScreen.SetActive(false);

            AssignLevelsButtonListeners();
        }

        public void AssignLevelsButtonListeners()
        {
            deathScreenRestartButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.startLevelRestart();
            });
            deathScreenExitButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.startExitToMenu();
            });

            winScreenRestartButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.startLevelRestart();
            });
            winScreenExitButton.onClick.AddListener(delegate {
                AudioManager.instance.PlaySound(GameAudioClip.GameClip.BUTTON_CLICK);
                GameController.instance.startExitToMenu();
            });
        }

    }
}
