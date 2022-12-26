using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    //public GameObject loadingScreen;
    //public Slider slider;
    public Transform loadingImage;

    public void LoadScene(int sceneId) => StartCoroutine(LoadSceneAsync(sceneId));

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        //loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Vector3 changeScale = new Vector3(progressValue, 0f, 0f);
            loadingImage.localScale = changeScale; //slider.value = progressValue;
            yield return null;
        }
    }
}
