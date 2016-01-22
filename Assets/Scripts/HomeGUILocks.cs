
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using lazymonster;

public class HomeGUILocks : MonoBehaviour {

	public Button[] levelButtons;
	private List<dat> dataList;
	private Image[] locks;
	// Use this for initialization
	void Start () {
		dataList = DataHandling.GetList();
		for(int i=0; i<20; i++){
			levelButtons[i].enabled = dataList[i].unlockCheck();
			locks = levelButtons[i].GetComponentsInChildren<Image>();
			locks[1].enabled = !dataList[i].unlockCheck();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
