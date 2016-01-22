using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using lazymonster;

public class UpdateValuesSetting : MonoBehaviour {
	public Slider uiSlider, musicSlider;
	private Configuration cfg;
	// Use this for initialization
	void Start () {
		cfg = Settings.GetSettings();
		uiSlider.value = cfg.getUIVolume();
		musicSlider.value = cfg.getMusicVolume();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
