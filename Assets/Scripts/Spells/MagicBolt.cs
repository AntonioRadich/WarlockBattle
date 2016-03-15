using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MagicBolt : NetworkBehaviour {
	public float speed = 50, life = 10, knockback = 10;
	public int damage = 10;
	public GameObject impactEffect;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;

		Invoke ("die", life);
	}

	void Update() {
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}

	[ClientRpc]
	void Rpc_impactor() {
		Instantiate (impactEffect, transform.position, Random.rotation);
	}

	[Server]
	void die() {
		Rpc_impactor ();
		Instantiate (impactEffect, transform.position, Random.rotation);
		GameObject.Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (isServer) {
			if (other.transform.gameObject.tag == "Player") {
				other.transform.GetComponent<WizardStats> ().Rpc_takeDamage (damage, transform.forward * knockback);
				die ();
			} else if(other.transform.root.name != "Lava" && other.transform.root.name != "ManaZone") {
				die ();
			}
		}
	}
}
