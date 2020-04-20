using System.Collections;
using UnityEngine;
using GameManagement.GUIInterfacing;

namespace GameManagement
{
    public class MainMenuController : MonoBehaviour
    {
        void Start()
        {
            GUIInterface.instance.FindMenuUI();
        }
    }
}
