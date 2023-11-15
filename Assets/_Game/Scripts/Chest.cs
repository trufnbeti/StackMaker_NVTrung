using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestOpened;

    private void OnEnable() {
        EventManager.OnEventEmitted += OnEventEmitted;
    }
    private void OnDisable() {
        EventManager.OnEventEmitted -= OnEventEmitted;
    }

    private void OnEventEmitted(EventID eventID) {
        switch (eventID) {
            case EventID.WinLevel:
                SoundManager.Ins.Play(SoundType.OpenChest);
                StartCoroutine(ShowEndUI());
                break;
            case EventID.CompleteLevel:
                SoundManager.Ins.Play(SoundType.ShowCoinUI);
                break;
        }
    }


    private void OnTriggerEnter(Collider other) {
        EventManager.EmitEvent(EventID.CompleteLevel);
    }

    private IEnumerator ShowEndUI() {
        yield return new WaitForSecondsRealtime(2f);
        gameObject.SetActive(false);
        chestOpened.SetActive(true);
        GameManager.Ins.AddToResetList(gameObject);
        GameManager.Ins.AddToResetList(chestOpened);
        UIManager.Ins.OpenUI(IdUI.EndLevel);
        SoundManager.Ins.Play(SoundType.OpenUI);
    }

    

}
