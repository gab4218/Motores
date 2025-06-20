using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour, IInteractable
{

    [SerializeField] private PlayerActions.WeaponType type { get; }

    public void OnClick()
    {
        PlayerActions.instance.availableWeapons.Add(type, true);
        Destroy(gameObject);
    }

    public void OnRelease()
    {
        return;
    }
}
