﻿using UnityEngine;
[System.Serializable]
public abstract class SpriteSheet : ScriptableObject {
	public abstract Sprite Original { get; }

	public abstract Sprite Grey { get; }
}