using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using lazymonster;

public class SceneSettings: MonoBehaviour {
	public Slider uiSlider, musicSlider;
	public AudioSource btnTap;
	private Configuration cfg;

	void Start () {
		cfg = Settings.GetSettings();
		uiSlider.value = cfg.getUIVolume();
		musicSlider.value = cfg.getMusicVolume();
	}

	public void SaveChanges(){
		cfg.updateVolumeUI(uiSlider.value);
		cfg.updateVolumeMusic(musicSlider.value);
		Settings.UpdateSettings(cfg);
		btnTap.volume = cfg.getUIVolume();
	}
	
	public void LoadDefaults(){
		cfg.loadDefaults();
		uiSlider.value = cfg.getUIVolume();
		musicSlider.value = cfg.getMusicVolume();
	}
	
	public void ClearGameData(){
		DataHandling.GameReset();
		Settings.ResetSettings();
	}
}
