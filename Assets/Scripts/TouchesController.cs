using UnityEngine;
using System.Collections;

public class TouchesController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        DragRecognizer dragRecognizer = GetComponent<DragRecognizer>();
        dragRecognizer.OnGesture += onDrag;

        FingerDownDetector fingerDownDector = GetComponent<FingerDownDetector>();
        fingerDownDector += onFingerDown;
    }
    
    #region FingerGestures Drag-Action
    void onDrag(DragGesture dragGesture)
    {
        // First Finger
        FingerGestures.Finger finger = dragGesture.Fingers [0];
        if (dragGesture == ContinuousGesturePhase.Started)
        {
            if (dragGesture.Selection == GameObject.FindWithTag("Player"))
            {
            }
            else
            {
            }
        }
    }
    #endregion

    #region FingerDownEvent FingerDown-Action
    void onFingerDown(FingerDownEvent fingerDownEvent)
    {
        Debug.Log(" Finger Down");
    }
    #endregion
}
