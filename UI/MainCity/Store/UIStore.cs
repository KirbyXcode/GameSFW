using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStore : UIScene {
	public static UIStore Instance;

	private UISceneWidget mButton_Closed;
	public List<UISellGoods> uIGoodsList = new List<UISellGoods>();	
	private GameObject root;
	private UIGrid grid;
	private UISceneWidget[] mButton;
	private string objName;	//按钮名称
	private Vector3 v3;		//商品列表初始位置
	private SpringPanel springPanel;	//复位商品列表

	protected override void Start () {
		base.Start();
		Instance = this;
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;
		root = Global.FindChild(transform,"Grid");
		grid = root.GetComponent<UIGrid>();
		InitGoods();
		mButton = Global.FindChild(transform,"Grid_Button").GetComponentsInChildren<UISceneWidget>();
		for(int i = 0; i < mButton.Length; i++)
		{
			mButton[i].OnMouseClick = this.ButtonGoodsOnClick;
		}
		v3 = new Vector3 (-295f,125.9f,0);

	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		ResetList();
		SetVisible(false);
	}

	void InitGoods()
	{
		foreach(var item in StoreManager.Instance.weaponList)
		{
			GameObject obj = Instantiate(Resources.Load("Sprite"),
			                             root.transform.position,root.transform.rotation) as GameObject;
			obj.transform.parent = root.transform;
			obj.transform.localScale = new Vector3 (1,1,1);
			uIGoodsList.Add(obj.GetComponent<UISellGoods>());
			obj.GetComponent<UISellGoods>().Show(item);
		}
//		Debug.Log("sList:" + uIGoodsList.Count);
		//列表添加后用于刷新Grid
		grid.repositionNow = true;
		grid.maxPerLine = 2;
		grid.Reposition();
	}

	private void ButtonGoodsOnClick(UISceneWidget eventObj)
	{
		ResetList();
		if(objName == eventObj.name)
			return;
		objName = eventObj.name;
		int id = int.Parse(objName.Substring(objName.Length -1)) - 1;
		Debug.Log("id:" + id);
		Show(id);
	}


	public void Show(int id)
	{
		for(int i = 0; i < StoreManager.Instance.goodsList[id].Count; i++)
		{
			if((i+1) > uIGoodsList.Count)	//如果商品数量大于UI数量，则增加ui格子
			{
				GameObject obj = Instantiate(Resources.Load("Sprite"),
				                             root.transform.position,root.transform.rotation) as GameObject;
				obj.transform.parent = root.transform;
				obj.transform.localScale = new Vector3 (1,1,1);
				uIGoodsList.Add(obj.GetComponent<UISellGoods>());
				//列表添加后用于刷新Grid
				grid.repositionNow = true;
				grid.maxPerLine = 2;
				grid.Reposition();
			}
			uIGoodsList[i].gameObject.SetActive(true);
			uIGoodsList[i].Show(StoreManager.Instance.goodsList[id][i]);
		}

		if(StoreManager.Instance.goodsList[id].Count < uIGoodsList.Count)
		{
			for(int i = StoreManager.Instance.goodsList[id].Count; i < uIGoodsList.Count; i++)
			{
				uIGoodsList[i].gameObject.SetActive(false);
			}
		}
	}

	//复位商品列表位置
	void ResetList()
	{
		if(springPanel == null)
			springPanel = Global.FindChild(transform,"Scroll View").AddComponent<SpringPanel>();
		springPanel.target = v3;
		springPanel.strength = 100;
		springPanel.enabled = true;
	}

}
