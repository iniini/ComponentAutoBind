namespace Game.UIManage
{
    public partial class TestViewManager : UIFormLogic
    {
        private UnityEngine.RectTransform BackGround;
        private UnityEngine.UI.Image BackGround_Img;
        private UnityEngine.UI.Image TestButton_Img;
        private UnityEngine.UI.Button TestButton_Btn;
        private UnityEngine.UI.Text Button_Txt_Txt;
        private UnityEngine.RectTransform Title;
        private UnityEngine.UI.Text Title_Txt;
//##INSERTPROPERTY##
        protected override void AutoBind()
        {
            BackGround = Content.Find("BackGround").GetComponent<UnityEngine.RectTransform>();
            BackGround_Img = Content.Find("BackGround").GetComponent<UnityEngine.UI.Image>();
            TestButton_Img = BackGround.transform.Find("TestButton").GetComponent<UnityEngine.UI.Image>();
            TestButton_Btn = BackGround.transform.Find("TestButton").GetComponent<UnityEngine.UI.Button>();
            Button_Txt_Txt = TestButton_Img.transform.Find("Button_Txt").GetComponent<UnityEngine.UI.Text>();
            Title = BackGround.transform.Find("Title").GetComponent<UnityEngine.RectTransform>();
            Title_Txt = BackGround.transform.Find("Title").GetComponent<UnityEngine.UI.Text>();
//##INSERTFUNCTION##
        }
    }
}
