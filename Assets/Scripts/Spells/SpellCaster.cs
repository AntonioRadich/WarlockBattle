using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpellCaster : NetworkBehaviour {
	public Transform point;
	public SpellRoot[] spells;

	private NavMeshAgent agent;
	private WizardAnimator animator;
	private WizardStats wizard;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<WizardAnimator> ();
		wizard = GetComponent<WizardStats> ();
	}
	
	// Update is called once per frame
	void Update () {
	//	if (!isLocalPlayer || wizard.chat.input.isFocused)
		if (!isLocalPlayer)
			return;

		foreach(SpellRoot spell in spells) {
			if(Input.GetKey(spell.key)) {
				if(spell.cooldownTimer <= 0 && wizard.mana > spell.manaCost) {		
					Vector3 target = getTarget();
					Vector3 dir = target - transform.position;
					dir.Normalize ();

					agent.SetDestination (transform.position + dir);
					animator.cast();

					spell.cast(target);
				}
			}
		}
	}

	Vector3 getTarget() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)) {
			Vector3 target = hit.point;
			target.y = point.position.y;
			
			return target;
		}
		return point.position;
	}
}
