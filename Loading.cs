using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	public UISlider mProgress;	//进度条
	AsyncOperation async;		//异步加载对象 0-1

	void Start () {
		StartCoroutine(LoadScene());
	}

	//读取进度
	IEnumerator LoadScene()
	{
		if(Global.Contain3DScene)//判断加载场景是否有3D场景
		{
			async = Application.LoadLevelAsync(Global.LoadSceneName);
			Application.LoadLevelAdditiveAsync(Global.LoadUIName);
		}
		else
		{
			async = Application.LoadLevelAsync(Global.LoadUIName);
		}
		yield return async;
	}

	void Update () {
		mProgress.value = async.progress;
	}
}
