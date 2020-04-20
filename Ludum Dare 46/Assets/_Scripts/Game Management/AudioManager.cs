using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public GameAudioClip[] gameAudioClips;

        public Dictionary<GameAudioClip.GameClip, float> audioTimerDictionary;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            audioTimerDictionary = new Dictionary<GameAudioClip.GameClip, float>();
            audioTimerDictionary.Add(GameAudioClip.GameClip.DYING_TIMER_BEEP, 0);
        }

        public void PlaySound(GameAudioClip.GameClip _gameClip)
        {
            if (CanPlaySound(_gameClip))
            {
                GameObject _oneTimeClipGameObject = new GameObject("One-Time-Clip-Sound");
                AudioSource _onTimeClipAudioSource = _oneTimeClipGameObject.AddComponent<AudioSource>();

                GameAudioClip _foundGameClip = GetAudioClip(_gameClip);
                AudioClip _clip = _foundGameClip.audioClip;

                _onTimeClipAudioSource.volume = _foundGameClip.clipVolume;

                _onTimeClipAudioSource.PlayOneShot(_clip);

                Destroy(_oneTimeClipGameObject, _clip.length);
            }
        }

        private bool CanPlaySound(GameAudioClip.GameClip _gameClip)
        {
            switch (_gameClip)
            {
                default:
                    return true;

                case GameAudioClip.GameClip.DYING_TIMER_BEEP:
                    if (audioTimerDictionary.ContainsKey(_gameClip))
                    {
                        float _lastTimePlayed = audioTimerDictionary[_gameClip];
                        float _timeBetweenPlays = GetAudioClip(_gameClip).audioClip.length;
                        if (_lastTimePlayed + _timeBetweenPlays < Time.time)
                        {
                            audioTimerDictionary[_gameClip] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
            }
        }

        private GameAudioClip GetAudioClip(GameAudioClip.GameClip _gameClip)
        {
            foreach (GameAudioClip _clip in gameAudioClips)
            {
                if (_clip.gameClip == _gameClip)
                {
                    return _clip;
                }
            }
            Debug.LogError("AudioClip not found : " + _gameClip);
            return null;
        }
    }

    [System.Serializable]
    public class GameAudioClip{

        public string name;

        public GameClip gameClip;

        public AudioClip audioClip;

        [Range(0, 1)]
        public float clipVolume = 1f;

        public enum GameClip { 
            BEING_HURT,
            BEING_RECHARGE,
            BEING_RESTORE,
            BEING_DIE,
            ENEMY_DIE,
            ENEMY_HIT,
            BUTTON_CLICK,
            RESTORE_PICKUP,
            RECHARGE_PICKUP,
            PLAYER_JUMP,
            DYING_TIMER_BEEP,
            WARP_DASH,
            WARP_JUMP
        };
    }
}
