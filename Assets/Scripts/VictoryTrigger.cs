using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour 
{
    private GameObject mPlayerObj;
    private PlayerController mPlayerController;
 
	// Use this for initialization
	void Start () {
        mPlayerObj = GameObject.FindWithTag("Player");

        if (mPlayerObj)
        {
            mPlayerController = mPlayerObj.GetComponent<PlayerController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter (Collider collider)
    {
        Debug.Log("OnTriggerEnter");
        if (collider.gameObject == mPlayerObj)
        {
            if (mPlayerController)
            {
                mPlayerController.victory();
            }
        }
    }
}
