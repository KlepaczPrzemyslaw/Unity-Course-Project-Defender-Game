using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
	private Button button;
	private HealthSystem healthSystem;

	void Start()
	{
		button = GetComponentInChildren<Button>();
		button?.gameObject?.SetActive(false);
		healthSystem = GetComponent<HealthSystem>();

		healthSystem.SetMaxHealthAmount(
			GetComponent<BuildingTypeHolder>().buildingType.maxHealthAmount,
			true);
		healthSystem.OnDied += OnDied;
	}

	void OnMouseEnter() =>
		button?.gameObject?.SetActive(true);

	void OnMouseExit() =>
		button?.gameObject?.SetActive(false);

	private void OnDied(object sender, System.EventArgs e) =>
		Destroy(gameObject);
}
