using UnityEngine;

public class SceneOrderHandler : MonoBehaviour
{
	[SerializeField]
	private bool runOnlyOnce = true;

	[SerializeField]
	private float offsetForThisPart = 0f;

	private SpriteRenderer spriteRen;
	private const float precisionMultiplier = 10f;

	void Awake()
	{
		spriteRen = GetComponent<SpriteRenderer>();
	}

	private void LateUpdate()
	{
		spriteRen.sortingOrder = -Mathf.RoundToInt(
			(transform.position.y + offsetForThisPart) * precisionMultiplier);

		if (runOnlyOnce)
			Destroy(this);
	}
}
