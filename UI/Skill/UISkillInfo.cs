using UnityEngine;
using System.Collections;

public class UISkillInfo : MonoBehaviour {

	private UISprite mSprite_Icon;		//技能icon
	private GameObject mSprite_Light;	//蓝色sprite
	private UILabel mLabel_Name;		//技能名称
	private UILabel mLabel_lv;			//技能等级

	public int index;	//索引
		
	public void InitWidgets()
	{
		mSprite_Icon = 
			Global.FindChild(transform,"Sprite_Ico").GetComponent<UISprite>();
		mSprite_Light = Global.FindChild(transform,"Sprite_Light");
		mLabel_Name = Global.FindChild<UILabel>(transform,"Label_SkillName");
		mLabel_lv = Global.FindChild<UILabel>(transform,"Label_Level");
	}

	//设置技能icon
	public void SetSkillIcon(string icon)
	{
		if(mSprite_Icon != null)
			mSprite_Icon.spriteName = icon;
	}

	//获取技能icon
	public string GetSpriteName()
	{
		return mSprite_Icon.spriteName;
	}

	public void SetLight(bool isLigth)
	{
		if(mSprite_Light != null)
			mSprite_Light.SetActive(isLigth);
	}

	//设置技能名称
	public void SetSkillName(string name)
	{
		if(mLabel_Name != null)
			mLabel_Name.text = name;
	}

	//设置技能等级
	public void SetSkillLevel(int lv)
	{
		if(mLabel_lv != null)
			mLabel_lv.text = lv.ToString();
	}
}
