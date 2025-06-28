using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{
    private float _frogCooldown = 0;
    private Ray _frogRay;
    [SerializeField] private float _frogRayLength, _maxCooldown;
    [SerializeField] private Transform _grabTransform;
    [SerializeField] private GameObject _meshObject;
    [SerializeField] private LineRenderer _tongue;
    [SerializeField] private Transform _tonguePoint;
    [SerializeField] private Vector3 _tongueEnd;
    [SerializeField] private LayerMask _layerMask;
    private Coroutine _tongueCoroutine;

    private void OnEnable()
    {
        if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Frog) && PlayerActions.instance.selectedWeapon != PlayerActions.WeaponType.Frog)
        {
            PlayerActions.instance.ClickAction += Attack;
            PlayerActions.instance.selectedWeapon = PlayerActions.WeaponType.Frog;
            _meshObject.SetActive(true);
        }
    }

    void Update()
    {

        transform.position = _grabTransform.position;
        transform.rotation = _grabTransform.rotation;
        if (_frogCooldown > 0) _frogCooldown -= Time.deltaTime;
        else if (_frogCooldown < 0)
        {
            _frogCooldown = 0;
            PlayerActions.instance.availableWeapons[PlayerActions.WeaponType.Frog] = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Frog) && PlayerActions.instance.selectedWeapon != PlayerActions.WeaponType.Frog)
            {
                PlayerActions.instance.ClickAction += Attack;
                PlayerActions.instance.selectedWeapon = PlayerActions.WeaponType.Frog;
                _meshObject.SetActive(true);
            }
        } 
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Stick))
            {
                PlayerActions.instance.ClickAction -= Attack;
                _meshObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                _meshObject.SetActive(false);
                PlayerActions.instance.ClickAction -= Attack;
        }
    }

    private IEnumerator Tongue()
    {
        LineRenderer tongueNow = Instantiate(_tongue, _tonguePoint);
        EnemyBase enemy = null;
        float blend = 0;


        while (blend < 1)
        {
            tongueNow.SetPosition(0, _tonguePoint.position);
            tongueNow.SetPosition(1, Vector3.Lerp(_tonguePoint.position, _tongueEnd, blend));
            blend += Time.deltaTime * 5;
            yield return null;
        }

        tongueNow.SetPosition(1, _tongueEnd);
        _frogRay = new Ray(_tonguePoint.position, Camera.main.transform.forward);


        if (Physics.Raycast(_frogRay, out RaycastHit hit, _frogRayLength, _layerMask))
        {
            if (hit.collider.gameObject.TryGetComponent(out enemy))
            {
                enemy.PermaStun();
                foreach (Collider c in enemy.GetComponents<Collider>())
                {
                    c.enabled = false;
                }
            }
        }

        blend = 1;

        while (blend > 0)
        {
            
            tongueNow.SetPosition(0, _tonguePoint.position);
            tongueNow.SetPosition(1, Vector3.Lerp(_tonguePoint.position, _tongueEnd, blend));
            if(enemy != null) enemy.transform.position = Vector3.Lerp(_tonguePoint.position, _tongueEnd, blend);
            blend -= Time.deltaTime * 5;
            yield return null;
        }

        if(enemy != null)
        {
            _frogCooldown = _maxCooldown;
            PlayerActions.instance.availableWeapons[PlayerActions.WeaponType.Frog] = false;
            enemy.Die();
            StartCoroutine(Chew());
        }

        Destroy(tongueNow.gameObject);
        _tongueCoroutine = null;
    }

    private void Attack()
    {
        if (PlayerActions.instance.webbedTimer > 0 || !PlayerActions.instance.availableWeapons[PlayerActions.WeaponType.Frog]) return;
        if (_tongueCoroutine == null)
        {
            _frogRay = new Ray(_tonguePoint.position, Camera.main.transform.forward);
            if (Physics.Raycast(_frogRay, out RaycastHit hit, _frogRayLength, _layerMask))
            {
                _tongueEnd = hit.point;
            }
            else
            {
                _tongueEnd = _tonguePoint.position + _frogRay.direction.normalized * _frogRayLength;
            }
            _tongueCoroutine = StartCoroutine(Tongue());
        }
    }

    private IEnumerator Chew()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 shortScale = new Vector3(initialScale.x * 1.33f, initialScale.y * 0.5f, initialScale.z * 1.33f);
        Vector3 tallScale = new Vector3(initialScale.x * 0.75f, initialScale.y * 1.5f, initialScale.z * 0.75f);
        float t = 0;
        while (t < 1)
        {
            transform.localScale = Vector3.Lerp(initialScale, shortScale, t);
            t += Time.deltaTime * 2;
            yield return null;
        }
        transform.localScale = shortScale;
        for (int i = 0; i < _maxCooldown * 2; i++)
        {
            t = 0;
            while (t < 1)
            {
                transform.localScale = i%2 == 0? Vector3.Lerp(shortScale, tallScale, t) : Vector3.Lerp(tallScale, shortScale, t);
                t += Time.deltaTime * 2;
                yield return null;
            }
        }
        t = 0;
        while (t < 1)
        {
            transform.localScale = Vector3.Lerp(shortScale, initialScale, t);
            t += Time.deltaTime * 2;
            yield return null;
        }
        transform.localScale = initialScale;
    }

}
