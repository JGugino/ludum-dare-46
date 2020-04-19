using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyManagement;
using GameManagement;
using GameManagement.GUIInterfacing;
using TMPro;

namespace PlayerManagement
{
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerMotor pMotor { get; private set; }
        public PlayerInput pInput { get; private set; }

        public int beingCurrentCharge { get; private set; }
        public int beingDecayedCharge;
        public int beingMaxCharge { get; private set; } = 50;

        public bool isDead { get; private set; } = false;

        public PlayerStates playerState = PlayerStates.NORMAL;

        private float currentChargeRechargeDelay, maxChargeRechargeDelay = 2;

        private float currentChargeDecayDelay, maxChargeDecayDelay = 10;
        public bool canWarpJumpHere { get; private set; } = true;

        public Color canWarpHereColor, cantWarpHereColor;

        public SpriteRenderer playerSpriteRenderer;

        public Animator playerAnimator;

        public Transform portalTransform;

        private SpriteRenderer portalSpriteRenderer;

        private float warpJumpRange = 6.8f;

        [SerializeField]
        private TextMeshProUGUI currentChargeText;

        void Start()
        {
            pMotor = GetComponent<PlayerMotor>();
            pInput = GetComponent<PlayerInput>();

            beingCurrentCharge = beingMaxCharge;
            beingDecayedCharge = beingMaxCharge;

            currentChargeRechargeDelay = maxChargeRechargeDelay;
            currentChargeDecayDelay = maxChargeDecayDelay;

            portalSpriteRenderer = portalTransform.GetComponentInChildren<SpriteRenderer>();
        }

        void Update()
        {
            if (!isDead && !GameController.instance.isPaused)
            {
                if (playerState == PlayerStates.WARP_FOCUS)
                {
                    float _dist = Vector2.Distance(transform.position, portalTransform.position);

                    Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (_dist <= warpJumpRange)
                    {
                        portalTransform.position = _mousePos;

                        Collider2D[] _insidePortal = Physics2D.OverlapCircleAll(portalTransform.position, 0.5f);

                        canWarpJumpHere = VaildSpot(_insidePortal);

                        if (!canWarpJumpHere)
                        {
                            portalSpriteRenderer.color = cantWarpHereColor;
                        }
                        else if (canWarpJumpHere)
                        {
                            portalSpriteRenderer.color = canWarpHereColor;
                        }
                    }
                    else
                    {
                        canWarpJumpHere = false;
                        portalTransform.position = _mousePos;
                        portalSpriteRenderer.color = cantWarpHereColor;
                    }
                }

                BeingRechargeandDecay();
            }
        }

        void BeingRechargeandDecay()
        {
            if (beingDecayedCharge < beingMaxCharge)
            {
                if (beingCurrentCharge < beingDecayedCharge)
                {
                    if (currentChargeRechargeDelay <= 0)
                    {
                        giveBeingCharge(1);
                        currentChargeRechargeDelay = maxChargeRechargeDelay;
                        GUIControls.instance.changeBeingUI(beingCurrentCharge, beingMaxCharge);
                        return;
                    }
                    else
                    {
                        currentChargeRechargeDelay -= Time.deltaTime;
                    }
                }
            }
            else if (beingDecayedCharge >= beingMaxCharge)
            {
                if (beingCurrentCharge < beingMaxCharge)
                {
                    if (currentChargeRechargeDelay <= 0)
                    {
                        giveBeingCharge(1);
                        currentChargeRechargeDelay = maxChargeRechargeDelay;
                        GUIControls.instance.changeBeingUI(beingCurrentCharge, beingMaxCharge);
                        return;
                    }
                    else
                    {
                        currentChargeRechargeDelay -= Time.deltaTime;
                    }
                }
            }

            if (beingCurrentCharge > 0)
            {
                if (currentChargeDecayDelay <= 0)
                {
                    if (beingDecayedCharge > 0)
                    {
                        beingDecayedCharge--;
                        GUIControls.instance.changeBeingUI(beingCurrentCharge, beingMaxCharge);
                    }
                    currentChargeDecayDelay = maxChargeDecayDelay;
                    return;
                }
                else
                {
                    currentChargeDecayDelay -= Time.deltaTime;
                }
            }
        }

        public void activatePortal()
        {
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            portalTransform.position = _mousePos;

            portalTransform.gameObject.SetActive(true);
        }
        public void deactivatePortal()
        {
            portalTransform.gameObject.SetActive(false);
            portalTransform.position = Vector2.zero;
        }

        public bool takeBeingCharge(int _amount)
        {
            int _final = beingCurrentCharge - _amount;

            if (_final > 0)
            {
                beingCurrentCharge = _final;
                return true;
            }
            else if(_final <= 0)
            {
                isDead = true;
                beingCurrentCharge = 0;
                killPlayer();
            }

            return false;
        }

        public void giveBeingCharge(int _amount)
        {
            int _final = beingCurrentCharge + _amount;

            //Use beingDecayedCharge for max charge
            if (beingDecayedCharge < beingMaxCharge)
            {
                if (_final <= beingDecayedCharge)
                {
                    beingCurrentCharge = _final;
                    return;
                }
                else if(_final > beingDecayedCharge)
                {
                    beingCurrentCharge = beingDecayedCharge;
                    return;
                }
            }

            //Use beingMaxCharge for max charge
            if (beingDecayedCharge >= beingMaxCharge)
            {
                if (_final <= beingMaxCharge)
                {
                    beingCurrentCharge = _final;
                    return;
                }
                else if (_final > beingMaxCharge)
                {
                    beingCurrentCharge = beingMaxCharge;
                    return;
                }
            }


            if (_final >= 0)
            {
                beingCurrentCharge = _final;
            }
            else
            {
                beingCurrentCharge = 0;
            }
        }

        void killPlayer()
        {
            //TODO: Add in game over screen to restart level or exit to menu
            Debug.Log("Dead");
            deactivatePortal();
            GUIControls.instance.changeBeingUI(beingCurrentCharge, beingMaxCharge);
        }

        bool VaildSpot(Collider2D[] _objects)
        {
            if (_objects.Length > 0)
            {
                List<Collider2D> enemies = new List<Collider2D>();
                List<Collider2D> ground = new List<Collider2D>();

                foreach (Collider2D _currentObject in _objects)
                {
                    if (_currentObject.tag == "Enemy" || _currentObject.tag == "Enemy-Head")
                    {
                        enemies.Add(_currentObject);
                    }else if (_currentObject.tag == "Ground-Tiles")
                    {
                        ground.Add(_currentObject);
                    }
                }

                if (ground.Count <= 0 && enemies.Count <= 0)
                {
                    ground.Clear();
                    enemies.Clear();
                    return true;
                }
                else
                {
                    ground.Clear();
                    enemies.Clear();
                    return false;
                }

            }
            else
            {
                return true;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            string _otherTag = collision.collider.tag;

            if (_otherTag == "Ground-Tiles")
            {
                pMotor.ResetJumps();
            }

            if (_otherTag == "Enemy")
            {
                if (pMotor.isWarpDashing)
                {
                    collision.collider.GetComponentInParent<PatrolEnemyController>().KillEnemy();
                }
                else if(!pMotor.isWarpDashing)
                {
                    if (pMotor.playerDirection == PlayerDirection.LEFT)
                    {
                        pMotor.pRigidbody2D.velocity = Vector2.right * 3.8f;
                    }
                    else if (pMotor.playerDirection == PlayerDirection.RIGHT)
                    {
                        pMotor.pRigidbody2D.velocity = Vector2.left * 3.8f ;
                    }
                    takeBeingCharge(2);
                }
            }

            if (_otherTag == "Enemy-Head")
            {
                PatrolEnemyController _pEnemyController = collision.collider.GetComponentInParent<PatrolEnemyController>();

                if (_pEnemyController.canBeHeadKilled)
                {
                    _pEnemyController.KillEnemy();
                }

                pMotor.pRigidbody2D.velocity = Vector2.up * pMotor.playerJumpForce;

                pMotor.ResetJumps();
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            string _otherTag = collision.tag;

            if (_otherTag == "Recharge-Orb")
            {
                giveBeingCharge(Random.Range(6, 12));
                collision.gameObject.SetActive(false);
            }

            if (_otherTag == "Restore-Orb")
            {
                int _amountToAdd = Random.Range(2, 5);

                int _final = beingDecayedCharge + _amountToAdd;

                if (_final < beingMaxCharge)
                {
                    beingDecayedCharge = _final;
                }else if (_final > beingMaxCharge)
                {
                    beingDecayedCharge = beingMaxCharge;
                }
                collision.gameObject.SetActive(false);
            }
        }
    }

    public enum PlayerStates
    {
        NORMAL, WARP_FOCUS
    }
}
