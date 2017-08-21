using UnityEngine;
using System.Collections;

public class UIStandAlone : UIScene {

	private UISceneWidget mButton_Start;
	private UISceneWidget mButton_Continue;

	protected override void Start () {
		base.Start();
		mButton_Start = GetWidget("Button_Start");
		if(mButton_Start != null)
			mButton_Start.OnMouseClick = this.ButtonStartOnClick;
		mButton_Continue = GetWidget("Button_Continue");
		if(mButton_Continue != null)
			mButton_Continue.OnMouseClick = this.ButtonContinueOnClick;
	}

	private void ButtonStartOnClick(UISceneWidget eventObj)
	{
		Debug.Log("开始游戏！");
//		UIManager.Instance.SetVisible(UIName.UIRoleName, true);
//		UIManager.Instance.SetVisible(UIName.UIResetRole, true);
		OperatingDB.Instance.GetRoleInfoDB();
	}

	private void ButtonContinueOnClick(UISceneWidget eventObj)
	{
		Debug.Log("继续游戏！");
//		UIManager.Instance.SetVisible(UIName.UIMessageBox, true);
		OperatingDB.Instance.FindRoleInfoDB();
	}
	

}
