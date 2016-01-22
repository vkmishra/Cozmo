using UnityEngine;
using System.Collections;

public class planeAnimation : MonoBehaviour {

	private GameObject gm;
	private float t, s;
	// Use this for initialization
	void Start () {
		//gm = GetComponent<GameObject>();
		t = s = -0.1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if(gameObject.transform.position.y < 1.0f && gameObject.transform.position.y > -2.1f)
			gameObject.transform.Translate(Vector3.up*Time.deltaTime*t, Space.World);
		else{
			t*=-1;
			gameObject.transform.Translate(Vector3.up*Time.deltaTime*t, Space.World);
		}

		if(gameObject.transform.position.x < 4.0f && gameObject.transform.position.x > -5f)
			gameObject.transform.Translate(Vector3.right*Time.deltaTime*s*0.5f, Space.World);
		else{
			s*=-1;
			gameObject.transform.Translate(Vector3.right*Time.deltaTime*s*0.5f, Space.World);
		}
	}
}
