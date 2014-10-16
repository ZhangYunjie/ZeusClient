using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;
	}

	void FixedUpdate(){
		//float moveHorizontal = Input.GetAxis("Horizontal");

		//transform.RotateAround(player.transform.position, Vector3.up, rotateSpeed * moveHorizontal * Time.deltaTime);
	}

}
