using UnityEngine;
using System.Collections;

public class UIInfo : MonoBehaviour {

	private UIManager uiMagager;

	void Start () {
		Object obj = FindObjectOfType(typeof(UIManager));
		if(obj != null)
			uiMagager = obj as UIManager;
		if(uiMagager == null)
		{
			GameObject manager = new GameObject ("UIManager");
			uiMagager = manager.AddComponent<UIManager>();
			DontDestroyOnLoad(manager);
		}
		uiMagager.InitializeUIs();	//初始化
		uiMagager.SetStandAloneVisible(true);
		uiMagager.SetCreateRoleVisible(true);
		uiMagager.SetMainCityVisible(true);
		uiMagager.SetBattleVisible(true);
		uiMagager.SetLoginVisible(true);
	}
	

}
