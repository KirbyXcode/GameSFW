using UnityEngine;
using System.Collections;

public class UIPlayerInfo : UIScene {

	private UILabel mLabel_Name;
	private UILabel mLabel_lv;
	private UILabel mLabel_Hp;
	private UILabel mLabel_Mp;

	protected override void Start () {
		base.Start();
		mLabel_Name = Global.FindChild(transform,"Label_PlayerName").
						GetComponent<UILabel>();
		mLabel_lv = Global.FindChild(transform,"Label_Level").
			GetComponent<UILabel>();
		mLabel_Hp = Global.FindChild(transform,"Label_HP").
			GetComponent<UILabel>();
		mLabel_Mp = Global.FindChild(transform,"Label_Magic").
			GetComponent<UILabel>();
		InitPlayerInfo();
	}

	//设置玩家名称
	public void SetName(string name)
	{
		if(mLabel_Name != null)
			mLabel_Name.text = name;
	}

	public void SetLv(int lv)
	{
		if(mLabel_lv != null)
			mLabel_lv.text = lv.ToString();
	}

	public void SetHp(int hp)
	{
		if(mLabel_Hp != null)
			mLabel_Hp.text = hp.ToString();
	}

	public void SetMp(int mp)
	{
		if(mLabel_Mp != null)
			mLabel_Mp.text = mp.ToString();
	}

	public void InitPlayerInfo()
	{
		SetName(CharacterTemplate.Instance.name);
		SetLv(CharacterTemplate.Instance.lv);
		SetHp(CharacterTemplate.Instance.maxHp);
		SetMp(CharacterTemplate.Instance.maxMp);
	}

}
