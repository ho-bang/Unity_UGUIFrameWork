using Assets.UI.FrameWork;
using UnityEngine.UI;

namespace Assets.UI
{
    public class UIElement_Temp : UIElement
    {
        public Image Image;

        public void OnClick()
        {
            this.Close();
        }
    }
}
