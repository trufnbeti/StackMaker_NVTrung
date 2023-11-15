using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public Player player;
	public CameraFollow m_camera;
	private bool isEndLevel = false;
	private int coin;

	private Stack<GameObject> objResets = new Stack<GameObject>();

	public override void Awake() {
		base.Awake();
	}

	private void Start() {
		player.transform.position = LevelManager.Ins.StartPoint.position;
		Pref.Coin = 0;
		coin = Pref.Coin;
		if (UIManager.Ins) {
			UIManager.Ins.UpdateCoin(coin);
		}
	}

	public int Coin {
		get => coin;
		set => coin = value;
	}
	
	public bool IsEndLevel {
		get => isEndLevel;
		set => isEndLevel = value;
	}

	public void AddCoin(int value) {
		coin += value;
		Pref.Coin = coin;
		if (UIManager.Ins) {
			UIManager.Ins.UpdateCoin(coin);
		}
	}

	public void AddToResetList(GameObject item) {
		objResets.Push(item);
	}

	public void OnReset() {
		isEndLevel = false;
		m_camera.OnReset();
		player.OnReset();
		while (objResets.Count > 0) {
			var obj = objResets.Pop();
			obj.SetActive(!obj.activeInHierarchy);
		}

		player.transform.position = LevelManager.Ins.StartPoint.position;
	}
}
