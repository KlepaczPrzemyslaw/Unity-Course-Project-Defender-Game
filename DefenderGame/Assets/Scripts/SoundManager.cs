using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	public static int Volume { get; protected set; } = 5;

	[SerializeField]
	private OptionsUI optionsUI = null;

	private const string VolumeKey = "SoundVolume";

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

	void Awake()
	{
		Instance = this;

		if (PlayerPrefs.HasKey(VolumeKey))
		{
			Volume = PlayerPrefs.GetInt(VolumeKey);
		}
		else
		{
			PlayerPrefs.SetInt(VolumeKey, Volume);
			PlayerPrefs.Save();
		}

		audioSource = GetComponent<AudioSource>();
		listOfAudio = new Dictionary<Sounds, AudioClip>();
		foreach (Sounds snd in Enum.GetValues(typeof(Sounds)))
		{
			listOfAudio.Add(snd,
				Resources.Load<AudioClip>(snd.ToString()));
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			optionsUI.ToggleOptions();
	}

	public void PlaySound(Sounds sound)
	{
		audioSource.PlayOneShot(listOfAudio[sound], Volume / 10f);
	}

	public void ChangeVolumeBy(int by)
	{
		Volume += by;
		Volume = Mathf.Clamp(Volume, 0, 10);
		PlayerPrefs.SetInt(VolumeKey, Volume);
		PlayerPrefs.Save();
	}
}
