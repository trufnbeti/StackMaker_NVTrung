using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private Animator anim;
	[SerializeField] private Transform sprite;
	[SerializeField] private LayerMask wallLayer;
	[SerializeField] private float moveSpeed;
	[SerializeField] private Transform playerModel;

	private Stack<PlayerBrick> bricks = new Stack<PlayerBrick>();

	private Vector3 directionMove;
	private Vector2 startTouchPos, endTouchPos;
	private float maxDistance = Mathf.Infinity;
	private Vector3 targetPos;
	// Sử dụng Touch
	// private Touch touch;
	//============

	private bool isMoving = false;
	
	private string currentAnim;
	
	private void Start() {
		transform.position = LevelManager.Ins.StartPoint.position;
	}

	private void OnEnable() {
		EventManager.OnEventEmitted += OnEventEmitted;
	}

	private void OnDisable() {
		EventManager.OnEventEmitted -= OnEventEmitted;
	}

	private void OnEventEmitted(EventID eventID) {
		switch (eventID) {
			case EventID.Replay:
			case EventID.NextLevel:
				OnReset();
				break;
			case EventID.WinningLevel:
				RaceToWin();
				break;
			case EventID.WinLevel:
				SoundManager.Ins.Play(SoundType.Finish);
				ClearBrick();
				Win();
				break;
		}
	}

	public void OnReset() {
		isMoving = false;
		ClearBrick();
		sprite.rotation = Quaternion.Euler(180, -30, -180);
		ResetAnim();
		transform.position = LevelManager.Ins.StartPoint.position;

	}

	public bool IsEmpty() {
		return bricks.Count <= 0;
	}

	public void StopMoving() {
		InputManager.Ins.CanMove = false;
		GameManager.Ins.IsEndLevel = true;
		StartCoroutine(ReplayGame());
	}

	public void AddBrick() {
		if (bricks.Count > 0) {
			SoundManager.Ins.Play(SoundType.GetBrick);
		}
		Jump();
		playerModel.position += Vector3.up * PlayerBrick.BRICK_HEIGHT;
		PlayerBrick playerBrick = Pool.Ins.Spawn<PlayerBrick>(PoolType.PlayerBrick, transform.position);
		// GameObject playerBrick = Instantiate(playerBrickPrefab, transform);
		playerBrick.transform.SetParent(playerModel);
		bricks.Push(playerBrick);
	}

	public void DropBrick() {
		SoundManager.Ins.Play(SoundType.GetBrick);
		playerModel.position -= Vector3.up * PlayerBrick.BRICK_HEIGHT;
		var playerBrick = bricks.Pop();
		Pool.Ins.Despawn(PoolType.PlayerBrick, playerBrick);
	}

	public void ClearBrick() {
		while (bricks.Count > 0) {
			DropBrick();
		}
	}

	public void RaceToWin() {
		sprite.rotation = Quaternion.Euler(180, -180, -180);
	}

	public void Win() {
		ChangeAnim(Anim.win);
	}
	
	private void ChangeAnim(Anim ani) {
		string animName = ani.ToString();
		if (currentAnim != animName) {
			
			anim.ResetTrigger(animName);

			if(currentAnim != null) {
				anim.ResetTrigger(currentAnim);
			}

			currentAnim = animName;
			anim.SetTrigger(currentAnim);
		}
	}

	private IEnumerator ReplayGame() {
		yield return new WaitForSecondsRealtime(1f);
		EventManager.EmitEvent(EventID.Replay);
	}

	private void OnDrawGizmos()
	{
		RaycastHit hit;

		bool isHit = Physics.Raycast(transform.position, directionMove, out hit, maxDistance, wallLayer);

		if (isHit)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, directionMove * hit.distance);
		}
		else
		{
			Gizmos.color = Color.green;
			Gizmos.DrawRay(transform.position, directionMove * maxDistance);
		}
	}

	private void CheckMove() {
		RaycastHit hit;

		bool isHit = Physics.Raycast(transform.position, InputManager.Ins.DirectionMove, out hit, maxDistance, wallLayer);
		
		if (isHit)
		{
			targetPos =(transform.position + InputManager.Ins.DirectionMove * (hit.distance) - directionMove * 0.5f);
			if (InputManager.Ins.CanMove) {
				Move();
			}
		}
	}

	private PlayerDir MoveDirection() {
		if (Vector3.Distance(startTouchPos, endTouchPos) < Constant.ERROR_VALUE) {
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

	private void ResetAnim() {
		ChangeAnim(Anim.idle);
	}
	
	private void Jump() {
		ChangeAnim(Anim.jump);
		Invoke(nameof(ResetAnim), 0.5f);
	}

	private void Move() {
		transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.position, targetPos) < Constant.ERROR_VALUE) {
			InputManager.Ins.CanMove = false;
		}
	}
	
	private void Update() {
		CheckMove();
		// if (InputManager.Ins.CanMove || GameManager.Ins.IsEndLevel) return;
		if (!InputManager.Ins.CanMove) return;
		directionMove = InputManager.Ins.DirectionMove;
		Debug.Log(directionMove);

		
	}
}
