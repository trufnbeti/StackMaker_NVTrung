using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : Singleton<LevelManager> {

	[SerializeField] private Level[] levels;
	
	private int _currentLevel;
	private Level _currentLvlObj;
	private Transform _startPoint;

	public Transform StartPoint => _startPoint;

	public int CurrentLevel => _currentLevel;

	public override void Awake() {
		base.Awake();
		_currentLevel = Pref.Level;
		_currentLvlObj = Instantiate(levels[_currentLevel - 1]);
		_startPoint = _currentLvlObj.startPoint;
	}

	public void NextLevel() {
		int nextLevel = _currentLevel % levels.Length + 1;
		_currentLevel = nextLevel;
		Pref.Level = _currentLevel;
		Destroy(_currentLvlObj.gameObject);
		_currentLvlObj = Instantiate(levels[_currentLevel - 1]);
		_startPoint = levels[_currentLevel - 1].startPoint;
		UIManager.Ins.ChangeLevel();
		EventManager.EmitEvent(EventID.NextLevel);
	}
}
