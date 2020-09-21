using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	private BuildingTypeSO buildingType;
	private BuildingTypesSO availableBuildings;
	private Vector3 tempPosition;
	private Camera mainCamera;

	void Start()
	{
		mainCamera = Camera.main;
		availableBuildings = Resources.Load<BuildingTypesSO>(
			typeof(BuildingTypesSO).Name);
		buildingType = availableBuildings.List[0];
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			Instantiate(buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);

		if (Input.GetKeyDown(KeyCode.T))
			buildingType = availableBuildings.List[0];

		if (Input.GetKeyDown(KeyCode.Y))
			buildingType = availableBuildings.List[1];
	}

	private Vector3 GetMouseWorldPosition()
	{
		tempPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		tempPosition.z = 0f;
		return tempPosition;
	}
}
