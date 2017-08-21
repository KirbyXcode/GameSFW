using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIRoleName : UIScene {

	private UISceneWidget mButton_Exit;
	private UISceneWidget mButton_OK;
	private UIInput mInput_Name;

	protected override void Start () {
		base.Start();
		mButton_OK = GetWidget("Button_OK");
		if(mButton_OK != null)
			mButton_OK.OnMouseClick = this.ButtonOKOnClick;
		mButton_Exit = GetWidget("Button_Exit");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mInput_Name = Global.FindChild(transform,"Input_Name").GetComponent<UIInput>();
	}

	private void ButtonOKOnClick(UISceneWidget eventObj)
	{
		Debug.Log("确认！");
//		SetVisible(false);
		CharacterTemplate.Instance.name = mInput_Name.value;
		string uiscene = Configuration.GetContent("Scene","LoadCreatRole");
		StartCoroutine(GameMain.Instance.LoadScene(uiscene));
	}

	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		Debug.Log("关闭！");
		SetVisible(false);
	}
	

}
