using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions instance { get; private set; }
    public Player player;
    public Transform holdTransform;
    [SerializeField] private int _dmg;
    [SerializeField] private float _knockback;
    [SerializeField] private float _interactRange;
    [SerializeField] private float speed;
    [SerializeField] private float speedSprint;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundRayLength;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private BoxCollider stickCollider;
    [SerializeField] private float frogRayLength;
    private bool _isAttacking;
    private float _attackCooldown;
    private float _webbedTimer;
    private float _blurredTimer;
    private Ray frogRay;
    private Ray interactRay;
    private IInteractable interactable;
    private PostProcessVolume ppVolume;
    public enum WeaponType
    {
        Hand, 
        Stick,
        Frog
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        player = new Player(speed, speedSprint, jumpForce, groundRayLength, groundLayerMask, GetComponent<Rigidbody>(), transform);
        player.OnAwake();
        _attackCooldown = 0;
        stickCollider.enabled = false;
        ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        ppVolume.enabled = false;
    }

    private void Update()
    {
        player.OnUpdate();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(weapon);
        }
        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
        }
        if (_attackCooldown < 0)
        {
            _attackCooldown = 0;
        }
        if (_webbedTimer > 0)
        {
            _webbedTimer -= Time.deltaTime;
        }
        if (_webbedTimer <= 0)
        {
            _webbedTimer = 0;
        }
        if (_blurredTimer > 0)
        {
            _blurredTimer -= Time.deltaTime;
            ppVolume.enabled = true;
        }
        if (_blurredTimer <= 0)
        {
            _blurredTimer = 0;
            ppVolume.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GrabObject();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ReleaseObject();
        }
    }

    private void FixedUpdate()
    {
        player.OnFixedUpdate();
    }

    private WeaponType weapon;

    private void OnTriggerEnter(Collider other)
    {
        EnemyBase enemy = other.GetComponentInParent<EnemyBase>();
        if (enemy != null)
        {
            enemy.DamageEnemy(_dmg, _knockback);
        }
    }

    private void Attack(WeaponType weapon)
    {
        if (_attackCooldown <= 0 && _webbedTimer <= 0)
        {
            if (weapon == WeaponType.Stick)
            {
                stickCollider.enabled = true;
            }
            if (weapon == WeaponType.Frog)
            {
                if (Physics.Raycast(frogRay, out RaycastHit hit, frogRayLength))
                {
                    if (hit.collider.gameObject.TryGetComponent(out EnemyBase enemy))
                    {
                        enemy.Die();
                        _attackCooldown = 10;
                    }
                }
            }
        }
    }

    private void GrabObject()
    {
        interactRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(interactRay, out RaycastHit hit, _interactRange))
        { 
            if (hit.collider.gameObject.TryGetComponent(out interactable))
            {
                interactable.OnClick();
            }
        }
    }

    private void ReleaseObject()
    {
        if (interactable != null)
        {
            interactable.OnRelease();
            interactable = null;
        }
    }

    public void GetWebbed(float t)
    {
        _webbedTimer = t;
    }

    private void EscapeWeb()
    {
        if (_webbedTimer > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float f = Random.Range(0f, 3f);
                _webbedTimer -= f;
            }
        }
    }

    public void GetBlurred(float t)
    {
        _blurredTimer = t;
    }
}