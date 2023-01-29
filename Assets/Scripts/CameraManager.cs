using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform posA;
    public Transform posB;

    [Header("Cart colors")]
    public Color cart1color;
    public Color cart2color;
    public Color cart3color;

    private void Start()
    {
        /*transform.SetParent(posA);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);*/
        InputManager.tabInput += ChangeCamPosition;
    }

    private void ChangeCamPosition()
    {
        if (transform.parent == posA)
            transform.SetParent(posB);
        else
            transform.SetParent(posA);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
