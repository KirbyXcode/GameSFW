using UnityEngine;
using System.Collections;

public class UIBattleHead : UIScene {
	public static UIBattleHead instance;

	private UILabel mLabel_Name;
	private UILabel mLabel_lv;
	private UILabel mLabel_Hp;
	private UILabel mLabel_Mp;
	private UISlider mSlider_Hp;
	private UISlider mSlider_Mp;

	void Awake()
	{
		instance = this;
	}

	protected override void Start () {
		base.Start();
		InitWidget();
		InitRoleInfo();
	}

	private void InitWidget()
	{
		mLabel_Name = 
			Global.FindChild(transform,"Label_PlayerName").GetComponent<UILabel>();
		mLabel_lv = 
			Global.FindChild(transform,"Label_Level").GetComponent<UILabel>();
		mLabel_Hp = 
			Global.FindChild(transform,"Label_HP").GetComponent<UILabel>();
		mLabel_Mp = 
			Global.FindChild(transform,"Label_Magic").GetComponent<UILabel>();
		mSlider_Hp = 
			Global.FindChild(transform,"Bar_HP").GetComponent<UISlider>();
		mSlider_Mp = 
			Global.FindChild(transform,"Bar_Magic").GetComponent<UISlider>();
	}

	private void SetName(string name)
	{
		if(mLabel_Name != null)
			mLabel_Name.text = name;
	}

	private void SetLv(int lv)
	{
		if(mLabel_lv != null)
			mLabel_lv.text = lv.ToString();
	}

	public void SetHp(float curHp, float maxHp)
	{
		if(mSlider_Hp != null)
			mSlider_Hp.value = curHp / maxHp;
		if(mLabel_Hp != null)
			mLabel_Hp.text = curHp + "/" + maxHp;
	}

	public void SetMp(float curMp, float maxMp)
	{
		if(mSlider_Mp != null)
			mSlider_Mp.value = curMp / maxMp;
		if(mLabel_Mp != null)
			mLabel_Mp.text = curMp + "/" + maxMp;
	}

	private void InitRoleInfo()
	{
		SetName(CharacterTemplate.Instance.name);
		SetLv(CharacterTemplate.Instance.lv);
		SetHp(CharacterTemplate.Instance.maxHp,CharacterTemplate.Instance.maxHp);
		SetMp(CharacterTemplate.Instance.maxMp,CharacterTemplate.Instance.maxMp);
	}

}
