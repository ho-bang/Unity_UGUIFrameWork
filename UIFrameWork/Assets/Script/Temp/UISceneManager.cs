using Assets.UI.FrameWork;
using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        var element = UIManager.Open(canvas.gameObject, "TempUI");
        var element1 = UIManager.Open(canvas.gameObject, "TempUI");
        
        // temp
        var ap = element1.GetComponent<RectTransform>().anchoredPosition;
        element1.GetComponent<RectTransform>().anchoredPosition = new Vector2(ap.x + 100, ap.y);
    }
}