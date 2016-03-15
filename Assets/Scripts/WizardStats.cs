using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WizardStats : NetworkBehaviour {
	[SyncVar]
	public string name = "Player";
	[SyncVar]
	public float hp = 100;
	[SyncVar]
	public Color colour = Color.black;
	public SkinnedMeshRenderer colorMat;

	public float mana = 100;
	public float MPs = 3;

	public ParticleSystem fire, shields;
	public GameObject shieldImpact, spawnEffect;

	public float maxHP, maxMana;
	private Rigidbody rigidbody;
	private NavMeshAgent agent;
	private bool onFire = false;

	public TextMesh text;

	[SyncVar]
	public bool shielded = false;

	public GameObject ragdoll;

	// Use this for initialization
	void Start () {
//		chat = GameObject.FindGameObjectWithTag ("Chat").GetComponent<ChatSystem> ();
		rigidbody = GetComponent<Rigidbody> (); 
		maxHP = hp;
		maxMana = mana;
		agent = GetComponent<NavMeshAgent> ();
		Cmd_turnOnShield (2);
		Instantiate(spawnEffect, transform.position + Vector3.up, Random.rotation);
		if (isLocalPlayer) {
			PreJoinSettings set = GameObject.Find ("StartSettings").GetComponent<PreJoinSettings>();
			string text = set.nameField.text;
			
			set.myplayer = gameObject;
			Cmd_uploadSettings(set.nameField.text, new Color(set.rSlider.value, set.gSlider.value, set.bSlider.value));
		}
	}

	[Command]
	public void Cmd_uploadSettings(string newName, Color newColor) {
		name = newName;
		colour = newColor;
	}

	public void castSpell(int manaCost) {
		mana -= manaCost;
	}

	[Command]
	public void Cmd_turnOnShield(float duration) {
		shielded = true;
		Invoke ("unShield", duration);
	}

	void unShield() {
		shielded = false;
	}

	[ClientRpc]
	public void Rpc_takeDamage(float damage, Vector3 knockback) {
		if (shielded) {
			Instantiate(shieldImpact, transform.position + Vector3.up, Random.rotation);
			return;
		}

		hp -= damage;
		hp = Mathf.Clamp(hp, 0, maxHP);

		if (hp <= 0) {
			die(knockback);
		}

		agent.enabled = false;
		rigidbody.AddForce (knockback, ForceMode.Impulse);
	}

	void die(Vector3 knockback) {
		if(isLocalPlayer)
			GameObject.FindGameObjectWithTag ("Respawn").GetComponent<PlayerRespawn>().dead = true;

		Cmd_createRagdoll(knockback);
		
		GameObject.Destroy(gameObject);
	}

	[Command]
	void Cmd_createRagdoll(Vector3 knockback) {
		GameObject rag = Instantiate(ragdoll, transform.position, transform.rotation) as GameObject;

		rag.transform.GetComponent<Rigidbody> ().AddForce (knockback * 15, ForceMode.Impulse);
		GameObject.Destroy (rag, 60);

		NetworkServer.Spawn (rag);
	}

	public void teleport(Vector3 location) {
		transform.position = location;
		agent.destination = location;
	}

	public void lavaDamage(float damage) {
		if (shielded) {
			return;
		}
		onFire = true;
		hp -= damage;
		hp = Mathf.Clamp(hp, 0, maxHP);

		if (hp <= 0) {
			die(transform.forward * -5);
		}
	}
	
	// Update is called once per frame
	void Update () {
		text.text = name;
		text.transform.LookAt (2 * transform.position - Camera.main.transform.position);
		text.color = colour;
		colorMat.material.color = colour;
		colorMat.material.SetColor("_SpecColor", colour);

		fire.enableEmission = onFire;
		shields.gameObject.SetActive (shielded);
		onFire = false;

		mana += Time.deltaTime * MPs;
		mana = Mathf.Clamp (mana, 0, maxMana);
	}
}
