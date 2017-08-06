using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DevelopEngine;
using Mono.Data.Sqlite;
using ARPGSimpleDemo.Skill;
using System;
using ARPGSimpleDemo.Character;
using LitJson;

public class FightingStatistics : MonoSingleton<FightingStatistics> {

	List<GameObject> enemyList;
	public int star;
	public int exp;
	SkillData sd;

	void Start () {
		enemyList = new List<GameObject>();
		InitEnemy();
	}

	//初始化敌人
	void InitEnemy()
	{

		//读取敌人信息
		OperatingDB.Instance.CreateDataBase();
		SqliteDataReader enemy = OperatingDB.Instance.db.Select("T_Scene","SceneName","11");
		int id = 0;
		while(enemy.Read())
		{
			id = enemy.GetInt32(enemy.GetOrdinal("Monster"));
		}
		SqliteDataReader skill = OperatingDB.Instance.db.Select("T_MonsterSkill","ID",id.ToString());
		while(skill.Read())
		{
			int i = 1;
			sd = new SkillData ();
			Type t = typeof(SkillData);
			foreach (var item in t.GetProperties()) 
			{ 
				if(item.PropertyType.Equals(typeof(string)))	
					item.SetValue(sd, skill[i].ToString(), null); 
				else if(item.PropertyType.Equals(typeof(float)))
					item.SetValue(sd, float.Parse(skill[i].ToString()), null);
				else if(item.PropertyType.Equals(typeof(string[])))
				{
					string[] str= skill[i].ToString().Split(',');
					item.SetValue(sd, str, null);
				}
				else
				{
					item.SetValue(sd, int.Parse(skill[i].ToString()), null); 
				}
				i++;
			} 
		}

		OperatingDB.Instance.db.CloseSqlConnection();
		AddEnemy();
	}

	void AddEnemy()
	{
		var enemyObj = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(var enemy in enemyObj)
		{	
			enemy.GetComponent<CharacterSkillManager>().skills.Add(sd);
			enemyList.Add(enemy);
		}
		Debug.Log("enemyList.Count:" + enemyList.Count);
	}

	//删除敌人
	public void DeleteEnemy(GameObject obj)
	{
		enemyList.Remove(obj);
		Debug.Log("enemyList.Count:" + enemyList.Count);
		if(enemyList.Count <= 0)
		{
			//游戏胜利
//			UIManager.Instance.SetVisible(UIName.UIVictory, true);
//			StartCoroutine(wait(2));
			if(GameMain.Instance.isOnline)
			{
				StartCoroutine(Results());
			}
			else
			{
				StartCoroutine(wait(2));
			}
		}
	}

	IEnumerator wait(float time)
	{
		yield return new WaitForSeconds(time);
		GetResults();
	}


	//单机版结算
	void GetResults()
	{
		UIMiniMap uiMiniMap = 
			UIManager.Instance.GetUI<UIMiniMap>(UIName.UIMiniMap);
		if(uiMiniMap.battleTime < 60)
		{
			star = 3;
		}
		else if(uiMiniMap.battleTime >= 60 && uiMiniMap.battleTime < 120)
		{
			star = 2;
		}
		else if(uiMiniMap.battleTime >= 120 && uiMiniMap.battleTime < 300)
		{
			star = 1;
		}

		OperatingDB.Instance.GetResultsDB(11,star);
	}


	//网络版结算
	IEnumerator Results()
	{
		UIMiniMap uiMiniMap = 
			UIManager.Instance.GetUI<UIMiniMap>(UIName.UIMiniMap);
		string resultsurl = 
			"http://172.164.0.10:800/SaveData.ashx?Ver=1&CharacterID=" +
				CharacterTemplate.Instance.characterId + "&RemainTime=" +
				uiMiniMap.battleTime + "&Scene=11";
		WWW www = new WWW (resultsurl);
		yield return www;
		if(www.error == null)
		{
			Debug.Log(www.text);
			JsonData json = JsonMapper.ToObject(www.text);
			star = int.Parse(json[0].ToString());
			exp = int.Parse(json[1].ToString());
			
			CharacterTemplate.Instance.characterId = int.Parse(json[2]["CharacterID"].ToString());
			CharacterTemplate.Instance.jobId = int.Parse(json[2]["JobID"].ToString());
			CharacterTemplate.Instance.lv = int.Parse(json[2]["Lv"].ToString());
			CharacterTemplate.Instance.expCur = int.Parse(json[2]["ExpCur"].ToString());
			CharacterTemplate.Instance.force = int.Parse(json[2]["Force"].ToString());
			CharacterTemplate.Instance.intellect = int.Parse(json[2]["Intellect"].ToString());
//			CharacterTemplate.Instance.speed = int.Parse(json[2]["Speed"].ToString());
			CharacterTemplate.Instance.maxHp = int.Parse(json[2]["MaxHP"].ToString());
			CharacterTemplate.Instance.maxMp = int.Parse(json[2]["MaxMP"].ToString());
			CharacterTemplate.Instance.damageMax = int.Parse(json[2]["DamageMax"].ToString());
			UIManager.Instance.SetVisible(UIName.UIVictory, true);
			UIVictory uiVictory = 
				UIManager.Instance.GetUI<UIVictory>(UIName.UIVictory);
			uiVictory.SetExp(exp);
			uiVictory.SetStar(star);
		}
		else{
			Debug.Log(www.error);
		}
		
	}
}
