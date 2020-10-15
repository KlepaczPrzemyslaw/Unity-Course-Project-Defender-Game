using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
	[Header("Outside")]
	[SerializeField]
	private ResourceGenerator resourceGenerator = null;

	[Header("Own")]
	[SerializeField]
	private SpriteRenderer icon = null;

	[SerializeField]
	private Transform barScale = null;

	[SerializeField]
	private TextMeshPro text = null;

	private ResourceGeneratorData resourceGeneratorDataCache;
	private Vector3 barScaleVectorCache = new Vector3(0, 1, 1);

	void Start()
	{
		resourceGeneratorDataCache = resourceGenerator.GetResourceGeneratorData();
		icon.sprite = resourceGeneratorDataCache.resourceType.sprite;
		text.text = resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1");
	}

	void Update()
	{
		barScaleVectorCache.x = 1 - resourceGenerator.GetTimerNormalized();
		barScale.localScale = barScaleVectorCache;
	}
}
