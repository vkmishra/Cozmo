using UnityEngine;
using System.Collections;

public class HomeScenes : MonoBehaviour {

	public AudioSource btnTap, lvlStart;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
	
	public void gameExit(){
		Application.Quit();
	}
	
	public void gameHelp(){
		Application.LoadLevel("Help");
	}

	public void gameHome(){
		Application.LoadLevel("home");
	}
	
	public void gameInfo(){
		Application.LoadLevel("Info");
	}

	public void gameSettings(){
		Application.LoadLevel("Settings");
	}

	public void Level01(){
		Application.LoadLevel("L01");
	}
	
	public void Level02(){
		Application.LoadLevel("L02");
	}
	
	public void Level03(){
		Application.LoadLevel("L03");
	}

	public void buttonTap(){
		//btnTap.PlayOneShot(btnTap.clip);
	}

	public void levelStart(){
		//lvlStart.PlayOneShot(btnTap.clip);
	}

}
