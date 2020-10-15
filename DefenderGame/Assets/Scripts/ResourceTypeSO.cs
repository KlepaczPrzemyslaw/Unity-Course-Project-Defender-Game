using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
	public string resourceName;
	public Sprite sprite;
	public string colorInHex;
	public string nameShort;
}
