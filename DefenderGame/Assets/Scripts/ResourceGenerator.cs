using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
	private float timer;
	private float timerMax;
	private BuildingTypeSO buildingType;

	void Start()
	{
		buildingType = GetComponent<BuildingTypeHolder>().buildingType;
		timerMax = buildingType.generatorData.timerMax;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			// Add
			ResourceManager.Instance.AddResource(
				buildingType.generatorData.resourceType, 1);

			// Reset Timer
			timer += timerMax;
		}
	}
}
