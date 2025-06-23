using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButton : MonoBehaviour, IInteractable
{
    public static event Action onPress;

    public void OnClick()
    {
        onPress.Invoke();
    }

    public void OnRelease()
    {
        return;
    }
}
