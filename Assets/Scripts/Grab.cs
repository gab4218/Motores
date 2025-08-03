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
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseObject();
        }
    }
    
    public void GrabObject()
    {
        if (PlayerActions.instance.webbed) return;
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

    public void Select()
    {
        if (PlayerActions.instance.selectedWeapon != PlayerActions.WeaponType.Hand)
        {
            PlayerActions.instance.ClickAction = GrabObject;
            PlayerActions.instance.selectedWeapon = PlayerActions.WeaponType.Hand;
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
