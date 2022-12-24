using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider; //public Image loadingImage;

    public void LoadScene(int sceneId) => StartCoroutine(LoadSceneAsync(sceneId));

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progressValue; //loadingImage.fillAmount = progressValue;
            yield return null;
        }
    }
}
