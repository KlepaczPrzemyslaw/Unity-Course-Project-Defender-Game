﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
	public static Enemy Create(Vector3 position) =>
		Instantiate(Resources.Load<Transform>(typeof(Enemy).Name),
			position, Quaternion.identity).GetComponent<Enemy>();

	[SerializeField]
	private float speed = 6f;

	[SerializeField]
	private float targetMaxRadius = 10f;

	[SerializeField]
	private int baseHealth = 3;

	private Rigidbody2D rb;
	private Transform targetTransform;
	private float lookForTargetTimer;
	private HealthSystem healthSystem;
	private Transform particles;

	// Consts
	private const float lookForTargetOffset = 0.2f;

	// Cache
	private Building collisionCache;
	private HealthSystem healthCache;
	private Collider2D[] buildingsSearchCache;
	private int calcHealthCache;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		healthSystem = GetComponent<HealthSystem>();

		// Base
		calcHealthCache = baseHealth +
			Mathf.FloorToInt(EnemyWaveManager.Instance.GetWaveNumber() / 4);
		// Extra
		if (EnemyWaveManager.Instance.GetWaveNumber() > 35)
			calcHealthCache += EnemyWaveManager.Instance.GetWaveNumber() - 35;
		// Set
		healthSystem.SetMaxHealthAmount(calcHealthCache, true);

		healthSystem.OnDied += OnDied;
		healthSystem.OnDamaged += OnDamaged;
		lookForTargetTimer = Random.Range(0f,
			lookForTargetOffset);

		particles = Resources.Load<Transform>("pfEnemyDieParticles");

		if (BuildingManager.Instance.HqExist())
			targetTransform = BuildingManager.Instance
				.GetHqBuilding().transform;
	}

	void OnDestroy()
	{
		healthSystem.OnDied -= OnDied;
		healthSystem.OnDamaged -= OnDamaged;
	}

	void Update()
	{
		if (targetTransform != null)
		{
			rb.velocity = (targetTransform.position -
				transform.position).normalized * speed;
		}
		else
		{
			LookForTargets();
			lookForTargetTimer = Time.timeSinceLevelLoad +
				lookForTargetOffset;
		}

		if (Time.timeSinceLevelLoad > lookForTargetTimer)
		{
			LookForTargets();
			lookForTargetTimer = Time.timeSinceLevelLoad +
				lookForTargetOffset;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out collisionCache))
		{
			if (collisionCache.TryGetComponent(out healthCache))
			{
				healthCache.Damage(10);
				healthSystem.Damage(int.MaxValue);
			}
		}
	}

	private void OnDamaged(object sender, System.EventArgs e) =>
		SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyHit);

	private void OnDied(object sender, System.EventArgs e)
	{
		SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyDie);
		Instantiate(particles, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void LookForTargets()
	{
		buildingsSearchCache = Physics2D.OverlapCircleAll(
			transform.position, targetMaxRadius);

		foreach (var collider in buildingsSearchCache)
		{
			if (collider.TryGetComponent(out collisionCache))
			{
				if (targetTransform == null)
				{
					targetTransform = collisionCache.transform;
				}
				else
				{
					if ((transform.position - collisionCache.transform.position).magnitude <
						(transform.position - targetTransform.position).magnitude)
					{
						targetTransform = collisionCache.transform;
					}
				}
			}
		}

		// If still null
		if (targetTransform == null && BuildingManager.Instance.HqExist())
			targetTransform = BuildingManager.Instance
				.GetHqBuilding().transform;
	}
}
