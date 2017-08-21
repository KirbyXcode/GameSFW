using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIMessageBox : UIScene {

	private UISceneWidget mButton_Sure;
	private UILabel mLabel_Message;
	
	protected override void Start () {
		base.Start();
		mButton_Sure = GetWidget("Button_Sure");
		if(mButton_Sure != null)
			mButton_Sure.OnMouseClick = this.ButtonSureOnClick;
		mLabel_Message = 
			Global.FindChild(transform,"Label_Message").GetComponent<UILabel>();
		if(mLabel_Message != null)
			mLabel_Message.text = Configuration.GetContent("StandAlone","Archive");
	}
	
	private void ButtonSureOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}

}
