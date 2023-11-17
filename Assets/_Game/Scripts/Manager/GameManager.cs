using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public Player player;
	public CameraFollow m_camera;
	private int coin;
	private bool _isEndLevel = false;

	private Stack<GameObject> objResets = new Stack<GameObject>();

	private void Start() {
		coin = Pref.Coin;
		if (UIManager.Ins) {
			UIManager.Ins.UpdateCoin(coin);
		}
	}

	private void OnEnable() {
		EventManager.OnEventEmitted += OnEventEmitted;
	}

	private void OnDisable() {
		EventManager.OnEventEmitted -= OnEventEmitted;
	}

	private void OnEventEmitted(EventID eventID) {
		switch (eventID) {
			case EventID.Replay:
			case EventID.NextLevel:
				OnReset();
				break;
		}
	}
	
	public bool IsEndLevel {
		get => _isEndLevel;
		set => _isEndLevel = value;
	}

	public int Coin {
		get => coin;
		set => coin = value;
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
		_isEndLevel = false;
		while (objResets.Count > 0) {
			var obj = objResets.Pop();
			obj.SetActive(!obj.activeInHierarchy);
		}
	}
}
