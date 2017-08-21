using UnityEngine;
using System.Collections;

public class UIMoeny : UIScene {

	private UILabel mLabel_Money1;
	private UILabel mLabel_Money2;

	protected override void Start () {
		base.Start();
		mLabel_Money1 = Global.FindChild(transform,"Label_Money1Value").
							GetComponent<UILabel>();
		mLabel_Money2 = Global.FindChild(transform,"Label_Money2Value").
							GetComponent<UILabel>();
		InitMoney();
	}

	public void SetGold(int gold)
	{
		if(mLabel_Money1 != null)
			mLabel_Money1.text = gold.ToString();
	}

	public void SetDiamond(int diamond)
	{
		if(mLabel_Money2 != null)
			mLabel_Money2.text = diamond.ToString();
	}

	public void InitMoney()
	{
		OperatingDB.Instance.FindMoneyDB();
	}
	

}
