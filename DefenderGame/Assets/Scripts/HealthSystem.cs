using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
	public event EventHandler OnDamaged;
	public event EventHandler OnDied;

	private int maxHealthAmount = 1;
	private int healthAmount;

	void Awake() => healthAmount = maxHealthAmount;

	public void Damage(int damageAmount)
	{
		// Calc
		healthAmount -= damageAmount;
		healthAmount = Mathf.Clamp(healthAmount, 0, maxHealthAmount);

		// Events 
		OnDamaged?.Invoke(this, EventArgs.Empty);
		if (IsDead())
			OnDied?.Invoke(this, EventArgs.Empty);
	}

	public void SetMaxHealthAmount(int amount, bool updateHelthToo)
	{
		maxHealthAmount = amount;

		if (updateHelthToo)
			healthAmount = amount;
	}

	public bool IsDead() => healthAmount == 0;

	public int GetHealthAmount() => healthAmount;

	public float GetHealthAmountNormalized() => (float)healthAmount / maxHealthAmount;

	public bool IsFullHealth() => healthAmount == maxHealthAmount;
}
