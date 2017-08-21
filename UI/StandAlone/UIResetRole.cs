using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIResetRole : UIScene {

	private UISceneWidget mButton_Sure;
	private UISceneWidget mButton_Canel;
	private UILabel mLabel_Message;

	protected override void Start () {
		base.Start();
		mButton_Sure = GetWidget("Button_Sure");
		if(mButton_Sure != null)
			mButton_Sure.OnMouseClick = this.ButtonSureOnClick;
		mButton_Canel = GetWidget("Button_Cancel");
		if(mButton_Canel != null)
			mButton_Canel.OnMouseClick = this.ButtonCancelOnClick;
		mLabel_Message = 
			Global.FindChild(transform,"Label_Message").GetComponent<UILabel>();
		if(mLabel_Message != null)
			mLabel_Message.text = Configuration.GetContent("StandAlone","Cover");
	}

	private void ButtonSureOnClick(UISceneWidget eventObj)
	{
//		SetVisible(false);
		OperatingDB.Instance.DeleteRoloInfoDB();
	}

	private void ButtonCancelOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}
	

}
