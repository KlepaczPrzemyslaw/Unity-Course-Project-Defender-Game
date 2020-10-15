using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayAndNightCicle : MonoBehaviour
{
	[SerializeField]
	private Gradient gradient = null;

	[SerializeField]
	[Range(10, 180)]
	private float secondsPerDay = 20f;

	private Light2D globalLight;
	private float dayTime;
	private float dayTimeSpeed;

	void Awake()
	{
		globalLight = GetComponent<Light2D>();
		dayTimeSpeed = 1 / secondsPerDay;
	}

	void Update()
	{
		dayTime += Time.deltaTime * dayTimeSpeed;
		globalLight.color = gradient.Evaluate(dayTime % 1f);
	}
}
