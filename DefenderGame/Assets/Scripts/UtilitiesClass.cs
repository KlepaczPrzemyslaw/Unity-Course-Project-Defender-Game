using UnityEngine;

public static class UtilitiesClass
{
	private static Vector3 tempPosition;
	private static Camera mainCamera;

	public static Vector3 GetMouseWorldPosition()
	{
		if (mainCamera == null)
			mainCamera = Camera.main;

		tempPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		tempPosition.z = 0f;
		return tempPosition;
	}

	public static float GetAngleFromVector(Vector3 vector) =>
		Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
}
