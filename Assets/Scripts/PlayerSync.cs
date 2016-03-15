using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour {
	[SyncVar]
	private Quaternion real_playerRot;
	[SyncVar]
	private Vector3 real_playerPos;

	private float lerpRate = 15;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transmitValues ();
		lerpEverything ();
	}
	
	[Command]
	void Cmd_giveValuesToServer(Vector3 pos, Quaternion playerRot) {
		real_playerRot = playerRot;
		real_playerPos = pos;
	}

	void lerpEverything() {
		if (!isLocalPlayer) {
			transform.position = Vector3.Lerp (transform.position, real_playerPos, lerpRate * Time.deltaTime);
			transform.rotation = Quaternion.Lerp (transform.rotation, real_playerRot, lerpRate * Time.deltaTime);
		}
	}

	[ClientCallback]
	void transmitValues() {
		if (isLocalPlayer) {
			Cmd_giveValuesToServer(transform.position, transform.rotation);
		}
	}
}
