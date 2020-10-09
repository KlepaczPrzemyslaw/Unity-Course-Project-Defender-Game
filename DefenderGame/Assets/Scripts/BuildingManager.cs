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

	[SerializeField]
	private Building hqBuilding = null;

	private BuildingTypeSO activeBuildingType;
	private BoxCollider2D colliderCache;
	private Collider2D[] collidersCache;
	private BuildingTypeHolder buildingTypeHolderCache;
	private string errorMessageCache = string.Empty;

	void Awake()
	{
		Instance = this;
		activeBuildingType = null;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) &&
			!EventSystem.current.IsPointerOverGameObject() &&
			activeBuildingType != null)
		{
			if (CanSpawnBuilding(activeBuildingType, UtilitiesClass.GetMouseWorldPosition(), out errorMessageCache) &&
				ResourceManager.Instance.CanAfford(activeBuildingType.constructionCostArray, out errorMessageCache))
			{
				ResourceManager.Instance.SpendResources(activeBuildingType.constructionCostArray);
				BuildingConstruction.Create(UtilitiesClass.GetMouseWorldPosition(), activeBuildingType);
			}
			else
			{
				TooltipUI.Instance.Show(errorMessageCache, true);
			}
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

	private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
	{
		// for null - return false
		if (buildingType == null)
		{
			errorMessage = "Unknown Error";
			return false;
		}

		// Get colliders
		colliderCache = buildingType.prefab.GetComponent<BoxCollider2D>();
		collidersCache = Physics2D.OverlapBoxAll(position + (Vector3)colliderCache.offset, colliderCache.size, 0);

		// If something under building -> false
		if (collidersCache.Length > 0)
		{
			errorMessage = "Area is not clear!";
			return false;
		}

		// Check if the same building in nearby
		collidersCache = Physics2D.OverlapCircleAll(position, buildingType.minConstrctionRadius);
		foreach (var collider in collidersCache)
		{
			if (collider.TryGetComponent(out buildingTypeHolderCache))
				if (buildingTypeHolderCache.buildingType == buildingType)
				{
					errorMessage = "Too close to another building of the same type!";
					return false;
				}
		}

		// Check if there is ANY building in nearby
		collidersCache = Physics2D.OverlapCircleAll(position, maxConstrctionRadius);
		foreach (var collider in collidersCache)
		{
			if (collider.TryGetComponent(out buildingTypeHolderCache))
			{
				// All requirements were fulfilled
				errorMessage = string.Empty;
				return true;
			}
		}

		// No building around
		errorMessage = "Too far from any other building!";
		return false;
	}

	public bool HqExist() => hqBuilding != null;
	public Building GetHqBuilding() => hqBuilding;
}
