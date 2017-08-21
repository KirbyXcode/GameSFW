using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DevelopEngine;

public class UIBackpack : UIScene {
	public static UIBackpack Instance;

	private BackpackRoleEquip Equip_Weapon;
	private BackpackRoleEquip Equip_Clothes;
	private BackpackRoleEquip Equip_Pants;
	private BackpackRoleEquip Equip_Bracers;
	private BackpackRoleEquip Equip_Necklace;
	private BackpackRoleEquip Equip_Shoes;
	private UISceneWidget mButton_Closed;

	private UILabel mLabel_Hp;
	private UILabel mLabel_Mp;
	private UISceneWidget mButton_Arrange;	//整理按钮
	private UISceneWidget mButton_sale;		//出售按钮
	private UILabel mLabel_GoodsCount;		//物品数量
	private UILabel mLabel_Price;			//物品价格
	public UILabel mLabel_Power;

	public int count = 0;	//物品数量

	public List<UIGoodsItem> uiItemList = new List<UIGoodsItem>();//所有的物品格子
	
	private UIGoodsItem uigi;

	private Vector3 v3;		//背包列表初始位置
	private SpringPanel springPanel;	//复位背包列表

	void Awake(){
		Instance = this;
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;
		Equip_Weapon = Global.FindChild<BackpackRoleEquip>(transform,"Button_Weapon");
		Equip_Clothes = Global.FindChild<BackpackRoleEquip>(transform,"Button_Clothes");
		Equip_Pants = Global.FindChild<BackpackRoleEquip>(transform,"Button_Pants");
		Equip_Bracers = Global.FindChild<BackpackRoleEquip>(transform,"Button_Bracers");
		Equip_Necklace = Global.FindChild<BackpackRoleEquip>(transform,"Button_Necklace");
		Equip_Shoes = Global.FindChild<BackpackRoleEquip>(transform,"Button_Shoes");
		mLabel_Hp = Global.FindChild<UILabel>(transform,"Label_Hp");
		mLabel_Mp = Global.FindChild<UILabel>(transform,"Label_Mp");
		mLabel_GoodsCount = Global.FindChild<UILabel>(transform, "Label_GoodsCount");
		mLabel_Price = Global.FindChild<UILabel>(transform, "Label_Price");
		mLabel_Power = Global.FindChild<UILabel>(transform,"Label_Power");

		mButton_Arrange = GetWidget("Button_Arrange");
		if(mButton_Arrange != null)
			mButton_Arrange.OnMouseClick = this.ButtonArrangeOnClick;
		mButton_sale = GetWidget("Button_Sale");
		if(mButton_sale != null)
			mButton_sale.OnMouseClick = this.ButtonsaleOnClick;
		DisableButton();	//禁用出售按钮
		GoodsManager.instance.FindGoods();	//初始化背包列表
		mLabel_Power.text = CharacterTemplate.Instance.GetPower().ToString();	//设置战斗力数值
		UpdateGoodsInfo();	//更新背包栏
		v3 = new Vector3 (372f,157f,0);
	}

	protected override void Start () {
		CharacterTemplate.Instance.OnCharacterInfoChanged += OnCharacterInfoChanged;
		UpdateEquipInfo(); 
	}

	void OnDestroy() {
		CharacterTemplate.Instance.OnCharacterInfoChanged -= OnCharacterInfoChanged;
	}

	void OnCharacterInfoChanged( InfoType type ) {
		if (type == InfoType.All || type == InfoType.maxHP || type == InfoType.damageMax || type == InfoType.expcur ||type==InfoType.Equip ) {
			UpdateEquipInfo();
		}
	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		ResetList();
		SetVisible(false);
	}

	//更新装备信息
	public void UpdateEquipInfo()
	{
		Equip_Weapon.SetGoodsItem(CharacterTemplate.Instance.weaponGoodsItem);
		Equip_Clothes.SetGoodsItem(CharacterTemplate.Instance.clothesGoodsItem);
		Equip_Pants.SetGoodsItem(CharacterTemplate.Instance.pantsGoodsItem);
		Equip_Bracers.SetGoodsItem(CharacterTemplate.Instance.bracersGoodsItem);
		Equip_Necklace.SetGoodsItem(CharacterTemplate.Instance.necklaceGoodsItem);
		Equip_Shoes.SetGoodsItem(CharacterTemplate.Instance.shoesGoodsItem);

		mLabel_Hp.text = CharacterTemplate.Instance.maxHp.ToString();
		mLabel_Mp.text = CharacterTemplate.Instance.maxMp.ToString();
	}
	
	//更新物品信息
	public void UpdateGoodsInfo()
	{
		int temp = 0;
		for (int i = 0; i < GoodsManager.instance.goodsItemList.Count; i++) {//遍历背包列表
			GoodsItem goodsItem = GoodsManager.instance.goodsItemList[i];
			uiItemList[temp].SetGoodsItem(goodsItem);	//设置物品
			temp++;
		}
		count = temp;	//物品数量

		for(int i = temp; i < uiItemList.Count; i++)//清空剩余背包
		{
			uiItemList[i].Clear();	//清除物品
		}
		mLabel_GoodsCount.text = count + "/36";
	}

	//角色装备按钮点击事件
	public void ShowGoodsItem(UISceneWidget eventObj, GoodsItem gi, bool isLeft)
	{

		if(gi.Goods.GoodsType == GoodsType.Equip)
		{
			UIGoodsItem uigi = null;
			BackpackRoleEquip roleEquip = null;
			if(isLeft)
			{
				uigi = eventObj.GetComponent<UIGoodsItem>();
			}
			else
			{
				roleEquip = eventObj.GetComponent<BackpackRoleEquip>();
			}
			UIGoods.Instance.Close();	//关闭道具界面
			UIEquip.Instance.Show(gi, isLeft, uigi, roleEquip);	//显示装备界面
		}
		else /*if(gi.Goods.GoodsType == GoodsType.Drug)*/
		{
			UIGoodsItem uigi = eventObj.GetComponent<UIGoodsItem>();
			UIEquip.Instance.Close();		//关闭装备界面
			UIGoods.Instance.Show(gi,uigi);	//显示道具界面
		}

		//如果是装备类型，且为背包装备，或者不为装备类型，可以出售物品
		if((gi.Goods.GoodsType == GoodsType.Equip && isLeft) || gi.Goods.GoodsType != GoodsType.Equip)
		{
			this.uigi = eventObj.GetComponent<UIGoodsItem>();
			EnableButton();
			//出售价格 = 物品单价 × 物品数量
			mLabel_Price.text = (this.uigi.goodsItem.Goods.Price * this.uigi.goodsItem.Count).ToString();
		}

	}

	private void ButtonArrangeOnClick(UISceneWidget eventObj)
	{
		UpdateGoodsInfo();
	}

	private void ButtonsaleOnClick(UISceneWidget eventObj)
	{
		if(!uigi.goodsItem.IsDressed)//如果装备没有穿着，可以出售
		{
			int price = int.Parse(mLabel_Price.text);
			CharacterTemplate.Instance.AddGold(price);	//出售获得金币
			GoodsManager.instance.RemoveGoodsItem(uigi.goodsItem);	//出售物品
			OperatingDB.Instance.DeleteGoods(uigi.goodsItem.Goods.ID);	//删除数据库内容
			uigi.Clear();
			count--;	//物品数量减少
			mLabel_GoodsCount.text = count + "/36";	//更新数量显示
			UIEquip.Instance.Close();	//关闭装备界面
			UIGoods.Instance.Close();	//关闭道具界面
			DisableButton();			//禁用出售按钮
		}
		else
		{
			Debug.Log("请先脱下装备，然后再出售装备！");
			UIMessage.instance.SetLabel(Configuration.GetContent("Backpack","TakeOff"));
		}
	}

	//禁用按钮
	public void DisableButton()
	{
		mButton_sale.gameObject.SetActive(false);
		mLabel_Price.text = "";
	}

	//启用按钮
	void EnableButton() 
	{
		mButton_sale.gameObject.SetActive(true);
	}

	//更新背包数量显示
	public void UpdateCount()
	{ 
		count = 0;
		foreach(UIGoodsItem uigi in uiItemList)
		{
			if(uigi.goodsItem != null)
			{
				count++;	//背包数量

			}
		}
		mLabel_GoodsCount.text = count + "/36";	//更新数量显示
	}

	//复位背包列表位置
	void ResetList()
	{
		if(springPanel == null)
			springPanel = Global.FindChild(transform,"Scroll View").AddComponent<SpringPanel>();
		springPanel.target = v3;
		springPanel.strength = 100;
		springPanel.enabled = true;
	}
}
