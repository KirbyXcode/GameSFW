using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	GameObject player;		//玩家

	public UITexture map_Texture;	//小地图
	public GameObject map_Plyaer;	//黄色箭头

	float mapWidth;		//地形宽度
	float mapHeight;	//地形高度
	float terrain_posX;	//地形X轴
	float terrain_posZ;	//地形z轴
	float curWidth;		//地图宽度
	float curHeight;	//地图高度
	float rotation_Y;	//角度
	Terrain terrain;
	float x;
	float z;

	void Start () {
		InitTerrain();
		player = GameObject.FindGameObjectWithTag("Player");
		float size_x = terrain.terrainData.size.x;		//地形X轴长度
		float scal_x = terrain.transform.localScale.x;	//地形X轴缩放
		mapWidth = size_x * scal_x;
		float size_z = terrain.terrainData.size.z;
		float scal_z = terrain.transform.localScale.z;
		mapHeight = size_z * scal_z;
		terrain_posX = terrain.transform.localPosition.x;
		terrain_posZ = terrain.transform.localPosition.z;

	}
	
	void InitTerrain()
	{
		Object obj = FindObjectOfType(typeof(Terrain));
		if(obj != null)
			terrain = obj as Terrain;
	}

	void Check()
	{
		if(player == null)
			return;
		x = player.transform.position.x;	//玩家
		z = player.transform.position.z;

		curWidth = map_Texture.width;		//小地图宽度
		curHeight = map_Texture.height;		//小地图高度
		SetPlayerForward();
		SetMapPos(x,z);
	}

	//旋转
	void SetPlayerForward()
	{
		rotation_Y = player.transform.rotation.eulerAngles.y;

		map_Plyaer.transform.localRotation = Quaternion.Euler(0,0,-rotation_Y);
	}

	void SetMapPos(float x, float z)
	{
		float mapPos_x = curWidth/2 - curWidth/mapWidth * (x - terrain_posX);
		float mapPos_z = curHeight/2 - curHeight/mapHeight * (z - terrain_posZ);
		map_Texture.transform.localPosition = 
			new Vector3 (mapPos_x,mapPos_z,0);
	}

	void Update () {
		Check();
	}
}
