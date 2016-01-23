using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed, topSpeed;
	public GameObject gc;
	public Vector3 handling;
	public Vector3 v_new;
	private float hori, vert;
	private int Score, Life, needlePosition;
	private Vector3 f_vert, f_hori, v_old, rot;
	private Rigidbody rb;
	private GameController GCScript;

	void Start () {
		rb = GetComponent<Rigidbody>();
		GCScript = gc.GetComponent<GameController>();
	}

	void FixedUpdate () {
		hori = Input.GetAxis("Horizontal");
		vert = Input.GetAxis("Vertical");
		f_vert = new Vector3( 0, vert, 0);
		v_old = transform.InverseTransformDirection(rb.velocity);
		if(v_old.y <= topSpeed)
			rb.AddRelativeForce(f_vert*(topSpeed - v_old.y), ForceMode.Force);
		
		rot = new Vector3(0, 0, hori);
		rb.MoveRotation(rb.rotation * Quaternion.Euler(handling * hori));
		
		v_new = transform.InverseTransformDirection(rb.velocity);
		f_hori = new Vector3(-1*v_new.x*3, 0, 0);
		rb.AddRelativeForce(f_hori, ForceMode.Force);
	}
	
	void OnTriggerEnter(Collider blind){
		GCScript.Triggers(blind);
	}
}
