using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTag.Player.ToString())) {
            SoundManager.Ins.Play(SoundType.Finish);
            GameManager.Ins.player.ClearBrick();
            GameManager.Ins.player.Win();
        }
    }
}
