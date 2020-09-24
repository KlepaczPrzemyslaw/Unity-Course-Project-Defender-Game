using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingPickerUI : MonoBehaviour
{
	[SerializeField]
	private Transform pickBuildingPrefab = null;

	[SerializeField]
	private float moveByPixels = 160f;

	[SerializeField]
	private Sprite arrowSprite = null;

	private List<Button> buttons;
	private Dictionary<BuildingTypeSO, Image> referenceForSelected;
	private Image arrowImageRef;
	private BuildingTypeSO buildingCache;
	private Vector2 deltaForArrow = new Vector2(0, -40f);

	void Awake()
	{
		// Init
		int index = 0;
		buttons = new List<Button>();
		referenceForSelected = new Dictionary<BuildingTypeSO, Image>();

		// Building UI
		PlaceMouseSpriteAsFirstOption();
		// First button is placed - index have to be updated
		index++;
		// Rest buttons
		BuildRestOfUI(index);
	}

	void OnDestroy() => buttons.ForEach(x => x.onClick.RemoveAllListeners());

	void Start() => UpdateActiveBuildingType();

	private void UpdateActiveBuildingType()
	{
		arrowImageRef.enabled = false;
		foreach (var selectedImage in referenceForSelected.Keys)
		{
			referenceForSelected[selectedImage].enabled = false;
		}

		buildingCache = BuildingManager.Instance.GetActiveBuildingType();
		if (buildingCache == null)
			arrowImageRef.enabled = true;
		else
			referenceForSelected[buildingCache].enabled = true;
	}

	private void PlaceMouseSpriteAsFirstOption()
	{
		// Instantiate
		var currentInstance = Instantiate(pickBuildingPrefab, transform);

		// Update -> Position / Image / Text
		currentInstance.GetComponent<RectTransform>().anchoredPosition =
			new Vector2(0, 0);
		currentInstance.Find("BuildingImage")
			.GetComponent<Image>().sprite = arrowSprite;
		currentInstance.Find("BuildingImage")
			.GetComponent<RectTransform>().sizeDelta = deltaForArrow;

		// Events
		buttons.Add(currentInstance.GetComponent<Button>());
		buttons[0].onClick.AddListener(() =>
		{
			BuildingManager.Instance.ChangeActiveBuilding(null);
			UpdateActiveBuildingType();
		});

		arrowImageRef = currentInstance
			.Find("Selected").GetComponent<Image>();
	}

	private void BuildRestOfUI(int providedIndex)
	{
		int i = providedIndex;
		Resources.Load<BuildingTypesSO>(
			typeof(BuildingTypesSO).Name).List.ForEach(x =>
			{
				// Instantiate
				var currentInstance = Instantiate(pickBuildingPrefab, transform);

				// Update -> Position / Image / Text
				currentInstance.GetComponent<RectTransform>().anchoredPosition =
					new Vector2(moveByPixels * i, 0);
				currentInstance.Find("BuildingImage")
					.GetComponent<Image>().sprite = x.sprite;

				// Events
				buttons.Add(currentInstance.GetComponent<Button>());
				buttons[i].onClick.AddListener(() =>
				{
					BuildingManager.Instance.ChangeActiveBuilding(x);
					UpdateActiveBuildingType();
				});

				referenceForSelected.Add(x, currentInstance
					.Find("Selected").GetComponent<Image>());

				i++;
			});
	}
}
