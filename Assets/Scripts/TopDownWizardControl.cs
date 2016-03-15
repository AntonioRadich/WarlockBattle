using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TopDownWizardControl : NetworkBehaviour {
	public Vector3 camOffset;
	public float camSpeed = 1;
	public GameObject selectEffect;

	private WizardStats wizard;
	private Camera cam;
	private NavMeshAgent agent;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		agent = GetComponent<NavMeshAgent> ();
		rigidbody = GetComponent<Rigidbody> ();
		wizard = GetComponent<WizardStats> ();

		agent.enabled = isLocalPlayer;
		if (isLocalPlayer) {
			cam.transform.position = transform.position + camOffset;
			Vector3 rot = new Vector3 (50, 0, 0);
			cam.transform.localEulerAngles = rot;
			CameraFade.StartAlphaFade (Color.white, true, 3);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;

		cam.transform.position = Vector3.Lerp (cam.transform.position, transform.position + camOffset, camSpeed * Time.deltaTime);

		if (Input.GetMouseButtonDown (1) && agent.enabled) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				Instantiate(selectEffect, hit.point, Quaternion.identity);
				agent.SetDestination(hit.point);
			}
		}
	}

	void FixedUpdate() {
		if (isLocalPlayer && rigidbody.velocity.magnitude < 5)
			agent.enabled = true;
	}
}
