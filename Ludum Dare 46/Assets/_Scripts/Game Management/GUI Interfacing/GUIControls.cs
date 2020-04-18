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

        void Start()
        {

        }
    }
}
