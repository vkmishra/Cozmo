using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using lazymonster;

public class SoundScript : MonoBehaviour {
	
	public Slider uiSlider, musicSlider;
	public AudioSource btnTap, lvlStart, lvlMusic;
	public Sprite mute, snd;
	public Button myBtn;
	private Configuration cfg;
	private float uiSound, musicSound;

	// Use this for initialization
	void Start () {
		cfg = Settings.GetSettings();
		if(myBtn != null){
			if(cfg.isSoundEnabled() && myBtn!=null)
				myBtn.image.sprite = snd;		
			else if(myBtn!=null)
				myBtn.image.sprite = mute;
		}
		uiSound = cfg.getUIVolume();
		musicSound = cfg.getMusicVolume();

		if(uiSlider != null){
			uiSlider.value = uiSound = cfg.getUIVolume();
			musicSlider.value = musicSound = musicSound;
		}

		
		btnTap.volume = lvlStart.volume = uiSound;
		if(lvlMusic != null){
			lvlMusic.volume = musicSound;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonTapSound(){
		if(cfg.isSoundEnabled())
			btnTap.PlayOneShot(btnTap.clip);
	}

	public void LevelStartSound(){
		if(cfg.isSoundEnabled())
			lvlStart.PlayOneShot(lvlStart.clip);
	}

	public void ToggleSound(){
		cfg.toggleSound();
		Settings.UpdateSettings(cfg);

		if(cfg.isSoundEnabled()){
			myBtn.image.sprite = snd;
		}
		else{
			myBtn.image.sprite = mute;
			if(lvlMusic != null)
				lvlMusic.Pause();
		}
	}

	public void ChangeUISound(float value){
		uiSound = value;
	}

	public void ChangeMusicSound(float value){
		musicSound = value;
	}

	public void SaveChanges(){
		cfg.updateVolumeUI(uiSound);
		cfg.updateVolumeMusic(musicSound);
		Settings.UpdateSettings(cfg);

		btnTap.volume = lvlStart.volume = uiSound;
		if(lvlMusic != null)
			lvlMusic.volume = musicSound;
	}

	public void LoadDefaults(){
		cfg.loadDefaults();
		uiSound = cfg.getUIVolume();
		musicSound = cfg.getMusicVolume();
		uiSlider.value = cfg.getUIVolume();
		musicSlider.value = cfg.getMusicVolume();
	}

	public void ClearGameData(){
		DataHandling.GameReset();
		Settings.ResetSettings();
	}

}
