using UnityEngine;
using System.Collections;
using ARPGSimpleDemo.Character;	//角色控制器

public class UIBattleOption : UIScene {
	public static UIBattleOption instance;

	private UISceneWidget mButton_Attack;
	private UISprite[] skillSprite;	//cd
	private UICD[] cdGrid;	//技能按钮
	private float[] cdTime;	//CD
	private float[] nowTime;	//CD冷却

	GameObject player;
	private CharacterInputController chInput;
	private bool isPress;	//是否按住
	private CharacterSkillManager chSkill; 

	void Awake()
	{
		instance = this;
	}

	protected override void Start () {
		base.Start();
		mButton_Attack = GetWidget("BaseSkill");
		if(mButton_Attack != null)
		{
			mButton_Attack.OnMouseClick = this.ButtonAttackOnClick;	//单击按钮
			mButton_Attack.OnMousePress = this.ButtonAttackOnPress;	//按下按钮
		}
		player = GameObject.FindGameObjectWithTag("Player");
		chInput = player.GetComponent<CharacterInputController>();	//玩家输入控制
		chSkill = player.GetComponent<CharacterSkillManager>();

		cdGrid = GetComponentsInChildren<UICD>();
		Debug.Log("cdGrid.Length" + cdGrid.Length);
		skillSprite = new UISprite[cdGrid.Length];
		cdTime = new float[cdGrid.Length];
		nowTime = new float[cdGrid.Length];
		for(int i = 0; i < cdGrid.Length; ++i) 
		{
			UISceneWidget mButton_Skil = GetWidget("Skill" + (i+1));
			if(mButton_Skil != null)
				mButton_Skil.OnMouseClick = this.ButtonSkillOnClick;

			cdGrid[i] = mButton_Skil.GetComponent<UICD>();//初始化技能按钮数组
			cdGrid[i].index = i;
			cdGrid[i].InitWidgets();	//初始化技能icon
			cdGrid[i].SetIcon(SkillManager.Instance.mSkillInfo[i].skillIcon);//设置技能icon
			skillSprite[i] = cdGrid[i].mSprite_CD;//初始化cd数组
			mButton_Skil.Throughtime = GetSkillCD(SkillManager.Instance.mSkillInfo[i].id);
			cdTime[i] = nowTime[i] = (mButton_Skil.Throughtime + 1f);
			Debug.Log(mButton_Skil.Throughtime);
		}

	}

	private void ButtonAttackOnClick(UISceneWidget eventObj)
	{
		Debug.Log("普通攻击");
		chInput.On_ButtonDown(eventObj.name);
	}

	private void ButtonAttackOnPress(UISceneWidget eventObj,bool isDown)
	{
		Debug.Log("连续攻击");
		isPress = isDown;	//按下时为true，连续攻击在Update中执行
//		Debug.Log("isPress:" + isPress);
	}

	private void ButtonSkillOnClick(UISceneWidget eveneObj)
	{
		UICD uicd = eveneObj.GetComponent<UICD>();
		uicd.isSelect = true;

//		Debug.Log("SkillManager.Instance.mSkillInfo[" + uicd.index + "].id:=" 
//		          + SkillManager.Instance.mSkillInfo[uicd.index].id);
		if(SkillManager.Instance.mSkillInfo[uicd.index].id != 0)
		{
			chInput.On_ButtonDown("Skill" + SkillManager.Instance.mSkillInfo[uicd.index].id);
		}

	}

	protected override void Update()
	{
		base.Update();
		SetCoolTime();
		if(isPress)	//如果按住普通攻击按钮
			ButtonAttackOnClick(mButton_Attack);
	}

	float GetSkillCD(int skillid)
	{
		float cd = 0;
		for(int i = 3; i < chSkill.skills.Count; i++)
		{
			if(chSkill.skills[i].skillID == skillid)
			{
				cd = chSkill.skills[i].coolTime;
			}
		}
		return cd;
	}

	//设置CD
	private void SetCoolTime()
	{
		for(int i = 0; i < skillSprite.Length; i++)
		{
			if(cdGrid[i].isSelect)
			{
				if(nowTime[i] > 0)
				{
					nowTime[i] -= Time.deltaTime;
					skillSprite[i].fillAmount = nowTime[i] / cdTime[i];
				}
				else
				{
					cdGrid[i].isSelect = false;
					nowTime[i] = cdTime[i];
				}
				
			}
		}
	}

}
