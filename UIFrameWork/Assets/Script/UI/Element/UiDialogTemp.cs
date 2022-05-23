using CJR.UI;
using UnityEngine;
using UnityEngine.UI;

public class UiDialogTemp : UIDialog
{
    public Image Image;

    public void OnClick()
    {
        UIManager.SendMessageToOpenList(new UIMessage_ClickTempSlot(type: MessageType.Type.ClickTab));
        UIManager.Close(this);
    }

    public override void ReceiveMessage(IUIMessage type)
    {
        switch (type.Type)
        {
            case MessageType.Type.ClickTab when type is UIMessage_ClickTempSlot ClickTempSlot:
                Debug.Log($"message Type : {ClickTempSlot.Type} _ {this.name}");
                break;
            case MessageType.Type.Drag:
                Debug.Log("MessageType.Type.Drag");
                break;
        }
    }
}