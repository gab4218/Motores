using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Gabriel Benes
public class CheckButton : MonoBehaviour, IInteractable
{
    public static event Action onPress;

    public void OnClick()
    {
        onPress?.Invoke();
    }

    public void OnRelease()
    {
        return;
    }
}
