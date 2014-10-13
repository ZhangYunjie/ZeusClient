using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
	}

	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}

	public void run(Vector3 movement){
		//#FIXME set y = 0;
		rigidbody.AddForce(movement * speed);
	}
}
