using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

	public Transform target;	//预设位置
	public GameObject prefab;	//HUD预设（血条）
	GameObject blood;			//血条
	UIFollowTarget uiFollowTarget;
	UISlider bloodSlider;
	bool isBlood;

	void Start () {
		StartCoroutine(GetHud());
	}

	IEnumerator GetHud()//
	{
		while(!isBlood)
		{
			if(HUDRoot.go == null)
			{
				HUDRoot.go = GameObject.FindGameObjectWithTag("HUD");
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				isBlood = true;
				//初始化血条
				InitBlood();
			}
		}
	}

	void InitBlood()
	{
		blood = NGUITools.AddChild(HUDRoot.go,prefab);
		//血条跟随
		uiFollowTarget = blood.AddComponent<UIFollowTarget>();
		uiFollowTarget.target = target;
		uiFollowTarget.uiCamera = 
			blood.transform.parent.parent.parent.GetComponent<Camera>();
		bloodSlider = blood.GetComponentInChildren<UISlider>();
	}

	public void SetBlood(float curhp, float maxhp)
	{
		if(curhp <= 0)
			Destroy(blood,3);	//销毁血条
		bloodSlider.value = curhp / maxhp;
	}


}
