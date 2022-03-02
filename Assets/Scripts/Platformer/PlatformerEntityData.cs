﻿using System;
using UnityEngine;

public enum PlatformerEntityId {
	Player,
	Grass,
	Dirt,
	Brick,
	Cloud,
	Gem,
	Key,
	Door,
	Ladybug,
}

public enum PlatformerColorId {
	Purple,
	MedGreen,
	DarkOrange,
	MedOrange,
	DarkGreen,
	LightGreen,
	Crimson,
	LightOrange,
	MedBlue,
	Brown,
	MedOrange2,
	LightOrange2,
	DarkBlue,
	Lavender,
	LightOrange3,
	Yellow,
}

[Serializable]
[CreateAssetMenu(
	fileName = nameof(PlatformerEntityData),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(EntityData) + "/" + nameof(PlatformerEntityData))
]
public class PlatformerEntityData : EntityData {
	[SerializeField]
	private PlatformerEntityId entityId;
	public override int EntityId => (int)entityId;

	[SerializeField]
	private PlatformerColorId colorId;
	public override int ColorId => (int)colorId;
}