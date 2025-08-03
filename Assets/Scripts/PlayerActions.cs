using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
// Andrea Ferruelo
public class PlayerActions : MonoBehaviour
{
    public static PlayerActions instance { get; private set; }
    public Player player;
    public Transform holdTransform;
    public Transform heldItemTransform;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedSprint;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundRayLength;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private Grab _grab;
    [SerializeField] private FrogAttack _frog;
    [SerializeField] private StickAttack _stick;
    private float _blurredTimer;
    private PostProcessVolume _ppVolume;
    private DepthOfField _blur;
    public Action spacebarAction;
    public Action ClickAction;
    public bool webbed = false;




    public enum WeaponType
    {
        Hand, 
        Stick,
        Frog
    }
    public Dictionary<WeaponType, bool> availableWeapons = new Dictionary<WeaponType, bool>();
    public WeaponType selectedWeapon;


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
        availableWeapons.Add(WeaponType.Hand, true);
        player = new Player(_speed, _speedSprint, _jumpForce, _groundRayLength, _groundLayerMask, GetComponent<Rigidbody>(), transform);
        player.OnAwake();
        _ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        _ppVolume.profile.TryGetSettings(out _blur);
        _blur.active = false;
    }

    private void Update()
    {
        if (CameraScript.instance.isOnBoard) return;
        

        player.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacebarAction();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !webbed)
        {
            if(ClickAction != null) ClickAction();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _grab.Select();
            _frog.Deselect();
            _stick.Deselect();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _stick.Select();
            _frog.Deselect();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _frog.Select();
            _stick.Deselect();
        }


        if (_blurredTimer > 0)
        {
            _blurredTimer -= Time.deltaTime;
            if (_blur.active == false) _blur.active = true;
        }
        else if(_blurredTimer < 0)
        {
            _blurredTimer = 0;
            _blur.active = false;
        }
    }

    private void FixedUpdate()
    {
        if (CameraScript.instance.isOnBoard) return;
        if (webbed) return;
        player.OnFixedUpdate();
    }

    

    public void GetBlurred(float t)
    {
        _blurredTimer = t;
    }
}