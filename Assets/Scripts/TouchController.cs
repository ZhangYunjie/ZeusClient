using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{ 
    public enum TouchMode
    {
        NULL = 0,
        kModeDrag,
        kModePinch,
    }
    TouchMode mCurrentMode = TouchMode.NULL;

    int   mDragFingerIndex = -1;

    GameObject       mPlayer;
    PlayerController mPlayerController;
    CameraController mCameraController;
    Plane            mGroundPlane;
    Vector3          mDelta;

    // Use this for initialization
    void Start()
    {
        DragRecognizer dragRecognizer = GetComponent<DragRecognizer>();
        dragRecognizer.OnGesture += onDrag;

        PinchRecognizer pinchRcognizer = GetComponent<PinchRecognizer>();
        pinchRcognizer.OnGesture += onPinch;

        mPlayer = GameObject.FindWithTag("Player");
        mPlayerController = mPlayer.GetComponent<PlayerController>();
        mCameraController = Camera.main.GetComponent<CameraController>();

        mGroundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
    }
    
    #region FingerGestures Drag-Action
    void onDrag(DragGesture dragGesture)
    {
        if (dragGesture.Fingers.Count != 1)
        {
            return;
        }

        if (mCurrentMode > TouchMode.kModeDrag)
        {
            if (mPlayerController.getMode() == PlayerController.PlayerMode.kModeStrech) 
            {
                mPlayerController.setMode( PlayerController.PlayerMode.kModeAim );
            }
            mDelta           = Vector3.zero;
            mDragFingerIndex = -1;
            return;
        }
    
        // First Finger
        FingerGestures.Finger finger = dragGesture.Fingers [0];
        if (dragGesture.Phase == ContinuousGesturePhase.Started)
        {
            mDragFingerIndex = finger.Index;
            if (dragGesture.Selection == mPlayer && mPlayerController.getMode() == PlayerController.PlayerMode.kModeAim)
            {
                mPlayerController.setMode(PlayerController.PlayerMode.kModeStrech);
            }
            mCurrentMode = TouchMode.kModeDrag;
        }
        else if ( mDragFingerIndex == finger.Index)
        {
            if( dragGesture.Phase == ContinuousGesturePhase.Updated )
            {
                // update the position by converting the current screen position of the finger to a world position on the Z = 0 plane
                if (mPlayerController.getMode() == PlayerController.PlayerMode.kModeStrech)
                {
                    mDelta = getWorldPos(dragGesture.StartPosition) - getWorldPos(dragGesture.Position);
                    mPlayerController.updateArrow(mDelta);
                }
                else
                {
                    // TODO:when translate the camera, do not use the smooth move.
                    mCameraController.translateCamera(-dragGesture.DeltaMove);
                }
            }
            else
            {
                if ( mPlayerController.getMode() == PlayerController.PlayerMode.kModeStrech) 
                {
                    if(mDelta.sqrMagnitude >= 0.2f)
                    {
                        //#FIXME limit maximum delta
                        mPlayerController.strech_power = mDelta;
                        mPlayerController.setMode( PlayerController.PlayerMode.kModeEmit );
                    }
                    else
                    {
                        mPlayerController.setMode( PlayerController.PlayerMode.kModeAim );
                    }
                }

                mDelta           = Vector3.zero;
                mCurrentMode     = TouchMode.NULL;
                mDragFingerIndex = -1;
            }
        }
    }
    #endregion

    #region FingerGestures Pinch-Action
    void onPinch(PinchGesture pinchGesture)
    {
        if (mCurrentMode > TouchMode.kModePinch)
        {
            return;
        }

        if(pinchGesture.Phase == ContinuousGesturePhase.Started)
        {
            mCurrentMode = TouchMode.kModePinch;
        }
        else if(pinchGesture.Phase == ContinuousGesturePhase.Updated)
        {
            if (mCurrentMode == TouchMode.kModePinch)
            {
                mCameraController.pinchCamera(pinchGesture.Delta);
            }
        }
        else
        {
            mCurrentMode = TouchMode.NULL;
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
