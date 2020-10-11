using UnityEngine;
using UnityEngine.UI;

public class FixBuilding : MonoBehaviour
{
	[Header("Internal")]
	[SerializeField]
	private Button button = null;

	[SerializeField]
	private ResourceTypeSO requiedResource = null;

	[Header("External")]
	[SerializeField]
	private HealthSystem healthSystem = null;

	// TooltipMessage
	private string messageForTooltip; 

	// Cache
	private int repairCostCache;
	private ResourceAmount[] resourceCache =
		new ResourceAmount[1] { new ResourceAmount { ResourceType = null, Amount = 0 } };

	void Awake() =>
		button.onClick.AddListener(OnClickHandler);

	void OnDestroy() =>
		button.onClick.RemoveAllListeners();

	private void OnClickHandler()
	{
		repairCostCache = healthSystem.MissingHealth() / 2;
		resourceCache[0].Amount = repairCostCache;
		resourceCache[0].ResourceType = requiedResource;

		if (ResourceManager.Instance.CanAfford(resourceCache, out messageForTooltip))
		{
			ResourceManager.Instance.SpendResources(resourceCache);
			healthSystem.HealFull();
		}
		else
		{
			messageForTooltip += $" You need {repairCostCache} pcs of gold!";
			TooltipUI.Instance.Show(messageForTooltip, true);
		}
	}
}
