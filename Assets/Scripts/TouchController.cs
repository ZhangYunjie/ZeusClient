using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {
	private bool is_strech = false;
	private Vector3 origin_position;
	private Vector3 delta;
	public PlayerController playerController;
	public Plane groundPlane;

	// Use this for initialization
	void Start () {
		groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
		delta = new Vector3 ();
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
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//#FIXME not react when player is running
		//if(player.is_run){
		//	is_strech = false;
		//  return;
		//}

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

	void touchEndAction(){
		if (is_strech) {
			is_strech = false;
			
			if(delta.sqrMagnitude >= 1)
			{
				Debug.Log("player run");
				//#FIXME limit maximum delta
				playerController.run(delta);
			}
		}
	}
}
