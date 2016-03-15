using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpellRoot : NetworkBehaviour {
	public string spellName, desc;
	public int manaCost;
	public KeyCode key;
	public float cooldown, cooldownTimer;

	public WizardStats wizard;
	public SpellCaster caster;

	public Texture icon;
	
	public virtual void cast(Vector3 target) {
		
	}
	
	void Start() {
		//	cooldownTimer = cooldown;
	}
	
	public virtual void Update() {
		if(cooldownTimer > 0) {
			cooldownTimer -= Time.deltaTime;
		}
		if(cooldown <= 0.01f) {
			cooldownTimer = 0;
		}
	}
}
