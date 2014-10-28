using UnityEngine;
using System.Collections;

public class TouchesController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        DragRecognizer dragRecognizer = GetComponent<DragRecognizer>();
        dragRecognizer.OnGesture += onDrag;
	}
	
    #region FingerGestures Drag-Action
    void onDrag ( DragGesture dragGesture )
    {
        Debug.Log (" drag ");
    }
    #endregion
}
