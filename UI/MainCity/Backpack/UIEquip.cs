using UnityEngine;
using System.Collections;

public class UIEquip : UIScene {
	public static UIEquip Instance;

	private GoodsItem goodsItem;
	private UIGoodsItem uiGoodsItem;		//装备
	private BackpackRoleEquip roleEquip;	//身上装备

	private UISprite mSprite_Equip;		//装备图标
	private UILabel mLabel_Name;	//装备名称
	private UILabel mLabel_Quality;		//装备品质
	private UILabel mLabel_Damage;		//装备伤害
	private UILabel mLabel_Hp;		//装备血量
	private UILabel mLabel_Lv;		//装备等级
	private UILabel mLabel_Des;		//装备描述
	private UILabel mLabel_Equip;	//装备按钮文本
	private UISceneWidget mButton_Exit;	//关闭按钮
	private UISceneWidget mButton_Equip;//装备按钮
	private UISceneWidget mButton_Upgrade;//升级按钮

	private bool isLeft = true;		//面板显示在左边

	void Awake()
	{
		Instance = this;
		mSprite_Equip = Global.FindChild<UISprite>(transform, "Sprite_Equip");
		mLabel_Name = Global.FindChild<UILabel>(transform, "Label_Name");
		mLabel_Quality = Global.FindChild<UILabel>(transform, "Label_Quality");
		mLabel_Damage = Global.FindChild<UILabel>(transform, "Label_Damage");
		mLabel_Hp = Global.FindChild<UILabel>(transform, "Label_Hp");
		mLabel_Lv = Global.FindChild<UILabel>(transform, "Label_Lv");
		mLabel_Des = Global.FindChild<UILabel>(transform, "Label_Des");
		mLabel_Equip = Global.FindChild<UILabel>(transform, "Label_Equip");
		mButton_Exit = GetWidget("Button_Exit");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mButton_Equip = GetWidget("Button_Equip");
		if(mButton_Equip != null)
			mButton_Equip.OnMouseClick = this.ButtonEquipOnClick;
		mButton_Upgrade = GetWidget("Button_Upgrade");
		if(mButton_Upgrade != null)
			mButton_Upgrade.OnMouseClick = this.ButtonUpgradeOnClick;

	}

	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		UIBackpack.Instance.DisableButton();	//关闭面板时，禁用出售按钮
		Close();
	}

	//关闭界面
	public void Close()
	{
		ClearObject();
		SetVisible(false);
	}

	public void Show(GoodsItem gi, bool isLeft, UIGoodsItem uigi, BackpackRoleEquip roleEquip) {
//		gameObject.SetActive(true);
		SetVisible(true);
		goodsItem = gi;
		uiGoodsItem = uigi;
		this.roleEquip = roleEquip;

		Vector3 pos = transform.localPosition;	//自身坐标
		this.isLeft = isLeft;
		if (isLeft) {	//如果在左边显示
			transform.localPosition = new Vector3(-Mathf.Abs(pos.x), pos.y, pos.z);	//Mathf.Abs取绝对值
			mLabel_Equip.text = "装备";
		} else {
			transform.localPosition = new Vector3(Mathf.Abs(pos.x), pos.y, pos.z);
			mLabel_Equip.text = "脱下";
		}

		mSprite_Equip.spriteName = gi.Goods.ICON;
		mLabel_Name.text = gi.Goods.Name;
		mLabel_Quality.text = gi.Goods.Quality.ToString();
		mLabel_Damage.text = gi.Goods.Damage.ToString();
		mLabel_Hp.text = gi.Goods.HP.ToString();
		mLabel_Des.text = gi.Goods.Des;
		mLabel_Lv.text = gi.Level.ToString();
	}

	/// <summary>
	/// 点击装备或脱下按钮触发
	/// </summary>
	private void ButtonEquipOnClick(UISceneWidget eventObj)
	{
		UpdatePower(goodsItem);
		ButtonExitOnClick(eventObj);	//调用关闭方法
	}

	public void UpdatePower(GoodsItem goodsItem)
	{
//		int startVal = CharacterTemplate.Instance.GetPower();
		if(isLeft)	//如果是在左边，即为装备页面
		{
			//			uiGoodsItem.Clear();	//清空该装备格子
			OperatingDB.Instance.CreateDataBase();	//打开数据库
			CharacterTemplate.Instance.Puton(goodsItem);	//穿上装备
			OperatingDB.Instance.db.CloseSqlConnection();
			OperatingDB.Instance.UpdateEquipStatus(goodsItem);	//更新数据库背包穿着状态
		}
		else//从身上脱下
		{
			roleEquip.Clear();	//把身上的装备清空
			OperatingDB.Instance.CreateDataBase();	//打开数据库
			CharacterTemplate.Instance.Unload(goodsItem);	//卸下装备
			OperatingDB.Instance.db.CloseSqlConnection();
			OperatingDB.Instance.UpdateEquipStatus(goodsItem);	//更新数据库背包穿着状态
		}
//		OperatingDB.Instance.UpdateEquipStatus(goodsItem);	//更新数据库背包穿着状态
		int endVal = CharacterTemplate.Instance.GetPower();
//		uiPower.ShowPowerChange(startVal, endVal);	//更新战斗力
		UIBackpack.Instance.mLabel_Power.text = endVal.ToString();
		UIBackpack.Instance.UpdateCount();			//更新背包数量
	}

	private void ClearObject()
	{
		goodsItem = null;
		uiGoodsItem = null;	//清空背包
	}

	/// <summary>
	/// 升级按钮触发
	/// </summary>
	private void ButtonUpgradeOnClick(UISceneWidget eventObj)
	{

		int goldNeed = (goodsItem.Level + 1) * goodsItem.Goods.Price;	//需要升级的金币
		bool isSuccess = CharacterTemplate.Instance.GetGolds(goldNeed);
		if(isSuccess)
		{
			goodsItem.Level += 1;
			mLabel_Lv.text = goodsItem.Level.ToString();	//更新lv
		}
		else
		{
			//金币不足
			Debug.Log("金币不足");
		}
	}




}
