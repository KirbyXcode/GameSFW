using UnityEngine;
using System.Collections;
using DevelopEngine;
using LitJson;

public class UIBattle : UIScene {

	private UISceneWidget mButton_Closed;
	private UISceneWidget mButton_Battle;

	protected override void Start () {
		base.Start();
		mButton_Battle = GetWidget("Button_Stage");
		if(mButton_Battle != null)
			mButton_Battle.OnMouseClick = this.ButtonBattleOnClick;
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;
	}
	
	private void ButtonBattleOnClick(UISceneWidget eventObj)
	{
		Debug.Log("进入战斗！");
//		StartCoroutine(GameMain.Instance.LoadScene(
//			Configuration.GetContent("Scene","LoadUIBattle"),
//			Configuration.GetContent("Scene","LoadBattle")));

		if(GameMain.Instance.isOnline)
		{
			StartCoroutine(SendScene());
		}
		else
		{
			StartCoroutine(GameMain.Instance.LoadScene(
				Configuration.GetContent("Scene","LoadUIBattle"),
				Configuration.GetContent("Scene","LoadBattle")));
		}
	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		Debug.Log("关闭");
		SetVisible(false);
	}
	
	IEnumerator SendScene()
	{
		string sceneurl = "http://172.164.0.10:800/InScene.ashx?CharacterID=" + 
			CharacterTemplate.Instance.characterId + "&Scene=11";
		WWW www = new WWW (sceneurl);
		yield return www;
		if(www.error == null)
		{
			JsonData json = JsonMapper.ToObject(www.text);
			CharacterTemplate.Instance.characterId = int.Parse(json["CharacterID"].ToString());
			CharacterTemplate.Instance.jobId = int.Parse(json["JobID"].ToString());
			CharacterTemplate.Instance.lv = int.Parse(json["Lv"].ToString());
			CharacterTemplate.Instance.expCur = int.Parse(json["ExpCur"].ToString());
			CharacterTemplate.Instance.force = int.Parse(json["Force"].ToString());
			CharacterTemplate.Instance.intellect = int.Parse(json["Intellect"].ToString());
//			CharacterTemplate.Instance.speed = int.Parse(json["Speed"].ToString());
			CharacterTemplate.Instance.maxHp = int.Parse(json["MaxHP"].ToString());
			CharacterTemplate.Instance.maxMp = int.Parse(json["MaxMP"].ToString());
			CharacterTemplate.Instance.damageMax = int.Parse(json["DamageMax"].ToString());
			StartCoroutine(GameMain.Instance.LoadScene(
				Configuration.GetContent("Scene","LoadUIBattle"),
				Configuration.GetContent("Scene","LoadBattle")));
		}
		else
		{
			Debug.Log(www.error);
		}
	}
}
