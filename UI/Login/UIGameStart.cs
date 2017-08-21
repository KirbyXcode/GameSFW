using UnityEngine;
using System.Collections;

public class UIGameStart : UIScene {

	private UISceneWidget mButton_Login;
	private UISceneWidget mButton_Register;

	protected override void Start () {
		base.Start();
		mButton_Login = GetWidget("Button_Login");
		if(mButton_Login != null)
			mButton_Login.OnMouseClick = this.ButtonLoginOnClick;
		mButton_Register = GetWidget("Button_Register");
		if(mButton_Register != null)
			mButton_Register.OnMouseClick = this.ButtonRegisterOnClick;
	}

	private void ButtonLoginOnClick(UISceneWidget eventObj)
	{
		Debug.Log("登录游戏！");
		UIManager.Instance.SetVisible(UIName.UILogin, true);
	}

	private void ButtonRegisterOnClick(UISceneWidget eventObj)
	{
		Debug.Log("注册帐号！");
		UIManager.Instance.SetVisible(UIName.UIRegister, true);
	}

	

}
