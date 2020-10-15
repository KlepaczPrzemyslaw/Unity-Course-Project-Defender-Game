using UnityEngine;
using UnityEngine.UI;

public class ConstrucionTimerUI : MonoBehaviour
{
	[SerializeField]
	private Image enternalImage = null;

	[SerializeField]
	private Image internalImage = null;

	[SerializeField]
	private BuildingConstruction buildingConstruction = null;

	void Update()
	{
		enternalImage.fillAmount = buildingConstruction
			.GetTimerNormalized();
		internalImage.fillAmount = buildingConstruction
			.GetTimerNormalized();
	}
}
