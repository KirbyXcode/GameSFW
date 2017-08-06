using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DevelopEngine;

public class StoreManager : MonoSingleton<StoreManager> {

	public List<Goods> weaponList = new List<Goods>();	//武器字典
	public List<Goods> clothesList = new List<Goods>();	//衣服字典
	public List<Goods> pantsList = new List<Goods>();	//裤子字典
	public List<Goods> bracersList = new List<Goods>();	//护腕字典
	public List<Goods> necklaceList = new List<Goods>();	//项链字典
	public List<Goods> shoesList = new List<Goods>();	//鞋子字典
	public List<Goods> DrugList = new List<Goods>();	//药品字典
	public List<List<Goods>> goodsList;

	void Awake () {
		weaponList = OperatingDB.Instance.FindGoods("T_Weapon");
		clothesList = OperatingDB.Instance.FindGoods("T_Clothes");
		pantsList = OperatingDB.Instance.FindGoods("T_Pants");
		bracersList = OperatingDB.Instance.FindGoods("T_Bracers");
		necklaceList = OperatingDB.Instance.FindGoods("T_Necklace");
		shoesList = OperatingDB.Instance.FindGoods("T_Shoes");
		DrugList = OperatingDB.Instance.FindGoods("T_Drug");
		goodsList = new List<List<Goods>>(){weaponList,clothesList,pantsList,bracersList,
						necklaceList,shoesList,DrugList};
	}

}
