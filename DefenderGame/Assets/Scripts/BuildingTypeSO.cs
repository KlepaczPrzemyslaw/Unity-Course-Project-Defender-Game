﻿using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
	public string buildingName;
	public Transform prefab;
	public ResourceGeneratorData generatorData;
}
