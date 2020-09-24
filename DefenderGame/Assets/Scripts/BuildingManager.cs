using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
	public static BuildingManager Instance { get; protected set; }

	private BuildingTypeSO activeBuildingType;
	private BuildingTypesSO availableBuildings;
	private Vector3 tempPosition;
	private Camera mainCamera;

	void Awake()
	{
		Instance = this;
		availableBuildings = Resources.Load<BuildingTypesSO>(
			typeof(BuildingTypesSO).Name);
		activeBuildingType = null;
	}

	void Start() => mainCamera = Camera.main;

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && 
			!EventSystem.current.IsPointerOverGameObject() &&
			activeBuildingType != null)
			Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
	}

	public void ChangeActiveBuilding(BuildingTypeSO newType) => activeBuildingType = newType;

	private Vector3 GetMouseWorldPosition()
	{
		tempPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		tempPosition.z = 0f;
		return tempPosition;
	}

	public BuildingTypeSO GetActiveBuildingType() => activeBuildingType;
}
