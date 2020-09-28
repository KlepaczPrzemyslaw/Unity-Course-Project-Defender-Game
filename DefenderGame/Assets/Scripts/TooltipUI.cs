using UnityEngine;
using TMPro;
using System.Collections;

public class TooltipUI : MonoBehaviour
{
	public static TooltipUI Instance { get; protected set; }

	[Header("Outside")]
	[SerializeField]
	private RectTransform generalCanvas = null;

	[Header("Inside")]
	[SerializeField]
	private TextMeshProUGUI textMeshPro = null;

	[SerializeField]
	private RectTransform textMeshProTransform = null;

	[SerializeField]
	private RectTransform bgImage = null;

	private Vector2 paddingCache = Vector2.zero;
	private RectTransform thisRectTrans;
	private Vector2 validationCache = Vector2.zero;

	void Awake()
	{
		Instance = this;
		thisRectTrans = GetComponent<RectTransform>();
		Hide();
	}

	void Update()
	{
		ValidateTooltipPosition();
		thisRectTrans.anchoredPosition = validationCache;
	}

	public void Show(string tooltipText, bool autoDispose)
	{
		ValidateTooltipPosition();
		gameObject.SetActive(true);

		StopAllCoroutines();
		if (autoDispose)
		{
			SetTooltipString(tooltipText);
			StartCoroutine(DisposeTooltip());
		}
		else
		{
			SetTooltipString(tooltipText);
		}
	}

	public void Hide() => gameObject.SetActive(false);

	#region Private

	private IEnumerator DisposeTooltip()
	{
		yield return new WaitForSeconds(3f);
		Hide();
	}

	private void ValidateTooltipPosition()
	{
		validationCache = Input.mousePosition /
			generalCanvas.localScale.x;

		if (validationCache.x + bgImage.rect.width > generalCanvas.rect.width)
			validationCache.x = generalCanvas.rect.width - bgImage.rect.width;

		if (validationCache.y + bgImage.rect.height > generalCanvas.rect.height)
			validationCache.y = generalCanvas.rect.height - bgImage.rect.height;

		if (validationCache.x < 0)
			validationCache.x = 0;

		if (validationCache.y < 0)
			validationCache.y = 0;
	}

	private void SetTooltipString(string tooltipText)
	{
		// Text
		textMeshPro.SetText(tooltipText);
		textMeshPro.ForceMeshUpdate();

		// Bg
		paddingCache.x = textMeshPro.GetRenderedValues(false).x +
			textMeshProTransform.localPosition.x * 2;
		paddingCache.y = textMeshPro.GetRenderedValues(false).y +
			textMeshProTransform.localPosition.y * 2;

		bgImage.sizeDelta = paddingCache;
	}

	#endregion
}
