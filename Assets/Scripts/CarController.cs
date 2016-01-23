using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using lazymonster;

public class CarController : MonoBehaviour {
	public Canvas GUICrash, FinishLine;
	public float speed, topSpeed;
	public Text guiLife, guiScore, guiSpeed;
	public Image needle;
	public RectTransform LifePanel;
	public Vector3 handling;
	private Rigidbody rb;
	private int Score, Life, needlePosition;
	private float LifeBarLength, currentSpeed;
	private Vector3 needleAxis;
	private bool IsComplete;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.interpolation = RigidbodyInterpolation.Interpolate;
		Time.timeScale = 1.0f;
		GUICrash.enabled = false;
		Score = 0;
		Life = 50;
		LifeBarLength = LifePanel.sizeDelta.x;
		LifePanel.sizeDelta.Set(50, LifePanel.sizeDelta.y);
		FinishLine.enabled = false;
		IsComplete = false;
	}
	
	// Update is called once per frame
	void Update(){
		/*else if(Input.GetKey("escape")){
			statePause = true;
			Time.timeScale = 0.0f;
		}*/
	}

	void FixedUpdate () {
		float hori = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");

		Vector3 move = new Vector3( 0, vert, 0);
		Vector3 v_old = transform.InverseTransformDirection(rb.velocity);
		if(v_old.y <= topSpeed)
			rb.AddRelativeForce(move*(topSpeed - v_old.y), ForceMode.Force);

		Vector3 rot = new Vector3(0, 0, hori);
		rb.MoveRotation(rb.rotation * Quaternion.Euler(handling * hori));

		Vector3 v_new = transform.InverseTransformDirection(rb.velocity);
		Vector3 f_hori = new Vector3(-1*v_new.x*3, 0, 0);
		rb.AddRelativeForce(f_hori, ForceMode.Force);

		guiScore.text = Score.ToString();
		guiLife.text = Life.ToString();
		guiSpeed.text = v_new.y.ToString("##.0");

		LifePanel.sizeDelta = new Vector2(LifeBarLength * Life/50, LifePanel.sizeDelta.y);

		needlePosition = (int)needle.transform.eulerAngles.z*(-1) + 230;
		if(needlePosition < (int)v_new.y){
			needle.transform.Rotate (0, 0, -1);
		}
		else if(needlePosition > (int)v_new.y){
			needle.transform.Rotate (0, 0, 1);
		}
		if(Life <= 0){
			guiLife.text = "0";
			GUICrash.enabled = true;
			Time.timeScale = 0.0f;
		}
	}

	void OnTriggerEnter(Collider blind){
		if(blind.gameObject.tag == "Coin"){
			blind.gameObject.SetActive(false);
			Score += 5;
		}

		else if(blind.gameObject.tag == "NotACoin"){
			blind.gameObject.SetActive(false);
			Life -=7;
		}

		else if(blind.gameObject.tag == "CollisionPlane"){
			GUICrash.enabled = true;
			Time.timeScale = 0.0f;
		}

		else if(blind.gameObject.tag == "FinishLine"){
			FinishLine.enabled = true;
			IsComplete = true;
			Time.timeScale = 0.0f;
			DataHandling.Complete(Application.loadedLevel-3);
		}
	}
}
