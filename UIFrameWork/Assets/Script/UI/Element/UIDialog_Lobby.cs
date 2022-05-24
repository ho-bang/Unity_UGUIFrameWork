using CJR.Scene;
using CJR.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDialog_Lobby : UIDialog
{
    public Image Image;

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

                SceneLoader.Instance.UnloadAllOpenedScene(onComplete: () =>
                {
                    SceneLoader.Instance.LoadScene(sceneName: SceneNames.main, loadType: LoadSceneMode.Additive, onComplete: null);
                });
                break;
        }
    }
}