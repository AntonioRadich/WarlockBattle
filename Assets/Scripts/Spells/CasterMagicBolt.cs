using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CasterMagicBolt : SpellRoot {
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
		NetworkServer.Spawn (spell);
		
	}
}