using UnityEngine;
using System.Collections;

public class BackpackRoleEquip : MonoBehaviour {

	private UISprite mSprite;
	private GoodsItem goodsItem;
	private UISceneWidget mButton_RoleEquip;
	private UISprite Sprite {
		get {
			if (mSprite == null) {
				mSprite = Global.FindChild<UISprite>(transform, "Sprite_Icon");
			}
			return mSprite;
		}
	}

	void Awake()
	{
		mButton_RoleEquip = GetComponent<UISceneWidget>();
		if(mButton_RoleEquip)
			mButton_RoleEquip.OnMouseClick = this.ButtonRoleEquipOnClick;
	}

	//设置装备信息
	public void SetGoodsItem(GoodsItem gi) {
		if (gi == null) return;
		this.goodsItem = gi;
		Sprite.spriteName = gi.Goods.ICON;
	}

	private void ButtonRoleEquipOnClick(UISceneWidget eventObj)
	{
		Debug.Log("查看装备");
		if(goodsItem != null)
			UIBackpack.Instance.ShowGoodsItem(eventObj,goodsItem, false);
	}

	public void Clear()
	{
		goodsItem = null;
		Sprite.spriteName = "EquipBox";
	}



}
