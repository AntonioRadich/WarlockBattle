using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
	public float dps = 10;
	public Color gizmosColor;

	void OnTriggerStay(Collider other) {
		if (other.transform.gameObject.tag == "Player") {
			other.transform.GetComponent<WizardStats> ().lavaDamage (dps * Time.deltaTime);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = gizmosColor;
		foreach (Transform child in transform.GetComponentsInChildren<Transform>()) {
			Gizmos.DrawCube(child.transform.position, child.localScale);
		}
	}
}
