using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

namespace EnemyManagement
{
    public class PatrolEnemyController : MonoBehaviour
    {
        public Transform[] enemyMovePoints;

        public float enemyWalkSpeed = 1.5f;

        public EnemyPatrolState _currentPatrolState = EnemyPatrolState.IDLE;

        public bool canBeHeadKilled = false;

        private bool isIdling = false, isMoving = false, reachedLeftPoint = false,
            reachedRightPoint = false, taskIndexPicked = false, shouldRunAI = false;

        private int nextTaskIndex = 0;

        private float activateDistance = 10f;

        private Transform player;

        public bool isDead { get; private set; }

        void Update()
        {
            if (!GameController.instance.isPaused)
            {
                if (PlayerSpawner.instance.spawnedPlayer != null)
                {
                    float _playerDistanceX = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(PlayerSpawner.instance.spawnedPlayer.transform.position.x, 0));

                    if (_playerDistanceX <= activateDistance)
                    {
                        if (!shouldRunAI)
                        {
                            shouldRunAI = true;
                        }
                    }
                    else
                    {
                        if (shouldRunAI)
                        {
                            shouldRunAI = false;
                        }
                    }

                    if (shouldRunAI)
                    {
                        EnemyPatrolAI();
                    }
                }
            }
        }

        void EnemyPatrolAI()
        {
            switch (_currentPatrolState)
            {
                case EnemyPatrolState.MOVING_LEFT:
                    if (!reachedLeftPoint)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemyMovePoints[0].position.x, transform.position.y, transform.position.z), enemyWalkSpeed * Time.deltaTime);
                        if (!isMoving)
                        {
                            isMoving = true;
                        }

                    }
                    else if (reachedLeftPoint)
                    {
                        _currentPatrolState = EnemyPatrolState.IDLE;
                        isMoving = false;
                    }
                    break;

                case EnemyPatrolState.MOVING_RIGHT:
                    if (!reachedRightPoint)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemyMovePoints[1].position.x, transform.position.y, transform.position.z), enemyWalkSpeed * Time.deltaTime);

                        if (!isMoving)
                        {
                            isMoving = true;
                        }
                    }
                    else if (reachedRightPoint)
                    {
                        _currentPatrolState = EnemyPatrolState.IDLE;
                        isMoving = false;
                    }
                    break;

                case EnemyPatrolState.IDLE:
                    if (!isIdling && !isMoving)
                    {
                        taskIndexPicked = false;

                        if (!taskIndexPicked)
                        {
                            nextTaskIndex = Random.Range(0, 10);
                            taskIndexPicked = true;
                        }

                        //Move Left
                        if (nextTaskIndex <= 4)
                        {
                            if (!reachedLeftPoint)
                            {
                                _currentPatrolState = EnemyPatrolState.MOVING_LEFT;
                                taskIndexPicked = false;
                            }
                        }//Move Right
                        else if (nextTaskIndex >= 5 && nextTaskIndex <= 8)
                        {
                            if (!reachedRightPoint)
                            {
                                _currentPatrolState = EnemyPatrolState.MOVING_RIGHT;
                                taskIndexPicked = false;
                            }
                        }//Idle
                        else if (nextTaskIndex >= 9)
                        {
                            _currentPatrolState = EnemyPatrolState.IDLE;
                            isIdling = true;
                            StartCoroutine(enemyWait());
                            return;
                        }
                    }
                    break;
            }
        }

        public void KillEnemy()
        {
            isDead = true;
            this.gameObject.SetActive(false);
        }

        IEnumerator enemyWait()
        {
            yield return new WaitForSeconds(2.5f);
            taskIndexPicked = false;
            isIdling = false;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            string _otherTag = collision.tag;

            if (_otherTag == "Enemy-Left-Move-Point")
            {
                reachedLeftPoint = true;
                reachedRightPoint = false;
                taskIndexPicked = false;
                isMoving = false;
            }

            if (_otherTag == "Enemy-Right-Move-Point")
            {
                reachedRightPoint = true;
                reachedLeftPoint = false;
                isMoving = false;
            }
        }
    }

    public enum EnemyPatrolState
    {
        IDLE, MOVING_LEFT, MOVING_RIGHT
    }
}
