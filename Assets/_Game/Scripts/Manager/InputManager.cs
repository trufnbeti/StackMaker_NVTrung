using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {

	private Vector2 startTouchPos, endTouchPos;
	private Vector3 directionMove;
	private bool _canMove = false;

	public bool CanMove {
		get => _canMove;
		set => _canMove = value;
	}

	public Vector3 DirectionMove => directionMove;
	
	private PlayerDir MoveDirection() {
		if (Vector2.Distance(startTouchPos, endTouchPos) < Constant.ERROR_VALUE) {
			return PlayerDir.None;
		}
		if (Mathf.Abs(endTouchPos.y - startTouchPos.y) > Mathf.Abs(endTouchPos.x - startTouchPos.x)) {
			if (endTouchPos.y > startTouchPos.y) {
				return PlayerDir.Up;
			} else {
				return PlayerDir.Down;
			}
		} else {
			if (endTouchPos.x > startTouchPos.x) {
				return PlayerDir.Right;
			} else {
				return PlayerDir.Left;
			}
		}

	}
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
		if (_canMove || GameManager.Ins.IsEndLevel) return;
		
		if (Input.GetMouseButtonDown(0)) {
			startTouchPos = Input.mousePosition;
		}
		
		if (Input.GetMouseButtonUp(0)) {
			endTouchPos = Input.mousePosition;
			switch (MoveDirection()) {
				case PlayerDir.Up:
					directionMove = Vector3.forward;
					break;
				case PlayerDir.Down:
					directionMove = Vector3.forward * -1;
					break;
				case PlayerDir.Right:
					directionMove =  Vector3.right;
					break;
				case PlayerDir.Left:
					directionMove = Vector3.right * -1;
					break;
			}
			if (MoveDirection() != PlayerDir.None) {
				SoundManager.Ins.Play(SoundType.Move);
				_canMove = true;
			}
		}
		
	}
}
