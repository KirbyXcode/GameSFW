using UnityEngine;
using System.Collections;
using DevelopEngine;
using LitJson;

public class UILogin : UIScene {

	private UISceneWidget mButton_Login;
	private UISceneWidget mButton_Exit;
	private UIInput mInput_Account;
	private UIInput mInput_PassWord;
	string accout;
	string password;

	protected override void Start () {
		base.Start();
		mButton_Login = GetWidget("Button_Login");
		if(mButton_Login != null)
			mButton_Login.OnMouseClick = this.ButtonLoginOnClick;
		mButton_Exit = GetWidget("Button_Exit");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mInput_Account = 
			Global.FindChild(transform,"Input_Account").GetComponent<UIInput>();
		mInput_PassWord = 
			Global.FindChild(transform,"Input_Password").GetComponent<UIInput>();
	}

	private void ButtonLoginOnClick(UISceneWidget eventObj)
	{
		Debug.Log("登录帐号！");
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
				Debug.Log("向服务器发送登录请求");
				accout = mInput_Account.value;
				password = mInput_PassWord.value;
				StartCoroutine(Login());
			}
		}
	}

	IEnumerator Login()
	{
		string logingurl = 
			GameMain.loginurl + "?Account=" + accout + "&password=" + password;
		Debug.Log("logingurl:" + logingurl);
		WWW www = new WWW (logingurl);
		yield return www;
		if(www.error == null)
		{
			Debug.Log(www.text); 
			JsonData json = JsonMapper.ToObject(www.text);
			if(json.IsArray)
			{
				//已经存在角色
				CharacterTemplate.Instance.characterId = int.Parse(json[0]["CharacterID"].ToString());
				CharacterTemplate.Instance.jobId = int.Parse(json[0]["JobID"].ToString());
				CharacterTemplate.Instance.lv = int.Parse(json[0]["Lv"].ToString());
				CharacterTemplate.Instance.expCur = int.Parse(json[0]["ExpCur"].ToString());
				CharacterTemplate.Instance.force = int.Parse(json[0]["Force"].ToString());
				CharacterTemplate.Instance.intellect = int.Parse(json[0]["Intellect"].ToString());
//				CharacterTemplate.Instance.attackSpeed = (int)float.Parse(json[0]["AttackSpeed"].ToString());
				CharacterTemplate.Instance.maxHp = int.Parse(json[0]["MaxHP"].ToString());
				CharacterTemplate.Instance.maxMp = int.Parse(json[0]["MaxMP"].ToString());
				CharacterTemplate.Instance.damageMax = int.Parse(json[0]["DamageMax"].ToString());
				CharacterTemplate.Instance.name = accout;
				string uiscene = Configuration.GetContent("Scene","LoadUIMainCity");
				string scene = Configuration.GetContent("Scene","LoadMainCity");
				StartCoroutine(GameMain.Instance.LoadScene(uiscene, scene));

			}
			else
			{
				//没有角色信息
				CharacterTemplate.Instance.accountId = int.Parse(json["AccountID"].ToString());
				CharacterTemplate.Instance.name = accout;
				string uiscene = Configuration.GetContent("Scene","LoadCreatRole");
				StartCoroutine(GameMain.Instance.LoadScene(uiscene));
			}

		}
		else
		{
			Debug.Log(www.error);
			UIManager.Instance.SetVisible(UIName.UIMessage, true);
			UIMessage.instance.SetLabel(Configuration.GetContent("Login","LoginFaild"));
		}
	}
	
	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		Debug.Log("关闭！");
		SetVisible(false);
	}

}
