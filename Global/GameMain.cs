using UnityEngine;
using System.Collections;
using DevelopEngine;
using System.IO;

public class GameMain : MonoSingleton<GameMain> {

	public readonly string configPath = "/config.txt";
	private AsyncOperation async;
//	public DbAccess db;	//数据库
	public string appDBPath;//数据库路径
	public const string registerurl = "http://172.164.0.10:800/Register.ashx";
	public const string loginurl = "http://172.164.0.10:800/Login.ashx";
	public const string createRoleurl = "http://172.164.0.10:800/CreateCharacter.ashx";
	public const string scnenurl = "http://172.164.0.10:800/InScene.ashx";
	public const string resultsurl = "http://172.164.0.10:800/SaveData.ashx";

	public bool isOnline = false;	//false单机版
	
	IEnumerator Start () {
		//限帧
		Application.targetFrameRate = 45;
		DontDestroyOnLoad(gameObject);
		//初始化技能
		SkillManager.Instance.InitSkill();
		//读取配置文件
		Configuration.LoadConfig(configPath);
		while(!Configuration.IsDone)
			yield return null;
		//读取数据库
		StartCoroutine(CreateDataBase());
		//加载游戏开始场景
		string uiscene = Configuration.GetContent("Scene","LoadGameStart");
		StartCoroutine(LoadScene(uiscene));
	}

	/// <summary>
	/// 加载进度条场景
	/// </summary>
	IEnumerator Load()
	{
		async = Application.LoadLevelAsync("Loading");
		yield return async;
	}

	
	/// <summary>
	/// 加载场景
	/// </summary>
	public IEnumerator LoadScene(string uiScene)
	{
		Global.Contain3DScene = false;
		Global.LoadUIName = uiScene;
		yield return StartCoroutine(Load());
	}

	public IEnumerator LoadScene(string uiScene, string scene)
	{
		Global.Contain3DScene = true;
		Global.LoadUIName = uiScene;
		Global.LoadSceneName = scene;
		yield return StartCoroutine(Load ());
	}


	/// <summary>
	///	创建数据库
	/// </summary>
	IEnumerator CreateDataBase()
	{
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		appDBPath = Application.streamingAssetsPath + "/ARPG.db";
		#elif UNITY_ANDROID || UNITY_IPHONE
		appDBPath = Application.persistentDataPath + "/ARPG.db";
		if(!File.Exists(appDBPath))
		{
			StartCoroutine(CopyDB());
		}
		#endif
//		db = new DbAccess ("URI=file:" + appDBPath);
		yield return appDBPath;
	}

	/// <summary>
	///	拷贝数据库
	/// </summary>
	IEnumerator CopyDB()
	{
		string loadPath = string.Empty;
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		loadPath = Application.streamingAssetsPath + "/ARPG.db";
		#elif UNITY_ANDROID
		loadPath = "jar:file://" + Application.dataPath + "!/assets" + "/ARPG.db";
		#elif UNITY_IPHONE
		loadPath = + Application.dataPath + "/Raw" + "/ARPG.db";
		#endif
		WWW www = new WWW (loadPath);
		yield return www;
		File.WriteAllBytes(appDBPath, www.bytes);
	}

	/// <summary>
	///	打开数据库
	/// </summary>
	public DbAccess OpenDB(DbAccess db)
	{
		db = new DbAccess ("URI=file:" + appDBPath);
		return db;
	}


}
