using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public static ResourceManager Instance { get; protected set; }

	private Dictionary<ResourceTypeSO, int> resourceAmoutDic;

	void Awake()
	{
		Instance = this;
		resourceAmoutDic = new Dictionary<ResourceTypeSO, int>();
		Resources.Load<ResourceTypesSO>(typeof(ResourceTypesSO).Name)
			.List.ForEach(x => resourceAmoutDic.Add(x, 0));
	}

	public void AddResource(ResourceTypeSO resourceType, int amount) =>
		resourceAmoutDic[resourceType] += amount;
}
