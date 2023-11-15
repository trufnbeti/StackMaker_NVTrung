using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTag {
	Player = 0,
	Wall = 1,
	Brick = 2,
	UnBrick = 3
}

public enum PoolType {
	PlayerBrick = 0
}

public enum SoundType {
	GetBrick,
	Finish,
	Move,
	OpenChest,
	OpenUI,
    ShowCoinUI,
    BtnSetting,
    GetCoin
}

public enum PlayerDir {
	Left = 0,
	Right = 1,
	Up = 2,
	Down = 3,
	None = 4
}

public enum Anim {
	idle,
	jump,
	win
}

public enum IdUI {
	EndLevel
}

public enum PrefKey {
	Coin
}
