using UnityEngine;
using System.Collections;

public class GoodsItem {

	private Goods goods;//物品
	private int level = 1;	//物品等级
	private int count = 1;	//物品个数
	private bool isDressed = false;	//是否装备着

	public Goods Goods {
		get {
			return goods;
		}
		set {
			goods = value;
		}
	}
	public int Level {
		get {
			return level;
		}
		set {
			level = value;
		}
	}
	public int Count {
		get {
			return count;
		}
		set {
			count = value;
		}
	}

	public bool IsDressed {
		get {
			return isDressed;
		}
		set {
			isDressed = value;
		}
	}

}
