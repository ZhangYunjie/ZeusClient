using UnityEngine;
using System.Collections;

public class TouchesController : MonoBehaviour
{ 
    int mDragFingerIndex = -1;
    GameObject player;
    PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        DragRecognizer dragRecognizer = GetComponent<DragRecognizer>();
        dragRecognizer.OnGesture += onDrag;

        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    
    #region FingerGestures Drag-Action
    void onDrag(DragGesture dragGesture)
    {
        // First Finger
        FingerGestures.Finger finger = dragGesture.Fingers [0];
        if (dragGesture.Phase == ContinuousGesturePhase.Started)
        {
            mDragFingerIndex = finger.Index;
            if (dragGesture.Selection == player)
            {
                playerController.setMode(PlayerController.PlayerMode.kModeStrech);
            }
        }
        else if ( mDragFingerIndex == finger.Index)
        {
            if( dragGesture.Phase == ContinuousGesturePhase.Updated )
            {
                // update the position by converting the current screen position of the finger to a world position on the Z = 0 plane
                if (playerController.getMode == PlayerController.PlayerMode.kModeStrech)
                {
                }
                else
                {
                }
            }
            else
            {
                // reset our drag finger index
                mDragFingerIndex = -1;
            }
        }
    }
    #endregion
}
