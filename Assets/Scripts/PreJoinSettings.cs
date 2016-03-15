using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PreJoinSettings : NetworkBehaviour {
	public UIMover totalUI;

	public InputField nameField;
	public Slider rSlider, gSlider, bSlider;

	public Image colours;

	public GameObject myplayer;

	public AudioSource menu, inGame;
    public Vector3 rotatePoint = Vector3.zero;

	// Use this for initialization
	void Start () {
		totalUI.shown = true;
    }


    void Update() {
		colours.color = new Color (rSlider.value, gSlider.value, bSlider.value);

		if (myplayer == null) {
			totalUI.shown = true;
			Transform cam = Camera.main.transform;

			cam.RotateAround (rotatePoint, Vector3.up, 2 * Time.deltaTime);
			cam.LookAt (rotatePoint);

			menu.volume = Mathf.Lerp(menu.volume, 0.3f, 2 * Time.deltaTime);
			inGame.volume = Mathf.Lerp(inGame.volume, 0, 2 * Time.deltaTime);
		} else {
			inGame.volume = Mathf.Lerp(inGame.volume, 0.3f, 2 * Time.deltaTime);
			menu.volume = Mathf.Lerp(menu.volume, 0, 2 * Time.deltaTime);

			totalUI.shown = false;
            rotatePoint = myplayer.transform.position;
		}
	}

	public void quitGame() {
		Application.Quit ();
	}
}
