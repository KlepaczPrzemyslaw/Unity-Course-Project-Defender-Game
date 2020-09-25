using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
	private float timer;
	private float timerMax;
	private ResourceGeneratorData generatorData;

	private Collider2D[] collidersCache;
	private ResourceNode resourceCache;
	private int nearbyResourceNodes;

	void Start()
	{
		generatorData = GetComponent<BuildingTypeHolder>().buildingType.generatorData;
		timerMax = generatorData.timerMax;

		collidersCache = Physics2D.OverlapCircleAll(transform.position,
			generatorData.resourceDetectionRadius);
		nearbyResourceNodes = 0;
		foreach (var collider in collidersCache)
		{
			if (collider.TryGetComponent(out resourceCache))
			{
				// Skip if wrong type
				if (resourceCache.ResourceType != generatorData.resourceType)
					continue;

				// Add source
				nearbyResourceNodes++;

				// Break if max reached
				if (nearbyResourceNodes >= generatorData.maxResourcesAmount)
					break;
			}
		}

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

		Debug.Log($"{nearbyResourceNodes} gives: {1 / timerMax} pcs. {timerMax}");
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
}
