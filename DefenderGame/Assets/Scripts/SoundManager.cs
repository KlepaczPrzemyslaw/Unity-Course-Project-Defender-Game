using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	public static int Volume { get; protected set; } = 5;

	public enum Sounds
	{
		BuildingDamaged,
		BuildingDestroyed,
		BuildingPlaced,
		EnemyDie,
		EnemyHit,
		EnemyWaveStarting,
		GameOver,
		Music
	}

	private AudioSource audioSource;
	private Dictionary<Sounds, AudioClip> listOfAudio;

	private void Awake()
	{
		Instance = this;

		audioSource = GetComponent<AudioSource>();
		listOfAudio = new Dictionary<Sounds, AudioClip>();
		foreach (Sounds snd in Enum.GetValues(typeof(Sounds)))
		{
			listOfAudio.Add(snd,
				Resources.Load<AudioClip>(snd.ToString()));
		}
	}

	public void PlaySound(Sounds sound)
	{
		audioSource.PlayOneShot(listOfAudio[sound], Volume / 10f);
	}

	public void ChangeVolumeBy(int by)
	{
		Volume += by;
		Volume = Mathf.Clamp(Volume, 0, 10);
	}
}
