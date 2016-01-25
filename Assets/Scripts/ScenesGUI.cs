using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using lazymonster;

public class ScenesGUI : MonoBehaviour {
	public AudioSource btnTap, lvlStart;
	public Sprite mute, snd;
	public Button myBtn;
	private Configuration cfg; 

	// Use this for initialization
	void Start () {
		cfg = Settings.GetSettings();
		if(myBtn != null)
			myBtn.image.sprite = (cfg.isSoundEnabled())? snd : mute;
		btnTap.volume = cfg.getUIVolume();
		if(lvlStart != null)
			lvlStart.volume = cfg.getMusicVolume();
	}

	//Button sound
	void bs(){
		if(cfg.isSoundEnabled()) btnTap.PlayOneShot(btnTap.clip);
	}

	//Level sound
	void ls(){
		if(cfg.isSoundEnabled()) lvlStart.PlayOneShot(lvlStart.clip);
	}

	public void ToggleSound(){
		cfg.toggleSound();
		Settings.UpdateSettings(cfg);
		myBtn.image.sprite = (cfg.isSoundEnabled())? snd : mute;
	}
	
	public void ExitGame(){
		bs ();
		Application.Quit();
	}
	
	public void StartHome(){
		bs ();
		Application.LoadLevel("home");
	}
	
	public void StartInfo(){
		bs ();
		Application.LoadLevel("Info");
	}
	
	public void StartHelp(){
		bs ();
		Application.LoadLevel("Help");
	}
	
	public void StartSettings(){
		bs ();
		Application.LoadLevel("Settings");
	}
	
	public void L01(){
		ls ();
		Application.LoadLevel("L01");
	}
	
	public void L02(){
		ls ();
		Application.LoadLevel("L02");
	}
	
	public void L03(){
		ls ();
		Application.LoadLevel("L03");
	}
	
	public void L04(){
		ls ();
		Application.LoadLevel("L04");
	}

}
