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
        kModeSkill,
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
    private float distToGround;
    private Transform m_trailNode;
    private Transform m_trailArrow;
   
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

        m_trailNode  = transform.Find ("TrailNode");
        m_trailArrow = m_trailNode.Find ("TrailArrow");
        enableArrow( false );

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
                enableArrow(false);
                break;
            case PlayerMode.kModeStrech:
                enableArrow(true);
                break;
            case PlayerMode.kModeEmit:
                run();
                enableArrow(false);
                break;
            case PlayerMode.kModeAction:
                handleAction();
                break;
            case PlayerMode.kModeSkill:
                break;
            case PlayerMode.kModeFall:
                break;
            case PlayerMode.kModeWin:
                showWinDialog();
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

    void showWinDialog()
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

    public void victory()
    {
        Debug.Log("victory!");

        rigidbody.isKinematic = true;
        if (rigidbody.isKinematic == false)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        setMode(PlayerMode.kModeWin);
    }

    private void run()
    {
        initParam();
       
        //#FIXME set y = 0;
        setMode(PlayerMode.kModeAction);
        rigidbody.AddForce(strech_power * speed);
    }

    private void rotate()
    {
        rigidbody.AddTorque(Vector3.back * rotatePower * 1);
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

    public void updateArrow(Vector3 delta)
    {
        float x = delta.sqrMagnitude / 2f > 1 ? delta.sqrMagnitude / 2f : 1;
        x = x < 5 ? x : 5;
        m_trailArrow.localScale = new Vector3(x, 2, 2);
        RaycastHit hitInfo;
        GameObject trail_arrow = GameObject.FindWithTag("TrailArrow");
        if (Physics.Raycast(trail_arrow.transform.position, new Vector3(delta.x, 0, delta.z), out hitInfo))
        {
            float distance = hitInfo.distance;
            float new_length = trail_arrow.GetComponent<MeshFilter>().mesh.bounds.size.x * x / 2f;
            Debug.Log(distance + " " + new_length);

            if (distance < new_length)
            {
                m_trailArrow.localScale = new Vector3( distance / trail_arrow.GetComponent<MeshFilter>().mesh.bounds.size.x, 2, 2);
            }
        }
        transform.rotation = Quaternion.LookRotation( new Vector3(delta.x, 0, delta.z) );
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
