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
    [SerializeField] private float _speed;
    [SerializeField] private float _speedSprint;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundRayLength;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private GameObject _webbedImage;
    [SerializeField] private RectTransform _webbedBar;
    public float webbedTimer;
    private float _blurredTimer;
    private PostProcessVolume _ppVolume;
    public event Action ClickAction;
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
        _ppVolume.enabled = false;
    }

    private void Update()
    {
        if (CameraScript.instance.isOnBoard) return;
        if (webbedTimer > 0)
        {
            EscapeWeb();
            webbedTimer -= Time.deltaTime;
            _webbedBar.sizeDelta = new Vector2(840f * webbedTimer / 30f, 68);
            return;
        }
        else if (webbedTimer < 0)
        {
            webbedTimer = 0;
            _webbedImage.SetActive(false);
        }

        player.OnUpdate();


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(ClickAction != null) ClickAction();
        }
        

        if (_blurredTimer > 0)
        {
            _blurredTimer -= Time.deltaTime;
            if (_ppVolume.enabled == false) _ppVolume.enabled = true;
        }
        else if(_blurredTimer < 0)
        {
            _blurredTimer = 0;
            _ppVolume.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if(webbedTimer <= 0) player.OnFixedUpdate();
    }

    public void GetWebbed(float t)
    {
        webbedTimer = t;
        _webbedImage.SetActive(true);
    }

    private void EscapeWeb()
    {
        if (webbedTimer > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float f = UnityEngine.Random.Range(0f, 3f);
                webbedTimer -= f;
            }
        }
    }

    public void GetBlurred(float t)
    {
        _blurredTimer = t;
    }
}