using UnityEngine;
using System.Collections;

public class UIOther : UIScene {
	
	private UISceneWidget mButton_Battle;
	private UISceneWidget mButton_Store;

	protected override void Start () {
		base.Start();
		mButton_Battle = GetWidget("Button_Battle");
		if(mButton_Battle != null)
			mButton_Battle.OnMouseClick = this.ButtonBattleOnClick;
		mButton_Store  = GetWidget("Button_Store");
		if(mButton_Store != null)
			mButton_Store.OnMouseClick = this.ButtonStoreOnClick;
	}

	private void ButtonBattleOnClick(UISceneWidget eventObj)
	{
		Debug.Log("选择关卡");
		UIManager.Instance.SetVisible(UIName.UIBattle, true);
	}

	private void ButtonStoreOnClick(UISceneWidget eventObj)
	{
		UIManager.Instance.SetVisible(UIName.UIStore, true);
	}

}
