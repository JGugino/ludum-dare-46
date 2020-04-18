using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement.GUIInterfacing;

namespace GameManagement
{
    [RequireComponent(typeof(GUIInterface))]
    [RequireComponent(typeof(GUIControls))]
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

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
    }
}
