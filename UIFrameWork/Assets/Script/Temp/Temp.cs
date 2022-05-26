using CJR.UI;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        var element= UIManager.Open(canvas.gameObject, "UI/Lobby/TempLobby");

        //UIManager.Open(canvas.gameObject, "TempUIPrefabs/TempUI");
        //var element1 = UIManager.Open(canvas.gameObject, "TempUIPrefabs/TempUI");

        //// temp
        //var ap = element1.GetComponent<RectTransform>().anchoredPosition;
        //element1.GetComponent<RectTransform>().anchoredPosition = new Vector2(ap.x + 100, ap.y);
    }
}