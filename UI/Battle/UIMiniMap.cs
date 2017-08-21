using UnityEngine;
using System.Collections;

public class UIMiniMap : UIScene {

	private UISceneWidget mButton_Exit;
	private UILabel mLabel_Time;
	public float battleTime;	

	protected override void Start () {
		base.Start();
		mButton_Exit = GetWidget("Button_Exit");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mLabel_Time = 
			Global.FindChild(transform,"Label_Time").GetComponent<UILabel>();
	}

	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		UIManager.Instance.SetVisible(UIName.UIPopup, true);
	}

	public void SetTime(int time)
	{
		if(mLabel_Time != null)
			mLabel_Time.text = Global.GetMinuteTime(time);
	}
	

	protected override void Update () {
		base.Update();
		if(battleTime >= 300)
		{
			//显示战斗失败
			UIManager.Instance.SetVisible(UIName.UIBattleOver, true);
		}
		else
		{
			battleTime += Time.deltaTime;
		}
		SetTime((int)battleTime);
	}
	
}
