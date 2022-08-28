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
        SceneManager.LoadScene("123");
    }

    public void CreditsButton() => SceneManager.LoadScene("Credits");

    public void HowToPlayButton() => SceneManager.LoadScene("HowToPlay");

    public void BackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame() => Application.Quit();
}
