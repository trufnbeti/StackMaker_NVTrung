using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
	    if (other.CompareTag(GameTag.Player.ToString())) {
		    GameManager.Ins.IsEndLevel = true;
		    GameManager.Ins.m_camera.RaceToWin();
		    GameManager.Ins.player.RaceToWin();
	    }
}
}
