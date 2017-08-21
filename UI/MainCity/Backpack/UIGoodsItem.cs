using UnityEngine;
using System.Collections;

public class UIGoodsItem : MonoBehaviour {

	private UISprite mSprite;
	private UILabel mLabel;
	public GoodsItem goodsItem;
	private UISceneWidget mButton_Goods;

	private UISprite Sprite {
		get {
			if (mSprite == null) {
				mSprite = Global.FindChild<UISprite>(transform,"Sprite_Item");
			}
			return mSprite;
		}
	}

	private UILabel Label {
		get {
			if (mLabel == null) {
				mLabel = Global.FindChild<UILabel>(transform,"Label");
			}
			return mLabel;
		}
	}

	void Awake()
	{
		mButton_Goods = GetComponent<UISceneWidget>();
		if(mButton_Goods)
			mButton_Goods.OnMouseClick = this.ButtonGoodsItemOnClick;
	}

	//设置物品信息
	public void SetGoodsItem( GoodsItem goodsItem ) {
		this.goodsItem = goodsItem;
		
		Sprite.spriteName = goodsItem.Goods.ICON;
		if (goodsItem.Count <= 1) {
			Label.text = "";
		} else {
			Label.text = goodsItem.Count.ToString();
		}
	}

	//清除物品信息
	public void Clear() {
		goodsItem = null;
		Label.text = "";
		Sprite.spriteName = "CunDangXiaoKuang";
	}


	private void ButtonGoodsItemOnClick(UISceneWidget eventObj)
	{
		Debug.Log("查看物品");
		if(goodsItem != null)
			UIBackpack.Instance.ShowGoodsItem(eventObj, goodsItem, true);
//		Debug.Log("goodsItem.Goods.ID:" + goodsItem.Goods.ID);
	}

	public void ChangeCount(int count) 
	{
		if(goodsItem.Count - count <= 0)
			Clear();
		else if(goodsItem.Count - count == 1)
		{
			mLabel.text = "";
		}
		else
		{
			mLabel.text = (goodsItem.Count - count).ToString();
		}
	}

}
