using UnityEngine;
using System.Collections;

public class UISkillStatus : MonoBehaviour {

	private UISprite mSprite_Icon;	//技能图标
	private GameObject mSprite_Light;
	public int index;

	public void InitWidgets()
	{
		mSprite_Icon = 
			Global.FindChild(transform,"Sprite_Ico").GetComponent<UISprite>();
		mSprite_Light = Global.FindChild(transform,"Sprite_Light");
	}

	//修改技能icon
	public void SetIcon(string icon)
	{
		if(mSprite_Icon != null)
			mSprite_Icon.spriteName = icon;
	}

	public string GetIcon()
	{
		return mSprite_Icon.spriteName;
	}

	public void SetLight(bool isLight)
	{
		if(mSprite_Light != null)
			mSprite_Light.SetActive(isLight);
	}

}
