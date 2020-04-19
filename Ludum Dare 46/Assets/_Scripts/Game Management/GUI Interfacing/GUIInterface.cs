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

        [HideInInspector]
        public BeingInterface beingUI;

        //Main Menu Buttons
        [HideInInspector]
        public Button mainMenuPlayButton, mainMenuControlsButton, mainMenuExitButton;

        [HideInInspector]
        public Button levelSelectExitButton, levelOneButton, levelTwoButton, levelThreeButton, levelFourButton;

        [HideInInspector]
        public GameObject mainMenuScreen, levelSelectScreen;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            FindMenuUI();
        }

        public void FindMenuUI()
        {
            mainMenuScreen = GameObject.FindGameObjectWithTag("Main-Menu-Screen");

            mainMenuPlayButton = GameObject.FindGameObjectWithTag("Main-Menu-Play-Button").GetComponent<Button>();
            mainMenuControlsButton = GameObject.FindGameObjectWithTag("Main-Menu-Controls-Button").GetComponent<Button>();
            mainMenuExitButton = GameObject.FindGameObjectWithTag("Main-Menu-Exit-Button").GetComponent<Button>();

            levelSelectScreen = GameObject.FindGameObjectWithTag("Level-Select-Screen");
            
            levelOneButton = GameObject.FindGameObjectWithTag("Level-One-Button").GetComponent<Button>();
            levelTwoButton = GameObject.FindGameObjectWithTag("Level-Two-Button").GetComponent<Button>();
            levelThreeButton = GameObject.FindGameObjectWithTag("Level-Three-Button").GetComponent<Button>();
            levelFourButton = GameObject.FindGameObjectWithTag("Level-Four-Button").GetComponent<Button>();

            levelSelectExitButton = GameObject.FindGameObjectWithTag("Level-Select-Exit-Button").GetComponent<Button>();

            levelSelectScreen.SetActive(false) ;

            AssignMenuButtonListeners();
        }

        public void AssignMenuButtonListeners()
        {
            mainMenuPlayButton.onClick.AddListener(delegate {
                mainMenuScreen.SetActive(false);
                levelSelectScreen.SetActive(true);
            });
            mainMenuControlsButton.onClick.AddListener(delegate { Debug.Log("Controls"); });
            mainMenuExitButton.onClick.AddListener(delegate { Application.Quit(); Debug.Log("Exit"); });

            levelSelectExitButton.onClick.AddListener(delegate {
                mainMenuScreen.SetActive(true);
                levelSelectScreen.SetActive(false);
            });

            //Level Buttons
            levelOneButton.onClick.AddListener(delegate { StartCoroutine(GameController.instance.LoadLevel(1)); });
            levelTwoButton.onClick.AddListener(delegate { StartCoroutine(GameController.instance.LoadLevel(2)); });
            levelThreeButton.onClick.AddListener(delegate { StartCoroutine(GameController.instance.LoadLevel(3)); });
            levelFourButton.onClick.AddListener(delegate { StartCoroutine(GameController.instance.LoadLevel(4)); });
        }

        public void FindLevelUI()
        {
            beingUI = GameObject.FindGameObjectWithTag("Being-UI").GetComponent<BeingInterface>();

            beingUI.setBeingHead(BeingHeadState.FULL);
            beingUI.setBeingEyes(BeingEyeState.FULL);
            beingUI.setBeingTail(BeingTailState.FULL);
        }
    }
}
