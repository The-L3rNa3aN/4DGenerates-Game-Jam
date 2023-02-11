using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool ifInitialRot;

    [Header("Cart colors")]
    public Color cart1color;
    public Color cart2color;
    public Color cart3color;

    private void Start()
    {
        InputManager.tabInput += ChangeCamPosition;
        ifInitialRot = true;
    }

    private void ChangeCamPosition()
    {
        if(ifInitialRot)
        {
            transform.rotation = Quaternion.Euler(new Vector3(45.62f, 313.46f, 0f));
            ifInitialRot = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(45.62f, 46.54f, 0f));
            ifInitialRot = true;
        }
    }
}
