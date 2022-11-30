using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button thisButton;
    public Text gradeNotif;
    private string hoverTex;

    private void Start() => thisButton = GetComponent<Button>();

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        switch(thisButton.name)
        {
            case "button_day1":
                hoverTex = "Grade achieved: " + PlayerPrefs.GetString("day1_grade");
                break;

            case "button_day2":
                hoverTex = "Grade achieved: " + PlayerPrefs.GetString("day2_grade");
                break;

            case "button_day3":
                hoverTex = "Grade achieved: " + PlayerPrefs.GetString("day3_grade");
                break;
        }

        gradeNotif.gameObject.SetActive(true);
        gradeNotif.text = hoverTex;
    }

    public void OnPointerExit(PointerEventData pointerEventData) => gradeNotif.gameObject.SetActive(false);
}
