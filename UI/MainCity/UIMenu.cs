using UnityEngine;
using System.Collections;

public class UIMenu : UIScene {

	private UISceneWidget mButton_Role;
	private UISceneWidget mButton_SKill;
	private UISceneWidget mButton_Backpack;

	protected override void Start () {
		base.Start();
		mButton_Role = GetWidget("Button_Charactor");
		if(mButton_Role != null)
			mButton_Role.OnMouseClick = this.ButtonRoleOnClick;
		mButton_SKill = GetWidget("Button_Skill");
		if(mButton_SKill != null)
			mButton_SKill.OnMouseClick = this.ButtonSKillOnClick;
		mButton_Backpack = GetWidget("Button_Backpack");
		if(mButton_Backpack != null)
			mButton_Backpack.OnMouseClick = this.ButtonBackpackOnClick;
	}

	private void ButtonRoleOnClick(UISceneWidget eventObj)
	{
//		UIManager.Instance.SetVisible(UIName.UIRole, true);
		UIRole.Instance.SetRoleInfo();
	}

	private void ButtonSKillOnClick(UISceneWidget eventObj)
	{
		UIManager.Instance.SetVisible(UIName.UISkillSelect, true);
	}

	private void ButtonBackpackOnClick(UISceneWidget eventObj)
	{
		UIManager.Instance.SetVisible(UIName.UIBackpack, true);
	}

}
