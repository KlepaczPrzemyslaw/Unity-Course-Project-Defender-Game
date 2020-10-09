using UnityEngine;
using UnityEngine.UI;

public class DemolishButton : MonoBehaviour
{
	[SerializeField]
	private Button button = null;

	[SerializeField]
	private Building building = null;

	private ResourceAmount[] costAmount;

	void Awake() =>
		button.onClick.AddListener(OnClickHandler);

	void OnDestroy() =>
		button.onClick.RemoveAllListeners();

	private void OnClickHandler()
	{
		costAmount = building.GetComponent<BuildingTypeHolder>()
			.buildingType.constructionCostArray;
		foreach (var resource in costAmount)
		{
			ResourceManager.Instance.AddResource(
				resource.ResourceType,
				Mathf.FloorToInt(resource.Amount / 2));
		}

		Destroy(building.gameObject);
	}
}
