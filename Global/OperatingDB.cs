using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.IO;
using DevelopEngine;
using ARPGSimpleDemo.Skill;
using System.Collections.Generic;
using System;

public class OperatingDB : MonoSingleton<OperatingDB> {

	public DbAccess db;	//数据库
	string appDBPath;//路径
	public int exp;	//经验
	public int gold;//金币
	GameObject player;

	//创建数据库
	public void CreateDataBase()
	{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		appDBPath = Application.streamingAssetsPath + "/ARPG.db";
#elif UNITY_ANDROID || UNITY_IPHONE
		appDBPath = Application.persistentDataPath + "/ARPG.db";
		if(!File.Exists(appDBPath))
		{
			StartCoroutine(CopyDB());
		}
#endif
		db = new DbAccess ("URI=file:" + appDBPath);
	}

	IEnumerator CopyDB()
	{
		string loadPath = string.Empty;
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		loadPath = Application.streamingAssetsPath + "/ARPG.db";
		#elif UNITY_ANDROID
		loadPath = "jar:file://" + Application.dataPath + "!/assets" + "/ARPG.db";
		#elif UNITY_IPHONE
		loadPath = + Application.dataPath + "/Raw" + "/ARPG.db";
		#endif
		WWW www = new WWW (loadPath);
		yield return www;
		File.WriteAllBytes(appDBPath, www.bytes);
	}

	//获取账号信息
	public void GetRoleInfoDB()
	{
		CreateDataBase();
//		SqliteDataReader roleInfo = db.SelectWhere("T_Account",
//		                                           new string[] {"AccountName"},
//		new string[] {"AccountID"}, new string[] {"="},new string[] {"1"});
		SqliteDataReader roleInfo = db.Select("T_Account","AccountID","1");
		while(roleInfo.Read())
		{
//			CharacterTemplate.Instance.name = 
//				roleInfo.GetString(roleInfo.GetOrdinal("AccountName"));//text类型数据不能为空
//			CharacterTemplate.Instance.name = 
//				roleInfo.GetValue(roleInfo.GetOrdinal("AccountName")).ToString();
			CharacterTemplate.Instance.name = roleInfo[1].ToString();
		}
		db.CloseSqlConnection();
		Debug.Log("CharacterTemplate.Instance.name:" + CharacterTemplate.Instance.name);

		//判断是否有游戏存档
		if(CharacterTemplate.Instance.name == string.Empty)
		{
			UIManager.Instance.SetVisible(UIName.UIRoleName, true);
		}
		else
		{
			UIManager.Instance.SetVisible(UIName.UIResetRole, true);
		}
	}

	/// <summary>
	/// 读取职业
	/// </summary>
	public void FindJobDB(int jobid)
	{
		CreateDataBase();
		SqliteDataReader sqReader = db.Select("T_Job","JobID",jobid.ToString());
		while(sqReader.Read())
		{
			int i = 0;
			CharacterTemplate.Instance.jobId = int.Parse(sqReader[i].ToString());
			CharacterTemplate.Instance.lv = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.expCur = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.force = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.intellect = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.attackSpeed = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.maxHp = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.maxMp = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.damageMax = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.jobModel = sqReader[++i].ToString();
		}
		db.InsertInto("T_Character",new string[] {"1",
			CharacterTemplate.Instance.jobId.ToString(),
			CharacterTemplate.Instance.lv.ToString(),
			CharacterTemplate.Instance.expCur.ToString(),
			CharacterTemplate.Instance.force.ToString(),
			CharacterTemplate.Instance.intellect.ToString(),
			CharacterTemplate.Instance.attackSpeed.ToString(),
			CharacterTemplate.Instance.maxHp.ToString(),
			CharacterTemplate.Instance.maxMp.ToString(),
			CharacterTemplate.Instance.damageMax.ToString(),
			"'" + CharacterTemplate.Instance.jobModel + "'"
		});

		//写入名称
		db.UpdateInto("T_Account",new string[] {"AccountName"},
		new string[] {"'" + CharacterTemplate.Instance.name + "'"},"AccountID","1");

		db.CloseSqlConnection();
		//进入主城
		Debug.Log("进入游戏主城！");
		StartCoroutine(GameMain.Instance.LoadScene(
					Configuration.GetContent("Scene","LoadUIMainCity"),
					Configuration.GetContent("Scene","LoadMainCity")));
	}

	/// <summary>
	/// 删除游戏存档
	/// </summary>
	public void DeleteRoloInfoDB()
	{
		CreateDataBase();
		db.UpdateInto("T_Account",new string[] {"AccountName"},new string[] {"''"},
						"AccountID","1");
		db.Delete("T_Character",new string[] {"CharacterID"}, new string[] {"1"});
		db.UpdateInto("T_Money", new string[] {"Gold","Diamond"},
						new string[] {"99999","0"},"AccountID","1");
		db.DeleteContents("T_Backpack");//删除表全部数据
		db.CloseSqlConnection();
		UIManager.Instance.SetVisible(UIName.UIResetRole, false);
		UIManager.Instance.SetVisible(UIName.UIRoleName, true);
	}

	/// <summary>
	/// 读取货币
	/// </summary>
	public void FindMoneyDB()
	{
		CreateDataBase();
		SqliteDataReader money = db.Select("T_Money","AccountID","1");
		while(money.Read())
		{
			CharacterTemplate.Instance.gold = int.Parse(money[1].ToString());
			CharacterTemplate.Instance.diamond = int.Parse(money[2].ToString());
		}
		db.CloseSqlConnection();
		UIMoeny uiMoeny = UIManager.Instance.GetUI<UIMoeny>(UIName.UIMoeny);
		uiMoeny.SetGold(CharacterTemplate.Instance.gold);
		uiMoeny.SetDiamond(CharacterTemplate.Instance.diamond);
	}

	/// <summary>
	/// 读取游戏存档
	/// </summary>
	public void FindRoleInfoDB()
	{
		CreateDataBase();
		SqliteDataReader roleInfo = db.Select("T_Account","AccountID","1");
		while(roleInfo.Read())
		{
			CharacterTemplate.Instance.name = roleInfo[1].ToString();
		}
		
		//判断是否有游戏存档
		if(CharacterTemplate.Instance.name == string.Empty)
		{
			db.CloseSqlConnection();
			UIManager.Instance.SetVisible(UIName.UIMessageBox, true);
		}
		else
		{
			SqliteDataReader sqReader = db.Select("T_Character","CharacterID","1");
			while(sqReader.Read())
			{
				int i = 0;
				CharacterTemplate.Instance.characterId = int.Parse(sqReader[i].ToString());
				CharacterTemplate.Instance.jobId = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.lv = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.expCur = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.force = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.intellect = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.attackSpeed = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.maxHp = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.maxMp = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.damageMax = int.Parse(sqReader[++i].ToString());
				CharacterTemplate.Instance.jobModel = sqReader[++i].ToString();
			}
			db.CloseSqlConnection();
			StartCoroutine(GameMain.Instance.LoadScene(
				Configuration.GetContent("Scene","LoadUIMainCity"),
				Configuration.GetContent("Scene","LoadMainCity")));
		}

	}

	//结算
	public void GetResultsDB(int sceneId, int star)
	{

		CreateDataBase();
		SqliteDataReader scene = db.SelectWhere("T_Scene",
        new string[] {"SceneExp" + star,"Gold" + star},new string[] {"SceneName"},
		new string[] {"="}, new string[] {sceneId.ToString()});
		while(scene.Read())
		{
			exp = int.Parse(scene[0].ToString());
			gold = int.Parse(scene[1].ToString());
		}

//		Debug.Log("exp:" + exp);
//		Debug.Log("gold:" + gold);
		UIManager.Instance.SetVisible(UIName.UIVictory,true);
		UIVictory uivictory = 
			UIManager.Instance.GetUI<UIVictory>(UIName.UIVictory);
		uivictory.SetExp(exp);
		uivictory.SetStar(star);

		//读取角色表
		SqliteDataReader sqReader = db.Select("T_Character","CharacterID","1");
		while(sqReader.Read())
		{
			int i = 0;
			CharacterTemplate.Instance.characterId = int.Parse(sqReader[i].ToString());
			CharacterTemplate.Instance.jobId = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.lv = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.expCur = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.force = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.intellect = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.attackSpeed = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.maxHp = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.maxMp = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.damageMax = int.Parse(sqReader[++i].ToString());
		}

		//读取经验表,获取当前等级最大经验
		SqliteDataReader lvExp = 
			db.Select("T_Exp","Lv",CharacterTemplate.Instance.lv.ToString());
		int maxExp = 0;
		while(lvExp.Read())
		{
			maxExp = int.Parse(lvExp[2].ToString());
		}

		//经验累加
		CharacterTemplate.Instance.expCur += exp;
		if(CharacterTemplate.Instance.expCur > maxExp)	//升级
		{
			CharacterTemplate.Instance.lv += 1;
			CharacterTemplate.Instance.force += 5;
			CharacterTemplate.Instance.intellect += 5;
			CharacterTemplate.Instance.maxHp += 1000;
			CharacterTemplate.Instance.maxMp += 500;
			CharacterTemplate.Instance.damageMax += 10;
			//更新数据库
			db.UpdateInto("T_Character",
              new string[] {"Lv","ExpCur","Force","Intellect","MaxHP","MaxMP","DamageMax"},
			new string[] {
				CharacterTemplate.Instance.lv.ToString(),
				CharacterTemplate.Instance.expCur.ToString(),
				CharacterTemplate.Instance.force.ToString(),
				CharacterTemplate.Instance.intellect.ToString(),
				CharacterTemplate.Instance.maxHp.ToString(),
				CharacterTemplate.Instance.maxMp.ToString(),
				CharacterTemplate.Instance.damageMax.ToString()},
				"CharacterID","1");
		}
		else
		{
			db.UpdateInto("T_Character",new string[] {"ExpCur"}, 
			new string[] {CharacterTemplate.Instance.expCur.ToString()},
			"CharacterID","1");
		}

		SqliteDataReader goldInfo = db.Select("T_Money","AccountID","1");
		while(goldInfo.Read())
		{
			CharacterTemplate.Instance.gold = int.Parse(goldInfo[1].ToString());
		}
		CharacterTemplate.Instance.gold += gold;
		db.UpdateInto("T_Money",new string[] {"Gold"},
						new string[] {CharacterTemplate.Instance.gold.ToString()},
						"AccountID","1");
		db.CloseSqlConnection();
	}

	//读取商城道具
	public List<Goods> FindGoods(string tableName)
	{
		List<Goods> goodsDict = new List<Goods>();
		CreateDataBase();
		SqliteDataReader goodsInfo = db.ReadFullTable(tableName);
		while(goodsInfo.Read())
		{
//			Debug.Log("goodsInfo.FieldCount:" + goodsInfo.VisibleFieldCount);
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
			goodsDict.Add(goods);
		}
		db.CloseSqlConnection();
		return goodsDict;
	}

	//删除背包物品
	public void DeleteGoods(int id)
	{
		CreateDataBase();
//		Debug.Log("id:" + id);
		db.Delete("T_Backpack",new string[] {"id"}, new string[] {id.ToString()});
		db.CloseSqlConnection();
	}

	//更新药品数量
	public void UpdateDrug(int id)
	{
		CreateDataBase();
		SqliteDataReader dr = db.Select("T_Backpack","ID",id.ToString());
		dr.Read();
		int count = int.Parse(dr[13].ToString());
		count--;
		db.UpdateInto("T_Backpack",new string[] {"Count"}, 
						new string[] {count.ToString()},"ID",id.ToString());
		db.CloseSqlConnection();
	}

	//添加金币
	public void AddGold(int gold)
	{
		CreateDataBase();
		SqliteDataReader money = db.Select("T_Money","AccountID","1");
		money.Read();
		Debug.Log("money[1].ToString():" + money[1].ToString());
		CharacterTemplate.Instance.gold = int.Parse(money[1].ToString());
		CharacterTemplate.Instance.gold += gold;
		db.UpdateInto("T_Money",new string[]{"Gold"},
				new string[] {CharacterTemplate.Instance.gold.ToString()},"AccountID","1");
		db.CloseSqlConnection();
		UIManager.Instance.GetUI<UIMoeny>(UIName.UIMoeny).
					SetGold(CharacterTemplate.Instance.gold);
	}

	//更新装备状态
	public void UpdateEquipStatus(GoodsItem gi)
	{
		CreateDataBase();
		int i = Convert.ToInt32(gi.IsDressed);	//using System;
		db.UpdateInto("T_Backpack",new string[] {"Dressed"},
						new string[] {i.ToString()},"ID",gi.Goods.ID.ToString());
		db.CloseSqlConnection();
	}

	public void UpdateEquip(GoodsItem gi)
	{
		int i = Convert.ToInt32(gi.IsDressed);	//using System;
		db.UpdateInto("T_Backpack",new string[] {"Dressed"},
		new string[] {i.ToString()},"ID",gi.Goods.ID.ToString());
	}

}
