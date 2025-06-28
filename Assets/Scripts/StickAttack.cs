using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAttack : MonoBehaviour
{
    private float _stickCooldown = 0;
    [SerializeField] Collider stickCollider;
    [SerializeField] private float _maxCooldown;
    [SerializeField] private Transform _grabTransform;
    [SerializeField] private GameObject _meshObject;
    [SerializeField] private Animator _anim;

    private void OnEnable()
    {
        if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Stick) && PlayerActions.instance.selectedWeapon != PlayerActions.WeaponType.Stick)
        {
            PlayerActions.instance.ClickAction += Attack;
            PlayerActions.instance.selectedWeapon = PlayerActions.WeaponType.Stick;
        }
    }

    void Update()
    {
        transform.position = _grabTransform.position;
        transform.rotation = _grabTransform.rotation;
        if (_stickCooldown > 0) _stickCooldown -= Time.deltaTime;
        else if (_stickCooldown < 0)
        {
            _stickCooldown = 0;
            PlayerActions.instance.availableWeapons[PlayerActions.WeaponType.Stick] = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Stick) && PlayerActions.instance.selectedWeapon != PlayerActions.WeaponType.Stick)
            {
                PlayerActions.instance.ClickAction += Attack;
                PlayerActions.instance.selectedWeapon = PlayerActions.WeaponType.Stick;
                _meshObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Frog))
            {
                PlayerActions.instance.ClickAction -= Attack;
                _meshObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            PlayerActions.instance.ClickAction -= Attack;
            _meshObject.SetActive(false);
        }
    }

    private void Attack()
    {
        if (PlayerActions.instance.webbedTimer > 0 || !PlayerActions.instance.availableWeapons[PlayerActions.WeaponType.Stick]) return;
        _anim.SetTrigger("Hit");
        stickCollider.enabled = true;
        _stickCooldown = _maxCooldown;
        PlayerActions.instance.availableWeapons[PlayerActions.WeaponType.Stick] = false;
        Invoke("StickEnd", 0.5f);

    }
    private void StickEnd()
    {
        stickCollider.enabled = false;
    }

}
