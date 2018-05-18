using UnityEngine;

[CreateAssetMenu (menuName = "Shop/Upgradable Item")]
public class UpgradableItems : ScriptableObject {

	public string itemName = "Magnent";
	public string itemDescription;
	public int level = 0;
	public int[] price;

}