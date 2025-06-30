using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
// Gabriel Benes
public class Grab : MonoBehaviour
{
    [SerializeField] private float _interactRange;
    private Ray interactRay;
    private IInteractable interactable;
    private void Start()
    {
        PlayerActions.instance.selectedWeapon = PlayerActions.WeaponType.Hand;
        PlayerActions.instance.ClickAction += GrabObject;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PlayerActions.instance.selectedWeapon != PlayerActions.WeaponType.Hand)
            {
                PlayerActions.instance.ClickAction += GrabObject;
                PlayerActions.instance.selectedWeapon = PlayerActions.WeaponType.Hand;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Stick))
            {
                PlayerActions.instance.ClickAction -= GrabObject;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerActions.instance.availableWeapons.ContainsKey(PlayerActions.WeaponType.Frog))
            {
                PlayerActions.instance.ClickAction -= GrabObject;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseObject();
        }

    }
    
    private void GrabObject()
    {
        if (PlayerActions.instance.webbedTimer > 0) return;
        interactRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(interactRay, out RaycastHit hit, _interactRange))
        {
            if (hit.collider.gameObject.TryGetComponent(out interactable))
            {
                interactable.OnClick();
                if(hit.collider.GetComponent<WeaponItem>()) PlayerActions.instance.ClickAction -= GrabObject;
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
}
