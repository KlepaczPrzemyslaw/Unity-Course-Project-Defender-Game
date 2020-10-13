using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
	[SerializeField]
	private CinemachineVirtualCamera virtualCamera = null;

	// Variables
	private Vector3 positionCache = Vector3.zero;
	private float ortographicSize;
	private float targetOrtographicSize;

	// Definitions
	private const float moveSpeed = 30f;
	private const float zoomMultiplier = 2f;
	private const float zoomSpeed = 5f;
	private const float minScrollValue = 10f;
	private const float maxScrollValue = 30f;

	// Chace
	private Vector3 clampPositionCache = Vector3.zero;

	void Start()
	{
		ortographicSize = virtualCamera.m_Lens.OrthographicSize;
		targetOrtographicSize = ortographicSize;
	}

	void Update()
	{
		// Get input
		positionCache.x = Input.GetAxisRaw("Horizontal");
		positionCache.y = Input.GetAxisRaw("Vertical");
		positionCache.z = 0f;
		positionCache = positionCache.normalized;

		// Setup new position
		transform.position += positionCache * moveSpeed * Time.deltaTime;
		// Clamping
		clampPositionCache = transform.position;
		clampPositionCache.x = Mathf.Clamp(clampPositionCache.x, -100, 100);
		clampPositionCache.y = Mathf.Clamp(clampPositionCache.y, -110, 110);
		transform.position = clampPositionCache;

		// Setup zoom in/out
		targetOrtographicSize += (-Input.mouseScrollDelta.y) * zoomMultiplier;
		targetOrtographicSize = Mathf.Clamp(targetOrtographicSize,
			minScrollValue, maxScrollValue);
		ortographicSize = Mathf.Lerp(ortographicSize,
			targetOrtographicSize, Time.deltaTime * zoomSpeed);
		virtualCamera.m_Lens.OrthographicSize = ortographicSize;
	}
}
