using UnityEngine;
using System.Collections;

public class UIRootVerticalAdjuster : MonoBehaviour {

    public int manualWidth = 640;

	// Use this for initialization
	void Start () 
    {
		UIRoot root = this.GetComponent<UIRoot>();
		root.manualHeight = Mathf.FloorToInt(Screen.height * this.manualWidth / Screen.width);
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
}
