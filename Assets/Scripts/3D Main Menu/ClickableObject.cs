using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// Used only in the MainMenu_3D scene.

public class ClickableObject : MonoBehaviour
{
    public UnityEvent ActivateSelectedFunction;
    private void OnMouseDown() => ActivateSelectedFunction.Invoke();

    #region MAIN MENU FUNCTIONS
    public void Play()
    {
        Debug.Log("Play");
    }

    public void HowToPlay()
    {
        Debug.Log("How to play");
    }

    public void Credits()
    {

    }

    public void Quit()
    {

    }
    #endregion

    public void Back()
    {

    }
}
