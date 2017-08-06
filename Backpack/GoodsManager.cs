using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Develop;
using System;

public class GoodsManager : MonoSingleton<GoodsManager>{

	public List<GoodsItem> goodsItemList = new List<GoodsItem> ();	//背包列表

	//删除物品
	public void RemoveGoodsItem(GoodsItem gi)
	{
		goodsItemList.Remove(gi);
	}
	
	//读取背包
	public void FindGoods()
	{
		OperatingDB.Instance.CreateDataBase();
		SqliteDataReader goodsInfo = OperatingDB.Instance.db.ReadFullTable("T_Backpack");
		while(goodsInfo.Read())
		{
			int i = 0;
			Goods goods = new Goods ();
			goods.ID = int.Parse(goodsInfo[i].ToString());
			goods.Name = goodsInfo[++i].ToString();
			goods.ICON = goodsInfo[++i].ToString();

			switch(goodsInfo[++i].ToString())//物品类型
			{
			case "Equip":
				goods.GoodsType = GoodsType.Equip;
				break;
			case "Drug":
				goods.GoodsType = GoodsType.Drug;
				break;
			case "Box":
				goods.GoodsType = GoodsType.Box;
				break;
			}

			if(goods.GoodsType == GoodsType.Equip)
			{
				switch(goodsInfo[++i].ToString())
				{
				case "Weapon":
					goods.EquipTYPE = EquipType.Weapon;
					break;
				case "Clothes":
					goods.EquipTYPE = EquipType.Clothes;
					break;
				case "Pants":
					goods.EquipTYPE = EquipType.Pants;
					break;
				case "Bracers":
					goods.EquipTYPE = EquipType.Bracers;
					break;
				case "Necklace":
					goods.EquipTYPE = EquipType.Necklace;
					break;
				case "Shoes":
					goods.EquipTYPE = EquipType.Shoes;
					break;
				}
			}
			//售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
			goods.Price = int.Parse(goodsInfo[5].ToString());
			if(goods.GoodsType == GoodsType.Equip)
			{
				goods.StarLevel = int.Parse(goodsInfo[6].ToString());
				goods.Quality = int.Parse(goodsInfo[7].ToString());
				goods.Damage = int.Parse(goodsInfo[8].ToString());
				goods.HP = int.Parse(goodsInfo[9].ToString());
			}
			if(goods.GoodsType == GoodsType.Drug)
			{
				goods.ApplyValue = int.Parse(goodsInfo[10].ToString());
			}
			goods.Des = goodsInfo[11].ToString();

			GoodsItem goodsItem = new GoodsItem();
			goodsItem.Goods = goods;
			goodsItem.Level = int.Parse(goodsInfo[12].ToString());
			goodsItem.Count = int.Parse(goodsInfo[13].ToString());
			goodsItem.IsDressed = Convert.ToBoolean(int.Parse(goodsInfo[14].ToString()));//using System;
			goodsItemList.Add(goodsItem);
			if(goodsItem.IsDressed)
			{
				Debug.Log("goodsItem:" + goodsItem.Goods.ID);
				CharacterTemplate.Instance.Puton(goodsItem);	//穿上装备
			}
		}
		OperatingDB.Instance.db.CloseSqlConnection();
	}

}
