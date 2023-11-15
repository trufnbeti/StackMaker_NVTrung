using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	[SerializeField] private UI[] listUI;
	[System.Serializable]
	private struct UI {
		public IdUI id;
		public GameObject unit;
	}
	[SerializeField] private Text coinTxt;
	[SerializeField] private Transform canvasParent;
	[SerializeField] private GameObject unmute;
	[SerializeField] private GameObject mute;
	[SerializeField] private Text levelTxt;
	
	private Dictionary<IdUI, GameObject> UIActive = new Dictionary<IdUI, GameObject>();
	private bool isBtnSettingToggle = false;
	private bool isEnableReset = true;

	private void Start() {
		ChangeLevel();
	}


	private void OnEnable() {
		EventManager.OnEventEmitted += OnEventEmitted;
	}

	private void OnDisable() {
		EventManager.OnEventEmitted -= OnEventEmitted;
	}

	public bool IsLoaded(IdUI id)
	{
		return UIActive.ContainsKey(id);
	}
	
	public GameObject GetUI(IdUI id)
	{
		if (!IsLoaded(id))
		{
			UIActive.Add(id, Instantiate(GetPrefab(id), canvasParent));
			// UIActive[id] = Instantiate(GetPrefab(id), canvasParent);
		}

		return UIActive[id];
	}
	
	public GameObject GetPrefab(IdUI id) {
		for (int i = 0; i < listUI.Length; ++i) {
			if (listUI[i].id == id) {
				return listUI[i].unit;
			}
		}
		return null;
	}
	
	public GameObject OpenUI(IdUI id)
	{
		GameObject canvas = GetUI(id);
		canvas.SetActive(true);
		return canvas;
	}
	
	public void CloseUI(IdUI id)
	{
		if (IsLoaded(id))
		{
			GetUI(id).SetActive(false);
		}
	}

	public void UpdateCoin(int coin) {
		if (coinTxt) {
			coinTxt.text = coin.ToString();
		}
	}

	public void BtnReplayClick() {
		if (!isEnableReset) return;
		EventManager.EmitEvent(EventID.Replay);
	}

	public void BtnSettingClick() {
		if (isBtnSettingToggle) {
			unmute.SetActive(false);
			mute.SetActive(false);
		} else {
			mute.SetActive(!SoundManager.Ins.IsMuted);
			unmute.SetActive(SoundManager.Ins.IsMuted);
		}
		
		SoundManager.Ins.Play(SoundType.BtnSetting);
		isBtnSettingToggle = !isBtnSettingToggle;
	}

	public void BtnSettingSoundClick(bool isMuted) {
		mute.SetActive(isMuted);
		unmute.SetActive(!isMuted);
		SoundManager.Ins.IsMuted = !isMuted;
	}

	public void ChangeLevel() {
		levelTxt.text = "Level: " + LevelManager.Ins.CurrentLevel;
	}
	private void OnEventEmitted(EventID eventID) {
		// switch (eventID) {
		// 	case EventID.NextLevel:
		// 		
		// }
	}

}
