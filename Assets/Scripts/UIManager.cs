using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void PlayButton()
    {
        //For now.
        SceneManager.LoadScene("SampleScene");
    }

    public void LevelSelectButton() => SceneManager.LoadScene("LevelSelect");

    public void QuitGame() => Application.Quit();
}
