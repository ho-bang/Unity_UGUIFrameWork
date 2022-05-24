using CJR.GameManager;
using CJR.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog_Lobby : UIDialog
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
        UIManager.SendMessageToOpenList(new UIMessage_EnterGameScene(type: MessageType.Type.ClickTab));
    }

    public override void ReceiveMessage(IUIMessage type)
    {
        switch (type.Type)
        {
            case MessageType.Type.ClickTab when type is UIMessage_EnterGameScene ClickTempSlot:
                Debug.Log($"message Type : {ClickTempSlot.Type} _ {this.name}");
                UIManager.Close(this);
                GameManager.Instance.ChangeToMain();
                break;
        }
    }
}