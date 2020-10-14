using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour
{
	[SerializeField]
	private TextMeshPro text = null;

	[SerializeField]
	private SpriteRenderer icon = null;

	private ResourceGeneratorData resourceGeneratorData;
	private int nerbyResAmountCache;

	void Awake()
	{
		Hide();
	}

	void Update()
	{
		nerbyResAmountCache = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, 
			transform.position - transform.localPosition);
		text.text = $"{Mathf.RoundToInt((float)nerbyResAmountCache / resourceGeneratorData.maxResourcesAmount * 100f)}%";
	}

	public void Show(ResourceGeneratorData resGenData)
	{
		resourceGeneratorData = resGenData;
		gameObject.SetActive(true);

		icon.sprite = resGenData.resourceType.sprite;
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
