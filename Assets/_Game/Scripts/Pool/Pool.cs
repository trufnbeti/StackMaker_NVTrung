using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
	public PoolAmount[] poolAmounts;
	[System.Serializable]
	public struct PoolAmount {
		public PoolType type;
		public int amount;
		public PoolMember gameUnit;
	}
	private Dictionary<PoolType, Queue<PoolMember>> dict = new Dictionary<PoolType, Queue<PoolMember>>();

	public override void Awake() {
		base.Awake();
		OnInit();
	}
	private void OnInit() {
		for (int i = 0; i < poolAmounts.Length; i++) {
			if (!dict.ContainsKey(poolAmounts[i].type)) {
				dict[poolAmounts[i].type] = new Queue<PoolMember>();
			}

			for (int j = 0; j < poolAmounts[i].amount; j++) {
				PoolMember gameUnit = Instantiate(poolAmounts[i].gameUnit);
				gameUnit.gameObject.SetActive(false);
				dict[poolAmounts[i].type].Enqueue(gameUnit);
			}
		}
	}
	public PoolMember Spawn(PoolType poolType, Vector3 pos) {
		PoolMember gameUnit = dict[poolType].Count > 0 ? dict[poolType].Dequeue() : Instantiate(GetPrefab(poolType));

		gameUnit.tf.position = pos;
		gameUnit.gameObject.SetActive(true);

		return gameUnit;
	}

	public T Spawn<T>(PoolType poolType, Vector3 pos) where T : PoolMember {
		return Spawn(poolType, pos) as T;
	}

	public void Despawn(PoolType poolType, PoolMember gameUnit) {
		gameUnit.gameObject.SetActive(false);
		dict[poolType].Enqueue(gameUnit);
	}


	public PoolMember GetPrefab(PoolType poolType) {
		for (int i = 0; i < poolAmounts.Length; i++) {
			if (poolAmounts[i].type == poolType) {
				return poolAmounts[i].gameUnit;
			}
		}
		return null;
	}
}