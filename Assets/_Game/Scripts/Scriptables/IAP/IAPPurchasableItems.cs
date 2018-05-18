using UnityEngine;

[CreateAssetMenu (menuName = "AIP/ Coins")]
public class IAPPurchasableItems : ScriptableObject {
	[SerializeField] private Coins[] IAPCoins;
}

[System.Serializable]
public class Coins {
	public string id;
	public int quantity = 100;
	public string price = "$ 0.99";
}