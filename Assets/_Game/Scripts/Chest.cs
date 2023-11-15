using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestOpened;

    private void OnTriggerEnter(Collider other) {
        SoundManager.Ins.Play(SoundType.OpenChest);
        StartCoroutine(ShowEndUI());
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
