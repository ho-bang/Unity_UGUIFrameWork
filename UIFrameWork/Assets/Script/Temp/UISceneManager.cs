using CJR.UI;
using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        UIManager.Open(canvas.gameObject, "TempUIPrefabs/TempUI");
        var element1 = UIManager.Open(canvas.gameObject, "TempUIPrefabs/TempUI");
        
        // temp
        var ap = element1.GetComponent<RectTransform>().anchoredPosition;
        element1.GetComponent<RectTransform>().anchoredPosition = new Vector2(ap.x + 100, ap.y);
    }
}