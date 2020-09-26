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

	[SerializeField]
	private float maxConstrctionRadius = 25f;

	private BuildingTypeSO activeBuildingType;
	private BuildingTypesSO availableBuildings;
	private BoxCollider2D colliderCache;
	private Collider2D[] collidersCache;
	private BuildingTypeHolder buildingTypeHolderCache;

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
			activeBuildingType != null &&
			CanSpawnBuilding(activeBuildingType, UtilitiesClass.GetMouseWorldPosition()) &&
			ResourceManager.Instance.CanAfford(activeBuildingType.constructionCostArray))
		{
			ResourceManager.Instance.SpendResources(activeBuildingType.constructionCostArray);
			Instantiate(activeBuildingType.prefab, UtilitiesClass.GetMouseWorldPosition(), Quaternion.identity);
		}
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

	private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
	{
		// for null - return false
		if (buildingType == null)
			return false;

		// Get colliders
		colliderCache = buildingType.prefab.GetComponent<BoxCollider2D>();
		collidersCache = Physics2D.OverlapBoxAll(position + (Vector3)colliderCache.offset, colliderCache.size, 0);

		// If something under building -> false
		if (collidersCache.Length > 0)
			return false;

		// Check if the same building in nearby
		collidersCache = Physics2D.OverlapCircleAll(position, buildingType.minConstrctionRadius);
		foreach (var collider in collidersCache)
		{
			if (collider.TryGetComponent(out buildingTypeHolderCache))
				if (buildingTypeHolderCache.buildingType == buildingType)
					return false;
		}

		// Check if there is ANY building in nearby
		collidersCache = Physics2D.OverlapCircleAll(position, maxConstrctionRadius);
		foreach (var collider in collidersCache)
		{
			if (collider.TryGetComponent(out buildingTypeHolderCache))
			{
				// All requirements were fulfilled
				return true;
			}
		}

		// No building around
		return false;
	}
}
