using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
	private void Update() {
		// Sử dụng Touch
		// if (Input.touchCount > 0) {
		// 	touch = Input.GetTouch(0);
		// }
		//
		// if (touch.phase == TouchPhase.Began) {
		// 	startTouchPos = touch.position;
		// }
		//
		// if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended) {
		// 	endTouchPos = touch.position;
		//
		// 	if (Mathf.Abs(endTouchPos.y - startTouchPos.y) > Mathf.Abs(endTouchPos.x - startTouchPos.x)) {
		// 		if (endTouchPos.y > startTouchPos.y) {
		// 			Debug.Log("UP");
		// 			transform.Translate(new Vector3(0, 0, 0.5f));
		// 		} else {
		// 			Debug.Log("down");
		// 			transform.Translate(new Vector3(0, 0, -0.5f));
		// 		}
		// 	} else {
		// 		if (endTouchPos.x > startTouchPos.x) {
		// 			Debug.Log("right");
		// 			transform.Translate(new Vector3(0.5f, 0, 0));
		// 		} else {
		// 			Debug.Log("left");
		// 			transform.Translate(new Vector3(-0.5f, 0, 0));
		// 		}
		// 	}
		// }
		//===========================
		
	}
}
