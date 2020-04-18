using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameManagement.GUIInterfacing
{
    public class GUIInterface : MonoBehaviour
    {
        public static GUIInterface instance;

        //Main Menu Buttons
        [HideInInspector]
        public Button mainMenuPlayButton, mainMenuControlsButton, mainMenuExitButton;

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

            mainMenuPlayButton = GameObject.FindGameObjectWithTag("Main-Menu-Play-Button").GetComponent<Button>();
            mainMenuControlsButton = GameObject.FindGameObjectWithTag("Main-Menu-Controls-Button").GetComponent<Button>();
            mainMenuExitButton = GameObject.FindGameObjectWithTag("Main-Menu-Exit-Button").GetComponent<Button>();

            AssignMenuButtonListeners();
        }

        public void AssignMenuButtonListeners()
        {
            mainMenuPlayButton.onClick.AddListener(delegate {

                UnityEngine.SceneManagement.SceneManager.LoadScene(1);

            });
            mainMenuControlsButton.onClick.AddListener(delegate { Debug.Log("Controls"); });
            mainMenuExitButton.onClick.AddListener(delegate { Application.Quit(); Debug.Log("Exit"); });
        }
    }
}
