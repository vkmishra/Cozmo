using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using lazymonster;

public class GUIScript : MonoBehaviour {
	public Canvas guiCanvas;
	public AudioSource musc;
	private Configuration cfg;
	//private bool statePause;
	// Use this for initialization
	void Start () {
		guiCanvas.enabled=false;
		cfg = Settings.GetSettings();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale>0.0f){
			//statePause = true;
			Time.timeScale = 0.0f;
			guiCanvas.enabled = true;
			musc.Pause();
			cfg = Settings.GetSettings();
		}
		else if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale < 1.0f){
			Time.timeScale = 1.0f;
			guiCanvas.enabled = false;
			cfg = Settings.GetSettings();
		}
		if(!musc.isPlaying && Time.timeScale > 0.0f && cfg.isSoundEnabled())
			musc.Play();
	
	}

	public void menuClick(){
		Application.LoadLevel("home");
	}

	public void restartClick(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void NextLevel(){
		Application.LoadLevel(Application.loadedLevel+1);
	}
}
