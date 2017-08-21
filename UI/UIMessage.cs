using UnityEngine;
using System.Collections;

public class UIMessage : UIScene {

	public static UIMessage instance;

	private UISceneWidget mButton_Sure;
	private UILabel mLabel_Message;

	void Awake()
	{
		instance = this;
		mLabel_Message = Global.FindChild(transform, "Label_Message").
			GetComponent<UILabel>();
	}

	protected override void Start () {
		base.Start();
		mButton_Sure = GetWidget("Button_Sure");
		if(mButton_Sure != null)
			mButton_Sure.OnMouseClick = this.ButtonSureOnClick;
	}

	private void ButtonSureOnClick(UISceneWidget eventObj)
	{
		Debug.Log("关闭");
		SetVisible(false);
	}

	public void SetLabel(string message)
	{
		SetVisible(true);
		if(mLabel_Message != null)
			mLabel_Message.text = message;
	}
	

}
