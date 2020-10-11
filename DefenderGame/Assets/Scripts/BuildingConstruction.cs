using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
	private static BuildingConstruction buildingCache;
	public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
	{
		buildingCache = Instantiate(Resources.Load<Transform>(typeof(BuildingConstruction).Name),
			position, Quaternion.identity).GetComponent<BuildingConstruction>();
		buildingCache.Setup(buildingType);
		return buildingCache;
	}

	private float constructionTimer;
	private float constructionTimerMax;
	private BuildingTypeSO buildingType;
	private BoxCollider2D colli;
	private SpriteRenderer spriteRenderer;
	private BuildingTypeHolder typeHolder;
	private Material constMaterial;

	void Awake()
	{
		colli = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		typeHolder = GetComponent<BuildingTypeHolder>();
		constMaterial = spriteRenderer.material;
	}

	void Update()
	{
		constMaterial.SetFloat("_Progress",
			GetTimerNormalized());

		constructionTimer -= Time.deltaTime;
		if (constructionTimer <= 0f)
		{
			Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	public float GetTimerNormalized() => 1 - (constructionTimer / constructionTimerMax);

	private void Setup(BuildingTypeSO building)
	{
		buildingType = building;
		constructionTimerMax = building.construcionTimerMax;
		constructionTimer = building.construcionTimerMax;
		colli.offset = building.prefab.GetComponent<BoxCollider2D>().offset;
		colli.size = building.prefab.GetComponent<BoxCollider2D>().size;
		spriteRenderer.sprite = building.sprite;
		typeHolder.buildingType = building;
	}
}
