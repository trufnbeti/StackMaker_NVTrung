using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour {
    [SerializeField] private GameObject boxCoin;
    [SerializeField] private Text coinTxt;
    
    private int valueToAdd = 0;
    private int speed;

    public void BtnAdsClick() {
        valueToAdd = Constant.COINS_PER_ADS;
        speed = 5;
        ClickDone();
    }

    public void BtnNextLevel() {
        valueToAdd = Constant.COINS_PER_LEVEL;
        speed = 1;
        ClickDone();
    }

    private void ClickDone() {
        boxCoin.SetActive(true);
        SoundManager.Ins.Play(SoundType.GetCoin);
        StartCoroutine(CountCoins(valueToAdd));
        StartCoroutine(OnReset());
    }

    private IEnumerator OnReset() {
        yield return new WaitForSecondsRealtime(2.5f);
        LevelManager.Ins.NextLevel();
        UIManager.Ins.CloseUI(IdUI.EndLevel);
    }

    private IEnumerator CountCoins(int value) {
        yield return new WaitForSecondsRealtime(0.25f);
        
        for (int i = 0; i < value / speed; i++) {
            if (GameManager.Ins) {
                GameManager.Ins.AddCoin(speed);
                coinTxt.text = GameManager.Ins.Coin.ToString();
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
        }

        boxCoin.SetActive(false);
    }
    
}
