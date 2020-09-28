using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
	private float timer;
	private float timerMax;
	private ResourceGeneratorData generatorData;
	private int nearbyResourceNodes;

	#region Static

	private static int nearbyResourceCache;
	private static Collider2D[] collidersCache;
	private static ResourceNode resourceCache;
	public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
	{
		collidersCache = Physics2D.OverlapCircleAll(position,
			resourceGeneratorData.resourceDetectionRadius);

		nearbyResourceCache = 0;
		foreach (var collider in collidersCache)
		{
			if (collider.TryGetComponent(out resourceCache))
			{
				// Skip if wrong type
				if (resourceCache.ResourceType != resourceGeneratorData.resourceType)
					continue;

				// Add source
				nearbyResourceCache++;

				// Break if max reached
				if (nearbyResourceCache >= resourceGeneratorData.maxResourcesAmount)
					break;
			}
		}

		return nearbyResourceCache;
	}

	#endregion

	void Awake()
	{
		generatorData = GetComponent<BuildingTypeHolder>().buildingType.generatorData;
		timerMax = generatorData.timerMax;
	}

	void Start()
	{
		// Get Nodes
		nearbyResourceNodes = GetNearbyResourceAmount(generatorData, transform.position);

		// Script update
		if (nearbyResourceNodes == 0)
		{
			enabled = false;
		}
		else
		{
			timerMax = (generatorData.timerMax / 2f) +
				generatorData.timerMax *
				(1 - (float)nearbyResourceNodes / generatorData.maxResourcesAmount);
		}
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			// Add
			ResourceManager.Instance.AddResource(
				generatorData.resourceType, 1);

			// Reset Timer
			timer += timerMax;
		}
	}

	public ResourceGeneratorData GetResourceGeneratorData() => generatorData;

	public float GetTimerNormalized() => timer / timerMax;

	public float GetAmountGeneratedPerSecond() => 1f / timerMax;
}
