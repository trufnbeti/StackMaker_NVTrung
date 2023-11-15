using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrick : PoolMember
{
	[SerializeField] private MeshRenderer meshRenderer;
	public const float BRICK_HEIGHT = 0.25f;

	// private void Awake() {
	// 	BRICK_HEIGHT = meshRenderer.bounds.size.y;
	// }
}
