using CJR.UI;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : CJRUIBase
{
    public Button StartButton;

    protected override void OnAwake()
    {
        StartButton = GetComponentInChildren<Button>(true);
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        UIManager.Close(this);
    }
}