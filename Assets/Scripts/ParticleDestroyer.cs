using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {
    public float life = 5;

	// Use this for initialization
	void Start () {
        GameObject.Destroy(gameObject, life);
	}
}
