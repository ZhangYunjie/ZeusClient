using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {
	private bool is_strech = false;
	private Vector3 origin_position;
	private Vector3 delta;
	public PlayerController player;
	public Plane groundPlane;

	// Use this for initialization
	void Start () {
		groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
		delta = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () {
		// not react when player is running
		if(player.is_running ){
			is_strech = false;
			if(Input.touches.Length > 0 || Input.GetMouseButtonDown(0)){
				Debug.Log("tap jump");
				player.tap_jump = true;
			}
		  	return;
		}

		mouseStrech ();

		if (Input.touches.Length == 0 && !is_strech) {
			return;
		}


		foreach( Touch touch in Input.touches ){
			if( touch.phase == TouchPhase.Began && !is_strech )
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider.tag == "Player"){
						float rayDistance;
						if (groundPlane.Raycast(ray, out rayDistance)){
							origin_position = ray.GetPoint(rayDistance);
							is_strech = true;
							Debug.Log("strech: " + origin_position);
						}
					}
				}
			}
			else if (touch.phase == TouchPhase.Moved) {
				float rayDistance;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(groundPlane.Raycast(ray, out rayDistance)){
					delta = origin_position - ray.GetPoint(rayDistance);
				}
			}
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				Debug.Log(delta.sqrMagnitude);
				touchEndAction();
			}
		}
	}

	void FixedUpdate(){
		
	}
	
	void mouseStrech(){
		if(Input.GetMouseButtonDown(0))
		{
			//save began touch point
			origin_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.collider.tag == "Player"){
					float rayDistance;
					if (groundPlane.Raycast(ray, out rayDistance)){
						origin_position = ray.GetPoint(rayDistance);
						is_strech = true;
						Debug.Log("strech: " + origin_position);
					}
				}
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			//save ended touch point
			Debug.Log(delta.sqrMagnitude);
			touchEndAction();
		}
		else if (is_strech) {
			float rayDistance;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(groundPlane.Raycast(ray, out rayDistance)){
				delta = origin_position - ray.GetPoint(rayDistance);
				Debug.Log("delta: " + delta);

				foreach (Transform t in player.GetComponentsInChildren<Transform>() ) {
					Debug.Log ( t.name );
					if ( t.name == "TrailNode" ) {
						t.rotation = Quaternion.LookRotation( delta );
					}
				}
			}
		}
	}
	void touchEndAction(){
		if (is_strech) {
			is_strech = false;
			
			if(delta.sqrMagnitude >= 0.2)
			{
				Debug.Log("player run");
				//#FIXME limit maximum delta
				player.strech_power = delta;
				player.streched = true;
			}
		}
	}
}
