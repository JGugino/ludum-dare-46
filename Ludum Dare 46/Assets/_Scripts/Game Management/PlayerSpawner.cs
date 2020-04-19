using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerManagement;
using GameManagement.GUIInterfacing;

namespace GameManagement
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner instance;

        [SerializeField]
        private GameObject playerPrefab;

        [HideInInspector]
        public PlayerController spawnedPlayer;

        void Awake()
        {
            instance = this;
        }

        public void spawnPlayer()
        {
            Transform _playerSpawn = GameObject.FindGameObjectWithTag("Player-Spawn").transform;

            spawnedPlayer = Instantiate(playerPrefab, _playerSpawn).GetComponent<PlayerController>();

            CameraTracker.instance.cameraTarget = spawnedPlayer.transform;
        }
    }
}