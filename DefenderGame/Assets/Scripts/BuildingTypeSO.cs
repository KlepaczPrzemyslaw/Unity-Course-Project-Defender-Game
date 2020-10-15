using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
	public string buildingName;
	public Transform prefab;
	public bool isGenerator;
	public ResourceGeneratorData generatorData;
	public Sprite sprite;
	public float minConstrctionRadius;
	public ResourceAmount[] constructionCostArray;
	public int maxHealthAmount;
	public float construcionTimerMax;

	private string tempCache;

	public string GetConstructionCost()
	{
		tempCache = string.Empty;
		foreach (var constConst in constructionCostArray)
		{
			tempCache += $"<color=#{constConst.ResourceType.colorInHex}>{constConst.ResourceType.nameShort}:{constConst.Amount}</color> "; 
		}
		return tempCache.Trim();
	}
}
