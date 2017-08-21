using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIVictory : UIScene {

	private UISceneWidget mButton_Exit;
	private UILabel mLabel_Exp;	//经验
	private GameObject[] mStar;	//星星

	void Awake()
	{
		InitWidget();
	}

	private void InitWidget()
	{
		mButton_Exit = GetWidget("Button_Closed");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mLabel_Exp = Global.FindChild<UILabel>(transform,"Label_GetExpValue");
		mStar = new GameObject[3];
		for(int i = 0; i < 3; i++)
		{
			mStar[i] = Global.FindChild(transform,"Sprite_Star" + (i+1));
		}
	}

	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadUIMainCity"),
			Configuration.GetContent("Scene","LoadMainCity")));
	}

	public void SetExp(int exp)
	{
		if(mLabel_Exp != null)
			mLabel_Exp.text = exp.ToString();
	}

	public void SetStar(int star)
	{

		if(star > 3 || star < 1)
		{
			for(int i = 0; i < 3; i++)
			{
				mStar[i].SetActive(false);
			}
		}
		else
		{
			for(int i = 0; i < 3; i++)
			{
				if(i < star)
				{
					mStar[i].SetActive(true);
				}
				else
				{
					mStar[i].SetActive(false);
				}
			}
		}
	}


}
