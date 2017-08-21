using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIRegister : UIScene {

	private UISceneWidget mButton_Register;
	private UISceneWidget mButton_Exit;
	private UIInput mInput_Account;
	private UIInput mInput_PassWord;

	string accout;
	string password;

	protected override void Start () {
		base.Start();
		mButton_Register = GetWidget("Button_Register");
		if(mButton_Register != null)
			mButton_Register.OnMouseClick = this.ButtonRegisterOnClick;
		mButton_Exit = GetWidget("Button_Exit");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mInput_Account = 
			Global.FindChild(transform,"Input_Account").GetComponent<UIInput>();
		mInput_PassWord = 
			Global.FindChild(transform,"Input_Password").GetComponent<UIInput>();
	}

	private void ButtonRegisterOnClick(UISceneWidget eventObj)
	{
		Debug.Log("注册帐号！");
		if(mInput_Account != null && mInput_PassWord != null)
		{
			//判断帐号是否为空
			if(mInput_Account.value == string.Empty)
			{
				Debug.Log("帐号不能为空");
				UIManager.Instance.SetVisible(UIName.UIMessage, true);
				UIMessage.instance.SetLabel(Configuration.GetContent("Login","Account"));
			}
			else if(mInput_PassWord.value == string.Empty)
			{
				Debug.Log("密码不能为空");
				UIManager.Instance.SetVisible(UIName.UIMessage, true);
				UIMessage.instance.SetLabel(Configuration.GetContent("Login","Password"));
			}
			else
			{
				Debug.Log("向服务器发送注册请求");
				accout = mInput_Account.value;
				password = mInput_PassWord.value;
				StartCoroutine(Register());
			}
		}
	}

	//提交注册
	IEnumerator Register()
	{
		string registerurl = 
			GameMain.registerurl + "?Account=" + accout + "&password=" + password;
		Debug.Log("registerurl:" + registerurl);
		WWW www = new WWW (registerurl);
		yield return www;
		if(www.error == null)
		{
			if(www.text == "ok")
			{
				UIManager.Instance.SetVisible(UIName.UIMessage, true);
				UIMessage.instance.SetLabel(Configuration.GetContent("Login","Success"));
			}
			else
			{
				Debug.Log(www.text);
			}
		}
		else
		{
			Debug.Log(www.error);
			UIManager.Instance.SetVisible(UIName.UIMessage, true);
			UIMessage.instance.SetLabel(Configuration.GetContent("Login","Faild"));
		}
		SetVisible(false);
	}


	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		Debug.Log("关闭！");
		SetVisible(false);
	}

	

}
