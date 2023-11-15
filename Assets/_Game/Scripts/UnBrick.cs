using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour {
	[SerializeField] private GameObject _yellow;
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			if (!GameManager.Ins.player.IsEmpty()) {
				GameManager.Ins.player.DropBrick();
				gameObject.SetActive(false);
				_yellow.SetActive(true);
				GameManager.Ins.AddToResetList(_yellow);
				GameManager.Ins.AddToResetList(gameObject);
			} else {
				GameManager.Ins.player.StopMoving();
			}
		}
	}
}