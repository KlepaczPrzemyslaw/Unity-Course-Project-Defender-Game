using UnityEngine;

public class HealthBar : MonoBehaviour
{
	[Header("External")]
	[SerializeField]
	private HealthSystem healthSystem = null;

	[Header("Internal")]
	[SerializeField]
	private Transform barTransform = null;

	private Vector3 barScaleCache = Vector3.zero;

	void Start()
	{
		barScaleCache.y = 1;
		barScaleCache.z = 1;
		UpdateBar();
		UpdateHealthBarVisible();
		healthSystem.OnDamaged += OnDamaged;
		healthSystem.OnHealed += OnHealed;
	}

	void OnDestroy()
	{
		healthSystem.OnDamaged -= OnDamaged;
		healthSystem.OnHealed -= OnHealed;
	}

	private void UpdateBar()
	{
		barScaleCache.x = healthSystem.GetHealthAmountNormalized();
		barTransform.localScale = barScaleCache;
	}

	private void OnDamaged(object sender, System.EventArgs e)
	{
		UpdateBar();
		UpdateHealthBarVisible();
	}

	private void OnHealed(object sender, System.EventArgs e)
	{
		UpdateBar();
		UpdateHealthBarVisible();
	}

	private void UpdateHealthBarVisible()
	{
		if (healthSystem.IsFullHealth())
			gameObject.SetActive(false);
		else
			gameObject.SetActive(true);
	}
}
