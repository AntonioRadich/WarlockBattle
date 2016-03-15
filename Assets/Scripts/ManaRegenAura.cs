using UnityEngine;
using System.Collections;

public class ManaRegenAura : MonoBehaviour {
	public float radius = 10, manaPS = 10;
	public Color gizmosColor;

	void OnTriggerStay(Collider other) {
		if (other.transform.gameObject.tag == "Player") {
			other.transform.GetComponent<WizardStats> ().mana += manaPS * Time.deltaTime;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = gizmosColor;
		Gizmos.DrawSphere(transform.position, radius);
	}
}
