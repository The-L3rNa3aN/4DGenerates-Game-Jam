using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Inputs")]
    public static Action escInput;
    public static Action tabInput;
    public static Action num1Input;
    public static Action num2Input;
    public static Action num3Input;
    public static Action lmbInput;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            escInput?.Invoke();

        if (Input.GetKeyDown(KeyCode.Tab) && !GameManager.instance.isPaused)
            tabInput?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha1) && !GameManager.instance.isPaused)
            num1Input?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha2) && !GameManager.instance.isPaused)
            num2Input?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha3) && !GameManager.instance.isPaused)
            num3Input?.Invoke();

        if (Input.GetMouseButtonDown(0) && !GameManager.instance.isPaused)
            lmbInput?.Invoke();
    }
}
