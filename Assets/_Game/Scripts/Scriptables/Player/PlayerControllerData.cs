using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Player/Player Controller Data")]
public class PlayerControllerData : ScriptableObject {

	public float minSpeed = 7.00f;
	public float maxSpeed = 9.00f;
	public float speedMultiplier = 1.01f;
	public float minMultiplier = 1.00f;
	public float maxMultiplier = 30.00f;
	public float increaseMultiplier = 0.05f;
	public float jumpForce = 4.00f;
	public float gravity = 12.00f;

}