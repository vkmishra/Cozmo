using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using lazymonster;

public class GameController : MonoBehaviour {
	public AudioSource musc, btn;
	public Canvas GUICrash, FinishLine, GUIPause;
	public Text guiLife, guiScore, guiSpeed;
	public Image needle;
	public Sprite mute, snd;
	public RectTransform LifePanel;
	public bool IsPause, IsComplete, IsCrashed;
	public Button myBtn, b2, b3;
	public GameObject plyr;
	private Configuration cfg;
	private int Score, Life, needlePosition;
	private float LifeBarLength, currentSpeed;
	private Vector3 needleAxis;
	private PlayerMovement pMovement;

	void Start () {
		IsPause = IsComplete = IsCrashed = false;
		cfg = Settings.GetSettings();
		btn.volume = cfg.getUIVolume();
		musc.volume = cfg.getMusicVolume();
		if(cfg.isSoundEnabled()){
			musc.Play();
			myBtn.image.sprite = snd;
			b2.image.sprite = snd;
			b3.image.sprite = snd;
		}
		else{
			myBtn.image.sprite = mute;
			b2.image.sprite = mute;
			b3.image.sprite = mute;
		}

		GUICrash.enabled = FinishLine.enabled = GUIPause.enabled = false;
		Life = 50;
		Score = 0;
		LifeBarLength = LifePanel.sizeDelta.x;
		pMovement = plyr.GetComponent<PlayerMovement>();
		Time.timeScale = 1.0f;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Pause();

		guiScore.text = Score.ToString();
		guiLife.text = Life.ToString();
		guiSpeed.text = pMovement.v_new.y.ToString("##.0");
		
		LifePanel.sizeDelta = new Vector2(LifeBarLength * Life/50, LifePanel.sizeDelta.y);
		
		needlePosition = (int)needle.transform.eulerAngles.z*(-1) + 230;
		if(needlePosition < (int)pMovement.v_new.y){
			needle.transform.Rotate (0, 0, -1);
		}
		else if(needlePosition > (int)pMovement.v_new.y){
			needle.transform.Rotate (0, 0, 1);
		}
		if(Life <= 0){
			guiLife.text = "0";
			GUICrash.enabled = true;
			Time.timeScale = 0.0f;
		}

	}

	public void Pause(){
		if(IsComplete || IsCrashed)
			return;
		if(IsPause){
			IsPause = !IsPause;
			Time.timeScale = 1.0f;
			GUIPause.enabled = false;
			if(cfg.isSoundEnabled())
				musc.Play();
		}
		else{
			IsPause = !IsPause;
			Time.timeScale = 0.0f;
			GUIPause.enabled = true;
			musc.Pause();
		}
	}

	public void Completed(){
		if(IsCrashed)
			return;
		IsComplete = true;
		FinishLine.enabled = true;
		Time.timeScale = 0.0f;
		DataHandling.Complete(Application.loadedLevel-3);
	}
	
	public void Crashed(){
		if(IsComplete)
			return;
		IsCrashed = true;
		GUICrash.enabled = true;
		Time.timeScale = 0.0f;
	}
	
	public void ToggleSound(){
		cfg.toggleSound();
		Settings.UpdateSettings(cfg);
		//myBtn.image.sprite = (cfg.isSoundEnabled()) ? snd : mute;
		if(cfg.isSoundEnabled()){
			myBtn.image.sprite = snd;
			b2.image.sprite = snd;
			b3.image.sprite = snd;
			if(!IsPause)
				musc.Play();
		}
		else{
			myBtn.image.sprite = mute;
			b2.image.sprite = mute;
			b3.image.sprite = mute;
			musc.Pause();
		}
	}

	public void Triggers(Collider blind){
		if(blind.gameObject.tag == "Coin"){
			blind.gameObject.SetActive(false);
			Score += 5;
		}
		
		else if(blind.gameObject.tag == "NotACoin"){
			blind.gameObject.SetActive(false);
			Life -=7;
		}
		
		else if(blind.gameObject.tag == "CollisionPlane"){
			Crashed();
		}
		
		else if(blind.gameObject.tag == "FinishLine"){
			Completed();
		}
	}

	public void ClickMenu(){
		Application.LoadLevel("home");
	}

	public void ClickRestart(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void ClickNext(){
		Application.LoadLevel(Application.loadedLevel+1);
	}

	public void ClickButtonSound(){
		if(cfg.isSoundEnabled())
			btn.PlayOneShot(btn.clip);
	}
}
