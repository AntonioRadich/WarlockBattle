using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class MainMenu : NetworkManager {
	public MenuState menuState = MenuState.Main;

	public UIMover mainUI, localUI, onlineUI;
	public RectTransform scrollViewList;
	public GameObject gameListItem;
	public Scrollbar scrollBar;

	public int testNum = 11;

	public void Start() {
		createGameList ();
	}

	public void clearGameList() {
		RectTransform[] currentList = scrollViewList.GetComponentsInChildren<RectTransform> ();
		foreach (RectTransform trans in currentList) {
			if(trans != scrollViewList)
				GameObject.Destroy(trans.gameObject);
		}
		scrollViewList.sizeDelta = new Vector2 (928, 770);
		scrollBar.value = 1;
	}
	
	public void createGameList() {
		clearGameList ();
		scrollViewList.sizeDelta = new Vector2 (928, testNum * 70);
		for (int i = 0; i < testNum; i++) {
			GameObject temp = Instantiate(gameListItem, Vector3.zero, Quaternion.identity) as GameObject;
			temp.transform.parent = scrollViewList;
			temp.GetComponent<RectTransform>().localScale = Vector3.one;
		}
		scrollBar.value = 1;
	}

	public void nav_backToMain() {
		menuState = MenuState.Main;
	}

	public void nav_onlineMode() {
		menuState = MenuState.Online;
	}
	public void nav_localMode() {
		menuState = MenuState.Local;
	}

	public void Update() {
		mainUI.shown = menuState == MenuState.Main;
		localUI.shown = menuState == MenuState.Local;
		onlineUI.shown = menuState == MenuState.Online;
	}

	public void getMatchList() {

		foreach (UnityEngine.Networking.Match.MatchDesc match in matches) {

		}
	}

	public void hostLocalGame() {
		setPort ();
		NetworkManager.singleton.StartHost ();
	}
	public void joinLocalGame() {
		NetworkManager.singleton.networkAddress = "localhost";
		setPort ();
		NetworkManager.singleton.StartClient ();
	}
	void setPort() {
		NetworkManager.singleton.networkPort = 7777;
	}

	public enum MenuState {
		Main,
		Local,
		Online
	}
}
