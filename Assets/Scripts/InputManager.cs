using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Inputs")]
    public static Action escInput;
    public static Action tabInput;
    public static Action num1Input;
    public static Action num2Input;
    public static Action num3Input;
    public static Action lmbInput;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            escInput?.Invoke();

        if (Input.GetKeyDown(KeyCode.Tab) && !gameManager.isPaused)
            tabInput?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha1) && !gameManager.isPaused)
            num1Input?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha2) && !gameManager.isPaused)
            num2Input?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha3) && !gameManager.isPaused)
            num3Input?.Invoke();

        if (Input.GetMouseButtonDown(0) && !gameManager.isPaused)
            lmbInput?.Invoke();
    }
}
