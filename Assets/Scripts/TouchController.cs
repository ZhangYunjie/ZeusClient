using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

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
		if(player.getMode() == PlayerController.PlayerMode.kModeAction ){
		  	return;
		}

		mouseStrech ();

		if ( Input.touches.Length <= 0 ) {
			return;
		}

        if (Input.touchCount == 2)
        {
            Vector2 touchPosition1 = Input.GetTouch(0).position;
            Vector2 touchPosition2 = Input.GetTouch(1).position;
        }
        else if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
			if( touch.phase == TouchPhase.Began && player.getMode() == PlayerController.PlayerMode.kModeAim )
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider.tag == "Player")
                    {
						float rayDistance;
						if (groundPlane.Raycast(ray, out rayDistance))
                        {
							origin_position = ray.GetPoint(rayDistance);
							player.setMode(PlayerController.PlayerMode.kModeStrech);
							Debug.Log("strech: " + origin_position);
						}
					}
				}
			}
			else if ( touch.phase == TouchPhase.Moved && player.getMode() == PlayerController.PlayerMode.kModeStrech ) 
            {
				float rayDistance;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(groundPlane.Raycast(ray, out rayDistance))
                {
					delta = origin_position - ray.GetPoint(rayDistance);
                    foreach (Transform t in player.GetComponentsInChildren<Transform>() ) 
                    {
                        if ( t.name == "TrailNode" ) 
                        {
                            t.rotation = Quaternion.LookRotation( delta );
                        }
                    }
				}
			}
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) 
            {
				Debug.Log(delta.sqrMagnitude);
				mouseReleased();
			}
		}
	}

	void FixedUpdate(){
		
	}

	void mousePushed()
    {
		//save began touch point
		origin_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if(hit.collider.tag == "Player" && player.getMode() == PlayerController.PlayerMode.kModeAim ){
				float rayDistance;
				if (groundPlane.Raycast(ray, out rayDistance)){
					origin_position = ray.GetPoint(rayDistance);
					player.setMode( PlayerController.PlayerMode.kModeStrech );
					Debug.Log("strech: " + origin_position);
				}
			}
		}
	}

	void mouseMoved()
    {
		if (player.getMode () != PlayerController.PlayerMode.kModeStrech)
		{
			return;
		}

		float rayDistance;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(groundPlane.Raycast(ray, out rayDistance))
        {
			delta = origin_position - ray.GetPoint(rayDistance);
			Debug.Log("delta: " + delta);
			
			foreach (Transform t in player.GetComponentsInChildren<Transform>() ) 
            {
				if ( t.name == "TrailNode" ) 
                {
					t.rotation = Quaternion.LookRotation( delta );
				}
			}
		}
	}

	void mouseReleased()
    {
		//save ended touch point
		if ( player.getMode() == PlayerController.PlayerMode.kModeStrech) 
		{
			if(delta.sqrMagnitude >= 0.2)
			{
				//#FIXME limit maximum delta
				player.strech_power = delta;
				player.setMode( PlayerController.PlayerMode.kModeEmit );
			}
			else
			{
				player.setMode( PlayerController.PlayerMode.kModeAim );
			}
		}
	}
	
	void mouseStrech()
    {
		if(Input.GetMouseButtonDown(0))
		{
			mousePushed();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			mouseReleased();
		}
		else if ( player.getMode() == PlayerController.PlayerMode.kModeStrech ) 
		{
			mouseMoved();
		}
	}
}
