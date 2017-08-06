using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using DevelopEngine;

public class UISellGoods : MonoBehaviour {

	private UILabel mLabel_Name;			//道具名称
	private UISprite mSprite_Icon;			//道具icon
	private UILabel mLabel_Des;				//道具简介
	private UILabel mLabel_Price;			//物品价格
	private UISceneWidget mButton_Buy;	//购买按钮
	public Goods goods;					//商品

	void Awake()
	{
		mButton_Buy = Global.FindChild<UISceneWidget>(transform,"Button_Buy");
		if(mButton_Buy)
			mButton_Buy.OnMouseClick = this.ButtonBuyOnClick;
		mLabel_Name = Global.FindChild<UILabel>(transform,"Label_GoodsName");
		mSprite_Icon = Global.FindChild<UISprite>(transform,"Sprite_Icon");
		mLabel_Des = Global.FindChild<UILabel>(transform,"Label_GoodsDes");
		mLabel_Price = Global.FindChild<UILabel>(transform,"Label_GoodsPrice");
	}

	private void ButtonBuyOnClick(UISceneWidget eventObj)
	{
		Debug.Log("购买物品");
		Buy();
	}

	//显示商品
	public void Show(Goods goods)
	{
		this.goods = goods;
		mLabel_Name.text = goods.Name;
		mSprite_Icon.spriteName = goods.ICON;
		mLabel_Des.text = goods.Des;
		mLabel_Price.text = goods.Price.ToString();
	}

	//购买物品
	public void Buy()
	{
			if(goods.Price <= CharacterTemplate.Instance.gold)
			{
				Debug.Log("goods.GoodsType:" + goods.GoodsType);
				UpdateBackpack(goods, goods.Price);	//把物品写入数据库
				UIBackpack.Instance.UpdateGoodsInfo();	//更新背包
			}
			else
			{
				Debug.Log("金币不足，请充值！");
				UIMessage.instance.SetLabel(Configuration.GetContent("Backpack","GoldShortage"));
			}

	}

	DbAccess db;
	//购买物品背包
	public void UpdateBackpack(Goods goods, int price)
	{
		GoodsItem goodsItem = new GoodsItem();
		goodsItem.Goods = goods;
		int i = 1;
		int id = 0;
		bool isAdd = true;
		db = GameMain.Instance.OpenDB(db);
		SqliteDataReader money = db.Select("T_Money","AccountID","1");
		money.Read();
		CharacterTemplate.Instance.gold = int.Parse(money[1].ToString());
		if(price <= CharacterTemplate.Instance.gold)
		{
			SqliteDataReader goodsId = db.SelectOrderASC("T_Backpack","ID");
			while(goodsId.Read())
			{
				if(goods.GoodsType == GoodsType.Drug)	//如果是道具类型
				{
					//背包数据库内有道具类型并且已经有此道具，道具数量加1
					if(goodsId[3].ToString() == "Drug" && 
					   goodsId[1].ToString() == goods.Name)	
					{
						CharacterTemplate.Instance.gold -= price; //扣除商品价格
						db.UpdateInto("T_Money", new string[] {"Gold"},	//更新数据库金币
						new string[] {CharacterTemplate.Instance.gold.ToString()},"AccountID","1");
						UIManager.Instance.GetUI<UIMoeny>(UIName.UIMoeny).
							SetGold(CharacterTemplate.Instance.gold);	//刷新金币UI
						int count = int.Parse(goodsId[13].ToString());
						count += 1;
						db.UpdateInto("T_Backpack",new string[] {"Count"},
						new string[] {count.ToString()},"ID",goodsId[0].ToString());//更新道具数量
						UpdateDrugCount(goods);		//更新背包药品显示数量
						db.CloseSqlConnection();
						return ;
					}

					if(isAdd)
					{
						if(i != int.Parse(goodsId[0].ToString()))
						{
							id = i;
							isAdd = false;
						}
					}
				}
				else{
					if(i != int.Parse(goodsId[0].ToString()))
					{
						id = i;
						break;
					}
				}
				i++;
			}
			if(GoodsManager.instance.goodsItemList.Count < 36)//判断背包是否有空位置
			{
				if(id == 0)
				{
					id = i;
				}
				CharacterTemplate.Instance.gold -= price; //扣除商品价格
				db.UpdateInto("T_Money", new string[] {"Gold"},	//更新数据库金币
				new string[] {CharacterTemplate.Instance.gold.ToString()},"AccountID","1");
				UIManager.Instance.GetUI<UIMoeny>(UIName.UIMoeny).
					SetGold(CharacterTemplate.Instance.gold);	//刷新金币UI
				db.InsertInto("T_Backpack",new string[] {id.ToString(), "'" + goods.Name + "'",
				"'" + goods.ICON + "'", "'" + goods.GoodsType.ToString() + "'", 
				"'" + goods.EquipTYPE.ToString() + "'",goods.Price.ToString(),
				goods.StarLevel.ToString(),goods.Quality.ToString(),goods.Damage.ToString(),
				goods.HP.ToString(),goods.ApplyValue.ToString(), "'" + goods.Des + "'", "1","1","0"});

				goods.ID = id;
				goodsItem.Goods = goods;
				GoodsManager.instance.goodsItemList.Add(goodsItem);	//添加物品到背包列表
			}
			else
			{
				UIMessage.instance.SetLabel(Configuration.GetContent("Backpack","Backpackfull"));
			}
		}
		else
		{
			Debug.Log("金币不足，请充值！");
			UIMessage.instance.SetLabel(Configuration.GetContent("Backpack","GoldShortage"));
		}
		
		db.CloseSqlConnection();
	}

	//更新药品数量
	void UpdateDrugCount(Goods goods)
	{
		foreach(GoodsItem gi in GoodsManager.instance.goodsItemList)
		{
			if(gi.Goods.Name == goods.Name)
				gi.Count++;
		}
	}

}
