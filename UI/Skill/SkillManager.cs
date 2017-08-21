using UnityEngine;
using System.Collections;
using DevelopEngine;

public class SkillInfo{
	public int id;
	public string skillIcon = "";
}

public class SkillManager : MonoSingleton<SkillManager> {

	public SkillInfo[] mSkillInfo = new SkillInfo[4] ;

	public void InitSkill()
	{
		SkillInfo info = new SkillInfo ();
		info.id = 0;
		info.skillIcon = "HeiSeChenDi";

		for(int i = 0; i < mSkillInfo.Length; ++i)
		{
			mSkillInfo[i] = info;
		}
	}
}
