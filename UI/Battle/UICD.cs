using UnityEngine;
using System.Collections;

public class UICD : MonoBehaviour {

	private UISprite mSprite_Icon;	//技能图片
	public UISprite mSprite_CD;	//技能冷却图片
	public int index;		//索引
	public bool isSelect;	//是否选择技能
	private float coolTime;
	private float coolRemain;

	public void InitWidgets()
	{
		mSprite_Icon = 
			Global.FindChild(transform, "Sprite_Ico").GetComponent<UISprite>(); 
		mSprite_CD = 
			Global.FindChild(transform, "Sprite_CD").GetComponent<UISprite>(); 
	}

	public void SetIcon(string icon)
	{
		if(mSprite_Icon != null)
			mSprite_Icon.spriteName = icon;
	}

}
