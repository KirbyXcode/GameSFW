using UnityEngine;
using System.Collections;

public class UIPower : MonoBehaviour {

	private float startVal = 0;
	private int endVal = 0;
	private bool isStart = false;
	public float speed = 0.01f;	//一秒增加战斗力多少
	private UILabel mLabel_Power;
	private bool isUp = true;
	private TweenAlpha tween;	//alpha动画

	void Awake()
	{
		mLabel_Power = Global.FindChild<UILabel>(transform,"Label_Power");
//		tween = GetComponent<TweenAlpha>();
//		EventDelegate ed = new EventDelegate(this, "OnTweenFinished");	//添加事件
//		tween.onFinished.Add(ed);
//		gameObject.SetActive(false);
		mLabel_Power = Global.FindChild<UILabel>(transform,"Label_Power");
		mLabel_Power.text = CharacterTemplate.Instance.damageMax.ToString();
	}

	void Update () {
		if(isStart)
		{
			if(isUp)
			{
				startVal += speed * Time.deltaTime;
				if(startVal > endVal)
				{
					isStart = false;
					startVal = endVal;
//					tween.PlayReverse();//结束alpha动画
				}
			}
			else
			{
				startVal -= speed * Time.deltaTime;
				if(startVal < endVal)
				{
					isStart = false;
					startVal = endVal;
//					tween.PlayReverse();//结束alpha动画
				}
			}
			mLabel_Power.text = ((int)startVal).ToString();
		}
	}

	//设置战斗力
	public void ShowPowerChange(int startVal, int endVal)
	{
//		gameObject.SetActive(true);
//		tween.PlayForward();	//播放alpha动画
		this.startVal = startVal;
		this.endVal = endVal;
		if(endVal > startVal)
			isUp = true;
		else 
			isUp = false;
		isStart = true;
	}


//	public void OnTweenFinished() {
//		if (isStart == false) {
//			gameObject.SetActive(false);
//		}
//	}
}
