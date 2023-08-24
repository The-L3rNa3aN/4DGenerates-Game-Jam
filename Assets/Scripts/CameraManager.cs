using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public bool ifInitialRot;
    public Image blackPanel;

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
        StartCoroutine(Fade());
        /*if(ifInitialRot)
        {
            transform.rotation = Quaternion.Euler(new Vector3(45.62f, 313.46f, 0f));
            ifInitialRot = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(45.62f, 46.54f, 0f));
            ifInitialRot = true;
        }*/
    }

    private IEnumerator Fade()
    {
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime * 5f;
            if(t > 1f) t = 1f;

            blackPanel.color = Color.Lerp(blackPanel.color, new Color(0, 0, 0, 1), t);
            yield return new WaitForEndOfFrame();
        }

        if (ifInitialRot)
        {
            transform.rotation = Quaternion.Euler(new Vector3(45.62f, 313.46f, 0f));
            ifInitialRot = false;
            GameManager.instance.parent_1.SetActive(false);
            GameManager.instance.parent_2.SetActive(true);
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(45.62f, 46.54f, 0f));
            ifInitialRot = true;
            GameManager.instance.parent_1.SetActive(true);
            GameManager.instance.parent_2.SetActive(false);
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            if (t > 1f) t = 1f;

            blackPanel.color = Color.Lerp(blackPanel.color, new Color(0, 0, 0, 0), t);
            yield return new WaitForEndOfFrame();
        }
    }
}
