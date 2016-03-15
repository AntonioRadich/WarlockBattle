using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SimpleSpell : NetworkBehaviour {
	public float cooldown = 1;
	public GameObject spellPrefab;
	public Transform point;

	private bool canFire = false;
	private NavMeshAgent agent;
	private WizardAnimator animator;

	// Use this for initialization
	void Start () {
		canFire = true;
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<WizardAnimator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer && canFire && Input.GetKey(KeyCode.Q)) {
			canFire = false;
			Invoke("resetFire", cooldown);

			Vector3 target = getTarget();
			Vector3 dir = target - transform.position;
			dir.Normalize ();
			agent.SetDestination (transform.position + dir);
			animator.cast();

			Cmd_fire(target);
		}
	}

	[Command]
	public void Cmd_fire(Vector3 target) {
		GameObject spell = Instantiate(spellPrefab, point.position, point.rotation) as GameObject;
		Physics.IgnoreCollision (GetComponent<CapsuleCollider> (), spell.GetComponent<SphereCollider> ());

		spell.transform.LookAt (target);
		NetworkServer.Spawn (spell);

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

	void resetFire() {
		canFire = true;
	}
}
