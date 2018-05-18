using UnityEngine;

[CreateAssetMenu (menuName = "Cloud Variables/Player Profile ")]
public class PlayerProfile : ScriptableObject {

	public string playerName;
	public int highScore;
	public int coins;

}