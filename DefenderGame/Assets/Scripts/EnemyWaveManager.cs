using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
	public static EnemyWaveManager Instance { get; protected set; }

	public event System.EventHandler OnWaveNumberChanged;

	[SerializeField]
	private Transform spawnerParent = null;

	[SerializeField]
	private Transform waveCircleTrans = null;

	private List<Vector3> spawnPositions;
	private Vector3 nextAttackFrom;
	private float nextWaveSpawnTimer;
	private int waveNumber;

	// Const
	private const int baseWaveEnemies = 200;
	private const int waveIncreaser = 3;

	// Cahce
	private Vector3 randomPositionMultiplier = Vector3.zero;

	void Awake() => Instance = this;

	void Start()
	{
		waveNumber = 1;

		// First wave is delayed
		nextWaveSpawnTimer = Time.timeSinceLevelLoad + 15f;

		// Spawners
		spawnPositions = new List<Vector3>();
		foreach (Transform child in spawnerParent)
			spawnPositions.Add(child.position);
		nextAttackFrom = spawnPositions[UtilitiesClass
			.GetRandomArrayIndex(spawnPositions.Count)];
		waveCircleTrans.position = nextAttackFrom;
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad > nextWaveSpawnTimer &&
			BuildingManager.Instance.HqExist())
			StartCoroutine(SpawnWave());
	}

	public int GetWaveNumber() => waveNumber;

	public float GetWaveTimeToAttack() => nextWaveSpawnTimer - Time.timeSinceLevelLoad;

	public Vector3 GetNextSpawnPosition() => nextAttackFrom;

	private IEnumerator SpawnWave()
	{
		// New timer
		nextWaveSpawnTimer = Time.timeSinceLevelLoad + 20f;

		// Spawer
		for (int i = 0; i < (baseWaveEnemies + waveIncreaser * waveNumber); i++)
		{
			randomPositionMultiplier.x = Random.Range(-3f, 3f);
			randomPositionMultiplier.y = Random.Range(-3f, 3f);
			Enemy.Create(nextAttackFrom + randomPositionMultiplier);

			yield return new WaitForSeconds(.5f);
		}

		// Cleanup
		waveNumber++;
		OnWaveNumberChanged?.Invoke(this, System.EventArgs.Empty);
		nextAttackFrom = spawnPositions[UtilitiesClass
			.GetRandomArrayIndex(spawnPositions.Count)];
		waveCircleTrans.position = nextAttackFrom;
	}
}
