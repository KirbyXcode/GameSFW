using UnityEngine;
using System.Collections;
using DevelopEngine;
using System.Collections.Generic;
using ARPGSimpleDemo.Skill;
using ARPGSimpleDemo.Character;

public class UISkillSelect : UIScene {

	private UISceneWidget mButton_Closed;	//关闭按钮
	private UISkillStatus[] skillStatus;	//按钮	
	private UISkillInfo[] skillInfo;		//开关
	private List<SkillData> skillData;
	private UILabel mLabel_SkillDes;		//技能描述
	bool isSelect = false;
	public int skillId;		//技能ID

	protected override void Start () {
		base.Start();
		skillStatus = GetComponentsInChildren<UISkillStatus>();
		skillInfo = GetComponentsInChildren<UISkillInfo>();
//		Debug.Log("skillStatus:" + skillStatus.Length);
//		Debug.Log("skillInfo:" + skillInfo.Length);
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;
		mLabel_SkillDes = Global.FindChild<UILabel>(transform,"Label_SkillDes");

		for(int i = 0; i < skillStatus.Length; ++i)
		{
			UISceneWidget mButton_Skill = GetWidget("Button_Skill" + (i+1));
			if(mButton_Skill != null)
				mButton_Skill.OnMouseClick = this.ButtonSkillOnClick;
			skillStatus[i] = mButton_Skill.GetComponent<UISkillStatus>();
			skillStatus[i].InitWidgets();	//初始化技能
			skillStatus[i].SetIcon(SkillManager.Instance.mSkillInfo[i].skillIcon);//设置技能icon
			skillStatus[i].index = i;	//设置索引
		}

		skillData = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSkillManager>().skills;

		for(int i = 0; i < skillInfo.Length; i++)
		{
			UISceneWidget mButton_Skill = GetWidget("Button" + (i+1));
			if(mButton_Skill != null)
				mButton_Skill.OnMouseClick = this.SkillOnClick;
			skillInfo[i] = mButton_Skill.GetComponent<UISkillInfo>();
			skillInfo[i].InitWidgets();	//初始化
			skillInfo[i].SetLight(false);
			skillInfo[i].SetSkillIcon(skillData[i + 3].skillIcon);//设置技能icon
			skillInfo[i].SetSkillName(skillData[i + 3].name);//设置技能名称
			skillInfo[i].SetSkillLevel(skillData[i + 3].level);//设置技能等级
			skillInfo[i].index = i;
		}
		SetLight(false);
	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}

	private void ButtonSkillOnClick(UISceneWidget eventObj)
	{
		Debug.Log("eventObj:" + eventObj);
		if(!isSelect) return;
		SetLight(false);
		for(int i = 0; i < skillInfo.Length; i++)
		{
			skillInfo[i].SetLight(false);
		}
		isSelect = false;
		UISkillStatus ss = eventObj.GetComponent<UISkillStatus>();
		//设置技能icon
		SetSkillIcon(ss.index, skillId);
		SetSkillDes(string.Empty);
	}

	//设置技能Icon
	private void SetSkillIcon(int index, int id)
	{
		Debug.Log("index:" + index);
		Debug.Log("skillId:" + id);
		SkillInfo si = new SkillInfo ();
		si.id = id;
		si.skillIcon = skillInfo[id - 1].GetSpriteName();//设置技能icon
		SkillManager.Instance.mSkillInfo.SetValue(si,index);
		Debug.Log("SkillManager.Instance.mSkillInfo[" + index + "]" +
		          SkillManager.Instance.mSkillInfo[index].skillIcon);
        skillStatus[index].SetIcon(
			SkillManager.Instance.mSkillInfo[index].skillIcon);

		for(int i = 0; i < 4; ++i)
		{
			//清除之前的技能信息
			if(skillStatus[i].GetIcon() == 
			   SkillManager.Instance.mSkillInfo[index].skillIcon &&
			   skillStatus[i] != skillStatus[index])
			{
				SkillManager.Instance.mSkillInfo[i].id = 0;
				SkillManager.Instance.mSkillInfo[i].skillIcon = "HeiSeChenDi";
				skillStatus[i].SetIcon("HeiSeChenDi");
				Debug.Log("SkillManager.Instance.mSkillInfo[i].id:" + SkillManager.Instance.mSkillInfo[i].id);
			}
		}
    }

	private void SkillOnClick(UISceneWidget eventObj)
	{
		Debug.Log("eventObj:" + eventObj);
		isSelect = true;
		SetLight(true);	//设置右侧选择图片精灵显示
		UISkillInfo si = eventObj.GetComponent<UISkillInfo>();
		skillInfo[si.index].SetLight(true);
		skillId = si.index + 1;
		SetSkillDes(skillData[si.index + 3].description);
		Debug.Log("SkillId:" + skillId);
	}

	//按钮精灵显示隐藏
	private void SetLight(bool isLight)
	{
		for(int i = 0; i < 4; ++i)
		{
			skillStatus[i].SetLight(isLight);
		}

	}

	//设置技能描述
	void SetSkillDes(string des)
	{
		Debug.Log("des:" + des);
		if(mLabel_SkillDes != null)
			mLabel_SkillDes.text = des;
	}

}
