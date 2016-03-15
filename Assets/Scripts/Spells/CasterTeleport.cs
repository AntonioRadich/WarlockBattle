using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CasterTeleport : SpellRoot {
	public GameObject teleportEffect;

	public override void cast(Vector3 target) {	
		Cmd_effect (target);
		Instantiate (teleportEffect, transform.position, Quaternion.identity);
		Instantiate (teleportEffect, target, Quaternion.identity);

		wizard.castSpell (manaCost);
		
		cooldownTimer = cooldown;

		wizard.teleport (target);
	}
	
	[Command]
	void Cmd_effect(Vector3 target) {
		Instantiate (teleportEffect, transform.position, Quaternion.identity);
		Instantiate (teleportEffect, target, Quaternion.identity);
		Rpc_effect (target);
	}
	
	[RPC]
	void Rpc_effect(Vector3 target) {
		Instantiate (teleportEffect, transform.position, Quaternion.identity);
		Instantiate (teleportEffect, target, Quaternion.identity);
	}
}