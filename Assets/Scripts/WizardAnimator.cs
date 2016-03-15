using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WizardAnimator : NetworkBehaviour {
	[SyncVar]
	public AnimState state = AnimState.Idle;

	public Transform body;
	
	private NavMeshAgent agent;
	private Rigidbody rigidbody;
	private bool casting = false;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		rigidbody = GetComponent<Rigidbody> ();
	}

	public void cast() {
		casting = true;
		Invoke ("finishCast", 0.5f);
	}

	void finishCast() {
		casting = false;
	}

	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {
			if(agent) {
				if(agent.velocity.magnitude > 0.1f) {
					Cmd_setState(AnimState.Moving);
				} else {
					Cmd_setState(AnimState.Idle);
				}
			} else if(rigidbody.velocity.magnitude > 5) {
				Cmd_setState(AnimState.Flying);
			}
			if(casting) {
				Cmd_setState(AnimState.Casting);
			}
		}
		setAnim ();
	}

	[Command]
	void Cmd_setState(AnimState anim) {
		state = anim;
	}

	void setAnim() {
		if (state == AnimState.Idle)
			body.GetComponent<Animation> ().CrossFade ("idle");
		if (state == AnimState.Moving)
			body.GetComponent<Animation> ().CrossFade ("run");
		if (state == AnimState.Flying)
			body.GetComponent<Animation> ().CrossFade ("flip");
		if (state == AnimState.Casting)
			body.GetComponent<Animation> ().Play ("punch", PlayMode.StopSameLayer);
	}

	public enum AnimState {
		Idle,
		Moving,
		Flying,
		Casting
	}
}
