using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
	private static ArrowProjectile selfCache;
	public static ArrowProjectile Create(Vector3 position, Enemy target)
	{
		selfCache = Instantiate(Resources.Load<Transform>(typeof(ArrowProjectile).Name),
			position, Quaternion.identity).GetComponent<ArrowProjectile>();
		selfCache.SetTarget(target);
		return selfCache;
	}

	[SerializeField]
	private float speed = 20f;

	private Enemy targetEnemy;
	private Vector3 lastMoveDir = Vector3.zero;
	private float timeToDestroy = 2f;

	// Cache
	private Enemy collisionCache;
	private Vector3 moveDirCache = Vector3.zero;
	private Vector3 rotationCache = Vector3.zero;

	void Start()
	{
		timeToDestroy = 2f;
	}

	void Update()
	{
		// Calc
		if (targetEnemy != null)
		{
			moveDirCache = (targetEnemy.transform.position -
				transform.position).normalized;
			lastMoveDir = moveDirCache;
		}
		else
		{
			moveDirCache = lastMoveDir;
		}

		// Update position and rotation
		rotationCache.z = UtilitiesClass.GetAngleFromVector(moveDirCache);
		transform.eulerAngles = rotationCache;
		transform.position += moveDirCache * Time.deltaTime * speed;

		// Check
		timeToDestroy -= Time.deltaTime;
		if (timeToDestroy < 0f)
			Destroy(gameObject);
	}

	private void SetTarget(Enemy enemy)
	{
		targetEnemy = enemy;

		// Set base information
		moveDirCache = (targetEnemy.transform.position -
				transform.position).normalized;
		lastMoveDir = moveDirCache;
		rotationCache.z = UtilitiesClass.GetAngleFromVector(moveDirCache);
		transform.eulerAngles = rotationCache;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent(out collisionCache))
		{
			collisionCache.GetComponent<HealthSystem>().Damage(1);
			Destroy(gameObject);
		}
	}
}
