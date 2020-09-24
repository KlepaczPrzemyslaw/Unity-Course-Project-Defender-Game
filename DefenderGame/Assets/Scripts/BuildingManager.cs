using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
	public static BuildingManager Instance { get; protected set; }
	
	public event EventHandler<OnActiveBuildingTypeChangeArgs> OnActiveBuildingTypeChange;
	public class OnActiveBuildingTypeChangeArgs : EventArgs
	{
		public BuildingTypeSO activeBuildingType;
	}

	private BuildingTypeSO activeBuildingType;
	private BuildingTypesSO availableBuildings;

	void Awake()
	{
		Instance = this;
		availableBuildings = Resources.Load<BuildingTypesSO>(
			typeof(BuildingTypesSO).Name);
		activeBuildingType = null;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && 
			!EventSystem.current.IsPointerOverGameObject() &&
			activeBuildingType != null)
			Instantiate(activeBuildingType.prefab, UtilitiesClass.GetMouseWorldPosition(), Quaternion.identity);
	}

	public void ChangeActiveBuilding(BuildingTypeSO newType)
	{
		activeBuildingType = newType;
		OnActiveBuildingTypeChange?.Invoke(this, new OnActiveBuildingTypeChangeArgs
		{
			activeBuildingType = newType
		});
	}

	public BuildingTypeSO GetActiveBuildingType() => activeBuildingType;
}
