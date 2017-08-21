using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UISelectRole : UIScene {

//	private UISceneWidget mButton_Master;		//法师
//	private UISceneWidget mButton_Warrior;		//战士
	UIRoleInfo uiRoleInfo;
	private UISceneWidget[] mButton;

	protected override void Start () {
		base.Start();
		mButton = GetComponentsInChildren<UISceneWidget>();
		Debug.Log(mButton.Length);
		InitWidget();
	}

//	private void InitWidget()
//	{
//		Debug.Log("UISelectRole");
//		uiRoleInfo = UIManager.Instance.GetUI<UIRoleInfo>(UIName.UIRoleInfo);
//		mButton_Master = GetWidget("Button_Job2");
//		if(mButton_Master != null)
//			mButton_Master.OnMouseClick = this.ButtonJobOnClick;
//		mButton_Warrior = GetWidget("Button_Job1");
//		if(mButton_Warrior != null)
//			mButton_Warrior.OnMouseClick = this.ButtonJobOnClick;
//	}

//	private void ButtonMasterOnClick(UISceneWidget eventObj)
//	{
//		Debug.Log("eventObj:" + eventObj);
//		Debug.Log("法师！");
//		Debug.Log("uiRoleInfo:" + uiRoleInfo);
//		if(uiRoleInfo != null)
//			uiRoleInfo.SetJobInfo("2");
//	}
//
//	private void ButtonWarriorOnClick(UISceneWidget eventObj)
//	{
//		Debug.Log("战士！");
//		Debug.Log("uiRoleInfo:" + uiRoleInfo);
//		if(uiRoleInfo != null)
//			uiRoleInfo.SetJobInfo("1");
//	}

	private void InitWidget()
	{
		string jobid;
		uiRoleInfo = UIManager.Instance.GetUI<UIRoleInfo>(UIName.UIRoleInfo);
		for(int i = 0; i < mButton.Length; i++)
		{
//			jobid = (i+1).ToString();
//			if(i < 10)
//				jobid = "0" + jobid;
			UISceneWidget mButton_Job = GetWidget("Button_Job" + (i+1));
			if(mButton_Job != null)
				mButton_Job.OnMouseClick = this.ButtonJobOnClick;
		}
	}

	private void ButtonJobOnClick(UISceneWidget eventObj)
	{
		string jobid = eventObj.name;
		jobid = jobid.Substring(jobid.Length - 1);
//		Debug.Log("jobid:" + jobid);
		if(uiRoleInfo != null)
			uiRoleInfo.SetJobInfo(jobid);
	}

}
