using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour {
	public float countdown = 15;
	public Text text;
	public UIMover ui;
	public bool dead = false;

	private bool canSpawn = false;
	
	public void Start() {

	}
	
	public void Update() {
		if (dead) {
			ui.shown = true;
			countdown -= Time.deltaTime;

			countdown = Mathf.Clamp (countdown, 0, 1000);

			if (countdown == 0) {
				canSpawn = true;
				text.text = "Click here to respawn!";
			} else {
				text.text = "Respawn in : " + Mathf.RoundToInt (countdown);
			}
		}
	}


	public void trySpawn() {
		if (canSpawn) {
			dead = false;
			canSpawn = false;
			ui.shown = false;
			countdown = 15;

			ClientScene.AddPlayer(0);
		}
	}
}
