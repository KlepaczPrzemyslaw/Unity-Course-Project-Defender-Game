using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
	[SerializeField]
	private EnemyWaveManager waveManager = null;

	[SerializeField]
	private TextMeshProUGUI waveNumberText = null;

	[SerializeField]
	private TextMeshProUGUI toDisasterText = null;

	[SerializeField]
	private TextMeshProUGUI waveMessageText = null;

	[SerializeField]
	private RectTransform nextWaveArrow = null;

	[SerializeField]
	private RectTransform closestEnemyArrow = null;

	private Camera mainCam;
	private float timerToNextEnemyCheck;
	private Enemy targetEnemy;

	// Cache
	private Vector3 arrowRotationCache = Vector3.zero;
	private Vector3 normlizedArrowCache = Vector3.zero;
	private Vector3 enemyPositionCache = Vector3.zero;
	private Collider2D[] buildingsSearchCache;
	private Enemy collisionCache;

	void Start()
	{
		mainCam = Camera.main;
		SetWaveNumberText($"Wave: {waveManager.GetWaveNumber()}");
		SetToDisasterText(EnemyWaveManager.Instance.GetWaveNumber());
		waveManager.OnWaveNumberChanged += OnWaveNumberChanged;
	}

	void OnDestroy()
	{
		waveManager.OnWaveNumberChanged -= OnWaveNumberChanged;
	}

	void Update()
	{
		// Wave will attack in -> text
		if (waveManager.GetWaveTimeToAttack() < 10f)
			SetMessageText($"Next Wave In {waveManager.GetWaveTimeToAttack().ToString("F1")}s!");
		else
			SetMessageText(string.Empty);

		// Arrow - Spawner - Rotation 
		normlizedArrowCache = (waveManager.GetNextSpawnPosition() -
			mainCam.transform.position).normalized;
		nextWaveArrow.anchoredPosition = normlizedArrowCache * 400f;
		arrowRotationCache.z = UtilitiesClass.GetAngleFromVector(
			normlizedArrowCache);
		nextWaveArrow.eulerAngles = arrowRotationCache;
		nextWaveArrow.gameObject.SetActive(
			(waveManager.GetNextSpawnPosition() - mainCam.transform.position).magnitude >
			mainCam.orthographicSize * 1.5f
		);

		// Arrow - Enemy - Rotation 
		if (Time.timeSinceLevelLoad > timerToNextEnemyCheck)
		{
			LocalizeEnemyTarget();
			timerToNextEnemyCheck = Time.timeSinceLevelLoad + 0.1f;
		}
		if (targetEnemy != null)
		{
			enemyPositionCache = targetEnemy.transform.position;
			normlizedArrowCache = (enemyPositionCache -
				mainCam.transform.position).normalized;
			closestEnemyArrow.anchoredPosition = normlizedArrowCache * 350f;
			arrowRotationCache.z = UtilitiesClass.GetAngleFromVector(
				normlizedArrowCache);
			closestEnemyArrow.eulerAngles = arrowRotationCache;
			closestEnemyArrow.gameObject.SetActive(
				(enemyPositionCache - mainCam.transform.position).magnitude >
				mainCam.orthographicSize * 1.5f
			);
		}
		else
		{
			closestEnemyArrow.gameObject.SetActive(false);
		}
	}

	private void OnWaveNumberChanged(object sender, System.EventArgs e)
	{
		SetWaveNumberText($"Wave: {waveManager.GetWaveNumber()}");
		SetToDisasterText(EnemyWaveManager.Instance.GetWaveNumber());
	}

	private void SetMessageText(string message) =>
		waveMessageText.SetText(message);

	private void SetWaveNumberText(string message) =>
		waveNumberText.SetText(message);

	private void SetToDisasterText(int currentWaveNumber) =>
		toDisasterText.SetText(currentWaveNumber > 35 ? 
			"DISASTER: NOW!" : 
			$"{36 - currentWaveNumber}: Waves To Disaster");

	private void LocalizeEnemyTarget()
	{
		buildingsSearchCache = Physics2D.OverlapCircleAll(
			transform.position, 9999f);

		targetEnemy = null;
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
					if ((mainCam.transform.position - collisionCache.transform.position).magnitude <
						(mainCam.transform.position - targetEnemy.transform.position).magnitude)
					{
						targetEnemy = collisionCache;
					}
				}
			}
		}
	}
}
