using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour 
{
    private GameObject mPlayerObj;
    private GameObject mWinDialog;
    private PlayerController mPlayerController;
 
	// Use this for initialization
	void Start () {
        mPlayerObj = GameObject.FindWithTag("Player");
        mWinDialog = GameObject.FindWithTag("WinDialog");

        if (mPlayerObj)
        {
            mPlayerController = mPlayerObj.GetComponent<PlayerController>();
        }
        if (mWinDialog)
        {
            mWinDialog.SetActive(false);
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
                mWinDialog.SetActive(true);
            }
        }
    }
}
