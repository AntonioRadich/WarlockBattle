using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Grenade : NetworkBehaviour {
	public float knockback = 20, radius = 5;
	public int damage = 20;
	public GameObject impactEffect;

	void Start() {
		Invoke ("die", 10);
	}

	[ClientRpc]
	void Rpc_impactor() {
		Instantiate (impactEffect, transform.position, Random.rotation);
	}
	
	void die() {
		Rpc_impactor ();
		Instantiate (impactEffect, transform.position, Random.rotation);
		GameObject.Destroy (gameObject);
	}
	
	void OnTriggerEnter(Collider other) {
		if (isServer) {
			if(other.transform.root.name != "Lava" && other.transform.root.name != "ManaZone") {
				RaycastHit[] hits;

				hits = Physics.SphereCastAll(transform.position, radius, Vector3.up);
				foreach(RaycastHit hit in hits) {
					if(hit.transform.gameObject.tag == "Player") {
						Vector3 blastDir = hit.transform.position - transform.position;
						blastDir.y = 0;

						hit.transform.GetComponent<WizardStats> ().Rpc_takeDamage (damage, blastDir.normalized * knockback);
					} else if(hit.transform.GetComponent<Rigidbody>()) {
						Vector3 blastDir = hit.transform.position - transform.position;

						hit.transform.GetComponent<Rigidbody> ().AddForce (blastDir.normalized * knockback + Vector3.up * 5, ForceMode.Impulse);
					}
				}
				die ();
			}
		}
	}
}
