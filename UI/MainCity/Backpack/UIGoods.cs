using UnityEngine;
using System.Collections;

public class UIGoods : UIScene {
	public static UIGoods Instance;
	
	private UILabel mLabel_Name;			//道具名称
	private UISprite mSprite_Icon;			//道具icon
	private UILabel mLabel_Des;				//道具简介
	private UILabel mLabel_Number;			//道具数量
	private UISceneWidget mButton_Use;		//使用按钮
	private UISceneWidget mButton_BatchUse;	//批量使用按钮
	private UISceneWidget mButton_Closed;	//关闭按钮

	private GoodsItem gi;
	private UIGoodsItem uigi;

	void Awake()
	{
		Instance = this;
		mLabel_Name = Global.FindChild<UILabel>(transform,"Label_GoodsName");
		mSprite_Icon = Global.FindChild<UISprite>(transform,"Sprite_Icon");
		mLabel_Des = Global.FindChild<UILabel>(transform,"Label_GoodsDes");
		mLabel_Number = Global.FindChild<UILabel>(transform,"Label_Number");
		mButton_Use = GetWidget("Button_Use");
		if(mButton_Use != null)
			mButton_Use.OnMouseClick = this.ButtonUseOnClick;
		mButton_BatchUse = GetWidget("Button_BatchUse");
		if(mButton_BatchUse != null)
			mButton_BatchUse.OnMouseClick = this.ButtonBatachUseOnClick;
		mButton_Closed = GetWidget("Button_Close");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;

	}

	private void ButtonUseOnClick(UISceneWidget eventObj)
	{
		uigi.ChangeCount(1);
		CharacterTemplate.Instance.GoodsUse(gi,1);
		ButtonClosedOnClick(eventObj);
	}
	
	private void ButtonBatachUseOnClick(UISceneWidget eventObj)
	{
		uigi.ChangeCount(gi.Count);
		CharacterTemplate.Instance.GoodsUse(gi,gi.Count);
		ButtonClosedOnClick(eventObj);
	}
	
	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		Close();
		UIBackpack.Instance.DisableButton();	//禁用出售按钮

	}

	//关闭界面
	public void Close()
	{
		Clear();
		SetVisible(false);
	}

	//显示 
	public void Show(GoodsItem gi, UIGoodsItem uigi)
	{
		SetVisible(true);
		this.gi = gi;
		this.uigi = uigi;
		mLabel_Name.text = gi.Goods.Name;
		mSprite_Icon.spriteName = gi.Goods.ICON;
		mLabel_Des.text = gi.Goods.Des;
		mLabel_Number.text = "使用(" + gi.Count + ")";
	}

	void Clear()
	{
		gi = null;
		this.uigi = null;
	}



}
