using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDCameraManager : MonoBehaviour
{
    public static ThreeDCameraManager instance { get; private set; }

    [Header("Camera Positions")]
    public Transform menu;
    public Transform levelSelect;
    public Transform howToPlay;
    public Transform credits;
    public Transform quit;

    [Header("UI References")]
    public Image img;

    [Header("Level Loader")]
    public LoadingScreen loadingScreen;

    private void Awake()
    {
        //Singleton.
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void MoveCamera(Vector3 start, Vector3 finish) => StartCoroutine(CoMoveCamera(start, finish));

    private IEnumerator CoMoveCamera(Vector3 start, Vector3 finish)
    {
        float t = 0f;

        while(t < 1f)
        {
            t += Time.deltaTime;
            if (t > 1f) t = 1;

            transform.position = Vector3.Lerp(start, finish, t);
            yield return new WaitForEndOfFrame(); //yield return null;
        }
    }
}
