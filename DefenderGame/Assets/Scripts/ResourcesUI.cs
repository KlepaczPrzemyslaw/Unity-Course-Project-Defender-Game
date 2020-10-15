using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
	[SerializeField]
	private Transform resourceTemplate = null;

	[SerializeField]
	private float moveByPixels = -160f;

	private Dictionary<ResourceTypeSO, TextMeshProUGUI> resourceText;
	private ResourceTypesSO resourceTypesSO;

	void Awake()
	{
		resourceText = new Dictionary<ResourceTypeSO, TextMeshProUGUI>();
	}

	void OnDestroy()
	{
		ResourceManager.Instance.OnResourceAmountChanged -= ResourceManager_OnResourceAmountChanged;
	}

	void Start()
	{
		// Event
		ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;

		// Init
		int i = 0;
		resourceTypesSO = Resources.Load<ResourceTypesSO>(typeof(ResourceTypesSO).Name);
		resourceTypesSO.List.ForEach(x =>
			{
				// Instantiate
				var currentInstance = Instantiate(resourceTemplate, transform);

				// Update -> Position / Image / Text
				currentInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(moveByPixels * i, 0);
				currentInstance.Find("ResImage").GetComponent<Image>().sprite = x.sprite;
				resourceText.Add(x, currentInstance.Find("ResText").GetComponent<TextMeshProUGUI>());
				resourceText[x].SetText(ResourceManager.Instance.GetResourceAmount(x).ToString());

				i++;
			});
	}

	private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
	{
		foreach (var resourceType in resourceTypesSO.List)
		{
			resourceText[resourceType].SetText(
				ResourceManager.Instance.GetResourceAmount(resourceType).ToString());
		}
	}
}
