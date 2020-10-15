using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
	[SerializeField]
	private bool runOnce = true;

	[SerializeField]
	private float positionOffsetY = 0;

	private SpriteRenderer spriteRenderer;
	private const float precisionMultiplier = 10f;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void LateUpdate()
	{
		spriteRenderer.sortingOrder = -Mathf.RoundToInt(
			(transform.position.y + positionOffsetY) * precisionMultiplier);

		if (runOnce)
			Destroy(this);
	}
}
