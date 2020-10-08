using UnityEngine;

public class Building : MonoBehaviour
{
	private HealthSystem healthSystem;

	void Start()
	{ 
		healthSystem = GetComponent<HealthSystem>();

		healthSystem.SetMaxHealthAmount(
			GetComponent<BuildingTypeHolder>().buildingType.maxHealthAmount,
			true);
		healthSystem.OnDied += OnDied;
	}

	private void OnDied(object sender, System.EventArgs e) =>
		Destroy(gameObject);
}
