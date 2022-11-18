using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform posA;
    public Transform posB;
    private GameManager gameManager;

    private void Start()
    {
        transform.SetParent(posA);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        InputManager.tabInput += ChangeCamPosition;
        gameManager = FindObjectOfType<GameManager>();
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
