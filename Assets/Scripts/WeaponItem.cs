using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Gabriel Benes
public class WeaponItem : MonoBehaviour, IInteractable
{

    [SerializeField] private PlayerActions.WeaponType _type;
    [SerializeField] private MonoBehaviour _weaponScript;
    [SerializeField] private Transform _finalPos;
    [SerializeField] private Collider _grabCollider;
    [SerializeField] private Vector3 _position;
    private bool _grabbed = false;

    private void Start()
    {
        _weaponScript.enabled = false;
    }

    public void OnClick()
    {
        if (!_grabbed)
        {
            _grabbed = true;
            if(!PlayerActions.instance.availableWeapons.ContainsKey(_type)) PlayerActions.instance.availableWeapons.Add(_type, true);
            StartCoroutine(Grab(transform.position, transform.rotation, transform.localScale));
            Destroy(_grabCollider);
        }
    }


    private IEnumerator Grab(Vector3 originalPos, Quaternion originalRotation, Vector3 originalScale)
    {
        float t = 0;
        while(t < 1)
        {
            transform.position = Vector3.Lerp(originalPos, _finalPos.position, t);
            transform.rotation = Quaternion.Lerp(originalRotation, _finalPos.rotation, t);
            transform.localScale = Vector3.Lerp(originalScale, _finalPos.localScale, t);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = _finalPos.position;
        transform.rotation = _finalPos.rotation;
        transform.localScale = _finalPos.localScale;
        _weaponScript.enabled = true;
        Destroy(this);
    }

    public void OnRelease()
    {
        return;
    }
}
