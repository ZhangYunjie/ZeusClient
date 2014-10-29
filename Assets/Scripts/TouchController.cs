using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{ 
    int mDragFingerIndex = -1;
    GameObject player;
    PlayerController playerController;

    Plane mGroundPlane;
    Vector3 mDelta;

    // Use this for initialization
    void Start()
    {
        DragRecognizer dragRecognizer = GetComponent<DragRecognizer>();
        dragRecognizer.OnGesture += onDrag;

        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        mGroundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
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
                Debug.Log("aaa");
                playerController.setMode(PlayerController.PlayerMode.kModeStrech);
            }
        }
        else if ( mDragFingerIndex == finger.Index)
        {
            if( dragGesture.Phase == ContinuousGesturePhase.Updated )
            {
                // update the position by converting the current screen position of the finger to a world position on the Z = 0 plane
                if (playerController.getMode() == PlayerController.PlayerMode.kModeStrech)
                {
                    mDelta = getWorldPos(dragGesture.StartPosition) - getWorldPos(dragGesture.Position);
                    playerController.updateArrow(mDelta);
                }
                else
                {
                    Vector2 deltaMove = -dragGesture.DeltaMove;
                    Camera.main.transform.Translate(new Vector3(deltaMove.x / 50f, deltaMove.y / 50f, 0f));
                }
            }
            else
            {
                if ( playerController.getMode() == PlayerController.PlayerMode.kModeStrech) 
                {
                    if(mDelta.sqrMagnitude >= 0.2f)
                    {
                        //#FIXME limit maximum delta
                        playerController.strech_power = mDelta;
                        playerController.setMode( PlayerController.PlayerMode.kModeEmit );
                    }
                    else
                    {
                        playerController.setMode( PlayerController.PlayerMode.kModeAim );
                    }
                }

                mDelta = Vector3.zero;
                mDragFingerIndex = -1;
            }
        }
    }
    #endregion

    private Vector3 getWorldPos(Vector2 pos)
    {
        float rayDistance;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if(mGroundPlane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }
        return Vector3.zero;
    }
}
