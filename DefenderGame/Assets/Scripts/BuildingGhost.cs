using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
	[SerializeField]
	private GameObject spriteGameObject = null;

	private SpriteRenderer spriteCache;

	void Awake()
	{
		spriteCache = spriteGameObject
			.GetComponent<SpriteRenderer>();
		Hide();
	}

	void Start()
	{
		transform.position = UtilitiesClass.GetMouseWorldPosition();
		BuildingManager.Instance.OnActiveBuildingTypeChange += Instance_OnActiveBuildingTypeChange;
	}

	void OnDestroy()
	{
		BuildingManager.Instance.OnActiveBuildingTypeChange -= Instance_OnActiveBuildingTypeChange;
	}

	void Update()
	{
		transform.position = UtilitiesClass.GetMouseWorldPosition();
	}

	private void Instance_OnActiveBuildingTypeChange(object sender,
		BuildingManager.OnActiveBuildingTypeChangeArgs e)
	{
		if (e.activeBuildingType == null)
			Hide();
		else
			Show(e.activeBuildingType.sprite);
	}

	private void Show(Sprite ghostSprite)
	{
		spriteCache.sprite = ghostSprite;
		spriteGameObject.SetActive(true);
	}

	private void Hide() => spriteGameObject.SetActive(false);
}
