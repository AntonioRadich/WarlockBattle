using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking;

public class CasterGrenade : SpellRoot {
	public GameObject projectile;
	
	public override void cast(Vector3 target) {	
		wizard.castSpell (manaCost);
		
		cooldownTimer = cooldown;
		
		Cmd_cast (target);
	}
	
	[Command]
	public void Cmd_cast(Vector3 target) {
		GameObject spell = Instantiate(projectile, caster.point.position, caster.point.rotation) as GameObject;
		Physics.IgnoreCollision (wizard.GetComponent<CapsuleCollider> (), spell.GetComponent<SphereCollider> ());
		
		spell.transform.LookAt (target);

		Vector3 dir = target - transform.position; // get target direction
		float h = dir.y;  // get height difference
		dir.y = 0;  // retain only the horizontal direction
		float dist = dir.magnitude ;  // get horizontal distance
		dir.y = dist;  // set elevation to 45 degrees
		dist += h - 1;  // correct for different heights
		float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
		Vector3 force = vel * dir.normalized;  // returns Vector3 velocity

		if (!float.IsNaN (force.x) && !float.IsNaN (force.y) && !float.IsNaN (force.z)) {
			spell.GetComponent<Rigidbody> ().velocity = force;
		} else {
			spell.GetComponent<Rigidbody> ().velocity = Vector3.up * 5;
		}


		NetworkServer.Spawn (spell);
		
	}
}