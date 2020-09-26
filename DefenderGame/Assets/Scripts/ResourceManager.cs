using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public static ResourceManager Instance { get; protected set; }
	public event EventHandler OnResourceAmountChanged;

	private Dictionary<ResourceTypeSO, int> resourceAmoutDic;

	void Awake()
	{
		Instance = this;
		resourceAmoutDic = new Dictionary<ResourceTypeSO, int>();
		Resources.Load<ResourceTypesSO>(typeof(ResourceTypesSO).Name)
			.List.ForEach(x => resourceAmoutDic.Add(x, 0));
	}

	public void AddResource(ResourceTypeSO resourceType, int amount)
	{
		resourceAmoutDic[resourceType] += amount;
		OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
	}

	public int GetResourceAmount(ResourceTypeSO resource) =>
		resourceAmoutDic[resource];

	public bool CanAfford(ResourceAmount[] resourceAmounts)
	{
		foreach (var resourceAmount in resourceAmounts)
			if (GetResourceAmount(resourceAmount.ResourceType) < resourceAmount.Amount)
				return false;

		return true;
	}

	public void SpendResources(ResourceAmount[] resourceAmounts)
	{
		foreach (var resourceAmount in resourceAmounts)
			resourceAmoutDic[resourceAmount.ResourceType] -= resourceAmount.Amount;
	}
}
