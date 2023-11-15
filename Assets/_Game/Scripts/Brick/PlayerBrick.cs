using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrick : PoolMember
{
	[SerializeField] private MeshRenderer meshRenderer;
	public static float BRICK_HEIGHT;

	private void Awake() {
		BRICK_HEIGHT = meshRenderer.bounds.size.y;
	}
}
