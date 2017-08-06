using UnityEngine;
using System.Collections;

public enum GoodsType{
	Equip,	//装备
	Drug,	//药品
	Box		//宝箱
}

public enum EquipType {
	Weapon,		//武器
	Clothes,	//衣服
	Pants,		//裤子
	Bracers,	//护腕
	Necklace,	//项链
	Shoes		//鞋子
}

public class Goods {

	private int id;//ID
	private string name;//名称
	private string icon;//图标的名称
	private GoodsType goodsType;//物品类型
	private EquipType equipType;//装备类型
	private int price = 0;//价格
	private int starLevel = 1;//星级
	private int quality = 1;//品质
	private int damage = 0;//伤害
	private int hp = 0;//生命
	private int applyValue;//作用值
	private string des;//描述

	public int ID {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}
	public string ICON {
		get {
			return icon;
		}
		set {
			icon = value;
		}
	}
	public GoodsType GoodsType {
		get {
			return goodsType;
		}
		set {
			goodsType = value;
		}
	}
	public EquipType EquipTYPE {
		get {
			return equipType;
		}
		set {
			equipType = value;
		}
	}
	public int Price {
		get {
			return price;
		}
		set {
			price = value;
		}
	}
	public int StarLevel {
		get {
			return starLevel;
		}
		set {
			starLevel = value;
		}
	}
	public int Quality {
		get {
			return quality;
		}
		set {
			quality = value;
		}
	}
	public int Damage {
		get {
			return damage;
		}
		set {
			damage = value;
		}
	}
	public int HP {
		get {
			return hp;
		}
		set {
			hp = value;
		}
	}
	public int ApplyValue {
		get {
			return applyValue;
		}
		set {
			applyValue = value;
		}
	}
	public string Des {
		get {
			return des;
		}
		set {
			des = value;
		}
	}

}
