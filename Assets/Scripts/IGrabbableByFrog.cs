using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Gabriel Benes
public interface IGrabbableByFrog
{

    public void Grab();
    public Collider[] GetColliders();
    public Transform GetTransform();
    public void End();
}
