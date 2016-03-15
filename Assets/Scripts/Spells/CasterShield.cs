using UnityEngine;
using System.Collections;

public class CasterShield : SpellRoot {
	public float duration;

	public override void cast(Vector3 target) {	
		wizard.castSpell (manaCost);
		
		cooldownTimer = cooldown;
		

		wizard.Cmd_turnOnShield (duration);
	}
}