using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Single Instances/Game Settings")]
public class GameSettings : ScriptableObject {

	public float version = 0;
	public PlayerProfile playerProfile;

}