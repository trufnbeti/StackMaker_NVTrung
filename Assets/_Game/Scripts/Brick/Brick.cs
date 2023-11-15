using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			GameManager.Ins.player.AddBrick();
			gameObject.SetActive(false);
			GameManager.Ins.AddToResetList(gameObject);
		}
	}
}
