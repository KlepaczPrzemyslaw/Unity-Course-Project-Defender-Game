using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField]
	private float targetMaxRadius = 20f;

	[SerializeField]
	private Transform projectileSpawnPoint = null;

	private Enemy targetEnemy;
	private float lookForTargetTimer;
	private float shootTimer;

	// Consts
	private const float lookForTargetOffset = 0.2f;
	private const float shootTimerOffset = 0.25f;

	// Cache
	private Collider2D[] buildingsSearchCache;
	private Enemy collisionCache;

	void Start()
	{
		lookForTargetTimer = Random.Range(0f,
			lookForTargetOffset);
		shootTimer = 0f;
	}

	void Update()
	{
		// Targeting
		if (Time.timeSinceLevelLoad > lookForTargetTimer)
		{
			LookForTargets();
			lookForTargetTimer = Time.timeSinceLevelLoad +
				lookForTargetOffset;
		}

		// Shooting
		Shoot();
	}

	private void LookForTargets()
	{
		buildingsSearchCache = Physics2D.OverlapCircleAll(
			transform.position, targetMaxRadius);

		foreach (var collider in buildingsSearchCache)
		{
			if (collider.TryGetComponent(out collisionCache))
			{
				if (targetEnemy == null)
				{
					targetEnemy = collisionCache;
				}
				else
				{
					if ((transform.position - collisionCache.transform.position).magnitude <
						(transform.position - targetEnemy.transform.position).magnitude)
					{
						targetEnemy = collisionCache;
					}
				}
			}
		}
	}

	private void Shoot()
	{
		if (targetEnemy != null)
		{
			if (Time.timeSinceLevelLoad > shootTimer)
			{
				ArrowProjectile.Create(projectileSpawnPoint
					.transform.position, targetEnemy);
				shootTimer = Time.timeSinceLevelLoad +
					shootTimerOffset;
			}
		}
	}
}
