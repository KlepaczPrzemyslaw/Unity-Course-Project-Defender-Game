using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
	[SerializeField]
	private Button buildingRepairButton = null;

	[SerializeField]
	private Button buildingDestroyButton = null;

	private HealthSystem healthSystem;

	void Start()
	{
		buildingDestroyButton?.gameObject?.SetActive(false);
		buildingRepairButton?.gameObject?.SetActive(false);
		healthSystem = GetComponent<HealthSystem>();

		healthSystem.SetMaxHealthAmount(
			GetComponent<BuildingTypeHolder>().buildingType.maxHealthAmount,
			true);
		healthSystem.OnDied += OnDied;
		healthSystem.OnDamaged += OnDamaged;
		healthSystem.OnHealed += OnHealed;
	}

	void OnDestroy()
	{
		healthSystem.OnDied -= OnDied;
		healthSystem.OnDamaged -= OnDamaged;
		healthSystem.OnHealed -= OnHealed;
	}

	void OnMouseEnter() =>
		buildingDestroyButton?.gameObject?.SetActive(true);

	void OnMouseExit() =>
		buildingDestroyButton?.gameObject?.SetActive(false);

	private void OnDied(object sender, System.EventArgs e)
	{
		SoundManager.Instance.PlaySound(SoundManager.Sounds.BuildingDestroyed);
		Destroy(gameObject);
	}

	private void OnDamaged(object sender, System.EventArgs e)
	{
		SoundManager.Instance.PlaySound(SoundManager.Sounds.BuildingDamaged);
		buildingRepairButton?.gameObject?.SetActive(true);
	}

	private void OnHealed(object sender, System.EventArgs e)
	{
		if (healthSystem.IsFullHealth())
			buildingRepairButton?.gameObject?.SetActive(false);
	}
}
