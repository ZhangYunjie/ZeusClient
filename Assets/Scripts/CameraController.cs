using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public enum MoveMode
    {
        kModeMoveFree = 0,
        kModeMoveSmooth,
        kModeMoveFollow,
        kModeMoveStop,
        kModeMoving,
    }

    public GameObject player;
    public float thresHolder_Y;
    public Vector3 cameraOffset;

    public float mZoomScaleFactor = 20f;
    public float mMoveScaleFactor = 50f;
    
    private MoveMode mCurrentMode = MoveMode.kModeMoveFree;

    // Use this for initialization
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        this.transform.position = playerObj.transform.position + cameraOffset;
        this.transform.LookAt(playerObj.transform.position);
    }
    
    // Update is called once per frame
    void Update()
    {
        switch(mCurrentMode)
        {

            case MoveMode.kModeMoveSmooth:
                moveSmooth();
                break;
            case MoveMode.kModeMoveFollow:
                followPlayer();
                break;
            case MoveMode.kModeMoveFree:
            case MoveMode.kModeMoveStop:
            case MoveMode.kModeMoving:
            default:
                break;
        }
    }

    void FixedUpdate()
    {

    }

    public void setMode(MoveMode mode)
    {
        mCurrentMode = mode;
    }

    public MoveMode getMode()
    {
        return mCurrentMode;
    }

    public void translateCamera(Vector2 deltaMove)
    {
        if(mCurrentMode == MoveMode.kModeMoveStop) return;
        Vector3 moveDistance = deltaMove.y * transform.forward / mMoveScaleFactor + deltaMove.x * transform.right /mMoveScaleFactor;
        moveDistance.y = 0f;
        transform.Translate(moveDistance, Space.World);

        if(mCurrentMode == MoveMode.kModeMoveFollow) mCurrentMode = MoveMode.kModeMoveSmooth;
    }

    public void pinchCamera(float pinchDelta)
    {
        if(mCurrentMode == MoveMode.kModeMoveStop) return;
        float delta = pinchDelta / mZoomScaleFactor;
        Camera.main.transform.Translate(new Vector3(0f, 0f, delta));
    }

    private void moveSmooth()
    {
        Vector3 delta = player.transform.position + cameraOffset - transform.position;
        if(delta.sqrMagnitude < 1.0f)
        {
            mCurrentMode = MoveMode.kModeMoveFollow;
            return;
        }
        transform.Translate(delta * 1.0f / delta.sqrMagnitude, Space.World);
    }
    
    private void followPlayer()
    {      
        Vector3 newPos = player.transform.position + cameraOffset;
        if (newPos.y < thresHolder_Y)
        {
            mCurrentMode = MoveMode.kModeMoveStop;
            return;
        }
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
  
    }
    
    
}
