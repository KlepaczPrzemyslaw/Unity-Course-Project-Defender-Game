using UnityEngine;

public class ResourceNode : MonoBehaviour
{
	[SerializeField]
	private ResourceTypeSO resourceType = null;

	public ResourceTypeSO ResourceType => resourceType;
}
