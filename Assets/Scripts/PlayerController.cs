using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float jumpPower = 1000;
	private float distToGround;
	
	public Vector3 strech_power {
		get;
		set;
	}

	public bool is_running {
		get;
		set;
	}

	public bool streched {
		get;
		set;
	}

	public bool tap_jump {
		get;
		set;
	}

	// Use this for initialization
	void Start () {
		strech_power = new Vector3 ();
		streched = false;
		is_running = false;
		tap_jump = false;
		distToGround = collider.bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () {
//		RaycastHit hit; 
//		Vector3 p1 = transform.position; 
//		Vector3 p2 = p1 + Vector3.forward * 0.5f; 
//		if(Physics.CapsuleCast(p1, p2, 0.0f, transform.forward, out hit, 0.1f)) {
//			//停止该角色 
//			rigidbody.velocity = new Vector3();
//		}
	}

	void FixedUpdate() {
		if (streched && !is_running) {
			run ();
			streched = false;
		}

		if (tap_jump && IsGrounded() && is_running) {
			Debug.Log("jump");
			jump();
			tap_jump = false;
		}

		if (rigidbody.velocity.sqrMagnitude > 0.1) {
			is_running = true;
		} else {
			Vector3 current_velocity = rigidbody.velocity;
			rigidbody.velocity = new Vector3(0, current_velocity.y, 0);
			is_running = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.gameObject.tag);
	}

	
	public bool IsGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	private void run(){
		//#FIXME set y = 0;
		rigidbody.AddForce(strech_power * speed);
	}

	private void jump(){
		rigidbody.AddForce( Vector3.up * jumpPower );
	}
}
