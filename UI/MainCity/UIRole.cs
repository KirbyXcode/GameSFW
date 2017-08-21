using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIRole : UIScene {
	public static UIRole Instance;

	private UILabel[] mRoleInfo;
	public UISceneWidget mButton_Closed;
	private SpinWithMouse[] players;

	protected override void Start () {
		base.Start();
		Instance = this;
		InitWidget();
		SetRoleInfo();
		SetPlayer(CharacterTemplate.Instance.jobId);
	}

	private void InitWidget()
	{
		int mLabelCount = Configuration.GetInt("UIRole","LabelCount");
		mRoleInfo = Global.FindChild(transform,"RoleInfo").GetComponentsInChildren<UILabel>();
//		Debug.Log("mRoleInfo.Length:" + mRoleInfo.Length);
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;

		players = Global.FindChild(transform,"RoleRoot").GetComponentsInChildren<SpinWithMouse>();
		foreach(var item in players)
		{
			item.gameObject.SetActive(false);
			Debug.Log("player:" + item.gameObject);
		}
	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}

	public void SetRoleInfo()
	{
		SetVisible(true);
		int i = 0;
		mRoleInfo[i].text = CharacterTemplate.Instance.name;
		mRoleInfo[++i].text = CharacterTemplate.Instance.lv.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.maxHp.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.maxMp.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.expCur.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.force.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.intellect.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.attackSpeed.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.power.ToString();
	}

	private void SetPlayer(int jobId)
	{
		//根据jobid显示对应的职业模型
		players[jobId-1].gameObject.SetActive(true);
	}

}
