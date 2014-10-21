using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public enum PlayerMode
    {
        kModeAim = 0,
        kModeStrech,
        kModeEmit,
        kModeAction,
        kModeJump,
        kModeFall,
        kModeWin,
        NULL,
    };

    public PlayerMode mPlayerMode = PlayerMode.kModeAim;    // current mode

    public float fallTime;
    private float mCurrentFallTime;
 
    //the time the ball has to be "stopped before it registers"
    public  float requiredStoppedTime   = 1.0f;
    private float mStoppedTime         = 0f;

    public float minVelocity = 0.2f;        //the velocity we use to determine its stopped
    public float minAngularVelocity = 1.0f; //the angular velocity we use to help determine if the ball is stopped.

    public float speed = 200;
    public float jumpPower = 300;
    public float rotatePower = 100;
    private int rotateDirection = 0;
    private float distToGround;
    private Transform m_trailNode;
   
    public Vector3 strech_power
    {
        get;
        set;
    }

    public bool is_running
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
        strech_power = new Vector3();
        distToGround = collider.bounds.extents.y;

        m_trailNode = transform.Find ("TrailNode");     

        rotateDirection = 1;
        rigidbody.maxAngularVelocity = 50;
    }
    
    // Update is called once per frame
    void Update()
    {
        updatePlayer();
    }

    void updatePlayer()
    {
        switch (mPlayerMode)
        {
            case PlayerMode.kModeAim:
                enableArrow(true);
                break;
            case PlayerMode.kModeEmit:
                run();
                enableArrow(false);
                break;
            case PlayerMode.kModeAction:
                handleAction();
                break;
            case PlayerMode.kModeJump:
                jump();
                break;
            case PlayerMode.kModeFall:
                break;
            case PlayerMode.kModeWin:
                break;
            default:
                break;
        }
    }

    public void setMode(PlayerMode _mode)
    {
        mPlayerMode = _mode;
    }

    public PlayerMode getMode()
    {
        return mPlayerMode;
    }

    void FixedUpdate()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
    }
    
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void run()
    {
        initParam();
       
        //#FIXME set y = 0;
        setMode(PlayerMode.kModeAction);
        rigidbody.AddForce(strech_power * speed);
        rotate();
    }

    private void rotate()
    {
        rigidbody.AddTorque(Vector3.up * rotatePower * rotateDirection);
    }

    private void jump()
    {
        setMode(PlayerMode.kModeAction);

        if (IsGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpPower);
        }
    }

    private void handleAction()
    {
        float speed = Mathf.Abs(rigidbody.velocity.sqrMagnitude);

        if(speed < minVelocity)
        {
            mStoppedTime += Time.deltaTime;
        }
   
        if(mStoppedTime > requiredStoppedTime)
        {
            Debug.Log("mStoppedTime: " + mStoppedTime);
            Debug.Log("speed: " + speed);
            stopPlayer();
        }
    }

    private void initParam()
    {
        //reset the stopped time
        mStoppedTime = 0f;
    }

    private void enableArrow(bool _enabled)
    {
        if(m_trailNode)
        {
            m_trailNode.gameObject.SetActive(_enabled);
        }
    }

    private void stopPlayer()
    {
        if (!rigidbody.isKinematic)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        if (getMode() != PlayerMode.kModeWin)
        {
            setMode(PlayerMode.kModeAim);
        }
    }
}
