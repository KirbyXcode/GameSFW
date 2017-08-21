using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIRoleInfo : UIScene {

	private UISceneWidget mButton_Creat;	//创建按钮
	private UILabel mLabel_Name;	//名字
	private UILabel mLabel_Weapon;	//武器
	private UILabel mLabel_Job;		//职业
	private UILabel mLabel_Des;		//职业介绍

	protected override void Start () {
		base.Start();
		InitWidget();
		SetJobInfo("2");
	}

	private void InitWidget()
	{
		Debug.Log("UIRoleInfo");
		mButton_Creat = GetWidget("Button_Creat");
		if(mButton_Creat != null)
			mButton_Creat.OnMouseClick = this.ButtonCreatOnClick;
		mLabel_Name = 
			Global.FindChild(transform, "Label_RoleName").GetComponent<UILabel>();
		mLabel_Weapon = 
			Global.FindChild(transform, "Label_Weapon").GetComponent<UILabel>();
		mLabel_Job = 
			Global.FindChild(transform, "Label_Job").GetComponent<UILabel>();
		mLabel_Des = 
			Global.FindChild(transform, "Label_Des").GetComponent<UILabel>();
	}

	private void ButtonCreatOnClick(UISceneWidget eventObj)
	{
		Debug.Log("创建角色！");

		if(GameMain.Instance.isOnline)
		{
			StartCoroutine(CreatRole());
		}
		else
		{
			OperatingDB.Instance.FindJobDB(CharacterTemplate.Instance.jobId);
		}
	}

	public void SetJobInfo(string jobId)
	{
		if(mLabel_Name != null)
			mLabel_Name.text = CharacterTemplate.Instance.name;
		if(mLabel_Job != null)
			mLabel_Job.text = Configuration.GetContent("Job","Job" + jobId);
		if(mLabel_Weapon != null)
			mLabel_Weapon.text = Configuration.GetContent("Job","Weapon" + jobId);
		if(mLabel_Des != null)
			mLabel_Des.text = Configuration.GetContent("Job","Description" + jobId);
		CharacterTemplate.Instance.jobId = int.Parse(jobId);
	}

	IEnumerator CreatRole()
	{
		string createRoleurl = GameMain.createRoleurl + "?AccountID=" + 
			CharacterTemplate.Instance.accountId + 
				"&JobId=" + CharacterTemplate.Instance.jobId;
		Debug.Log("createRoleurl:" + createRoleurl);
		WWW www = new WWW (createRoleurl);
		yield return www;
		if(www.error == null)
		{
			if(www.text == "ok")
			{
				string uiscene = Configuration.GetContent("Scene","LoadUIMainCity");
				string scene = Configuration.GetContent("Scene","LoadMainCity");
				StartCoroutine(GameMain.Instance.LoadScene(uiscene, scene));
			}
			else
			{
				Debug.Log("创建角色失败！");
			}
		}
		else
		{
			Debug.Log(www.error);
		}
	}
}
