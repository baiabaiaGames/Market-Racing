using UnityEngine;

[CreateAssetMenu (menuName = "Shop/Single Use")]
public class SingleUse : ScriptableObject {

    public string itemName;
    public string itemDescription;
    public int price = 300;

}