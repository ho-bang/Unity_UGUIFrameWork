namespace CJR.UI
{
    public class MessageType
    {
        public enum Type
        {
            ClickTab,
            Drag
        }
    }

    public interface IUIMessage
    {
        MessageType.Type Type { get; }
    }

    public class UIMessage_ClickTempSlot : IUIMessage
    {
        public MessageType.Type Type { private set; get; }
        public UIMessage_ClickTempSlot(MessageType.Type type)
        {
            Type = type;
        }
    }
}