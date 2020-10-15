using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
public class BuildingTypesSO : ScriptableObject
{
	public List<BuildingTypeSO> List;
}
