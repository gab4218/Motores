using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions instance { get; private set; }
    public Player player;
    public Transform holdTransform;
    public Transform heldItemTransform;
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
    private float _frogCooldown;
    private float _stickCooldown;
    private float _webbedTimer;
    private float _blurredTimer;
    private Ray frogRay;
    private Ray interactRay;
    private IInteractable interactable;
    private PostProcessVolume ppVolume;
    private Action ClickAction;

    public enum WeaponType
    {
        Hand, 
        Stick,
        Frog
    }
    public Dictionary<WeaponType, bool> availableWeapons = new Dictionary<WeaponType, bool>();
    private WeaponType selectedWeapon;


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
        ClickAction = GrabObject;
        availableWeapons.Add(WeaponType.Hand, true);
        player = new Player(speed, speedSprint, jumpForce, groundRayLength, groundLayerMask, GetComponent<Rigidbody>(), transform);
        player.OnAwake();
        _frogCooldown = 0;
        _stickCooldown = 0;
        stickCollider.enabled = false;
        ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        ppVolume.enabled = false;
    }

    private void Update()
    {
        if (CameraScript.instance.isOnBoard) return;
        player.OnUpdate();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(ClickAction != null) ClickAction();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseObject();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) ClickAction = GrabObject;
        if (Input.GetKeyDown(KeyCode.Alpha2) && availableWeapons.ContainsKey(WeaponType.Stick)) ClickAction = StickAttack;
        if (Input.GetKeyDown(KeyCode.Alpha3) && availableWeapons.ContainsKey(WeaponType.Frog)) ClickAction = FrogAttack;

        if (_frogCooldown > 0) _frogCooldown -= Time.deltaTime;
        else if (_frogCooldown < 0)
        {
            _frogCooldown = 0;
            availableWeapons[WeaponType.Frog] = true;
        }

        if (_stickCooldown > 0) _stickCooldown -= Time.deltaTime;
        else if (_stickCooldown < 0)
        {
            _stickCooldown = 0;
            availableWeapons[WeaponType.Stick] = true;
        }

        if (_webbedTimer > 0) _webbedTimer -= Time.deltaTime;
        else if (_webbedTimer < 0) _webbedTimer = 0;

        if (_blurredTimer > 0)
        {
            _blurredTimer -= Time.deltaTime;
            if (ppVolume.enabled == false) ppVolume.enabled = true;
        }
        else if(_blurredTimer < 0)
        {
            _blurredTimer = 0;
            ppVolume.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if(_webbedTimer <= 0) player.OnFixedUpdate();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        EnemyBase enemy = other.GetComponentInParent<EnemyBase>();
        if (enemy != null && stickCollider.enabled)
        {
            enemy.DamageEnemy(_dmg, _knockback);
        }
    }

    private void StickAttack()
    {
        if (_webbedTimer > 0 || !availableWeapons[WeaponType.Stick]) return;

        stickCollider.enabled = true;
        _stickCooldown = 1;
        availableWeapons[WeaponType.Stick] = false;
        Invoke("StickEnd", 0.5f);
        
    }
    private void StickEnd()
    {
        stickCollider.enabled = false;
    }

    private void FrogAttack()
    {
        if (_webbedTimer > 0 || !availableWeapons[WeaponType.Frog]) return;
        if (Physics.Raycast(frogRay, out RaycastHit hit, frogRayLength))
        {
            if (hit.collider.gameObject.TryGetComponent(out EnemyBase enemy))
            {
                enemy.Die();
                _frogCooldown = 10;
                availableWeapons[WeaponType.Frog] = false;
            }
        }
    }


    private void GrabObject()
    {
        if (_webbedTimer > 0) return;
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
                float f = UnityEngine.Random.Range(0f, 3f);
                _webbedTimer -= f;
            }
        }
    }

    public void GetBlurred(float t)
    {
        _blurredTimer = t;
    }
}