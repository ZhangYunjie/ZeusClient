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
    public float minVelocity = 1.5f;        //the velocity we use to determine its stopped
    public float minAngularVelocity = 1.0f; //the angular velocity we use to help determine if the ball is stopped.

    public float speed = 200;
    public float jumpPower = 300;
    private float distToGround;
    
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

    public bool streched
    {
        get;
        set;
    }

    public bool tap_jump
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
        strech_power = new Vector3();
        streched = false;
        is_running = false;
        tap_jump = false;
        distToGround = collider.bounds.extents.y;
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
                showArrow();
                break;
            case PlayerMode.kModeEmit:
                run();
                hideArrow();
                break;
            case PlayerMode.kModeAction:
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
        if (rigidbody.velocity.sqrMagnitude > 0.2)
        {
            is_running = true;
        } else if (is_running)
        {
            //FIXME reduce tiny movement before stop
            is_running = false;
            showArrow();
        }
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
        //#FIXME set y = 0;
        setMode(PlayerMode.kModeAction);
        rigidbody.AddForce(strech_power * speed);
    }

    private void jump()
    {
        setMode(PlayerMode.kModeAction);

        if (IsGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpPower);
        }
    }

    private void hideArrow()
    {
        foreach (Transform t in transform)
        {
            Debug.Log(t.name);
            if (t.name == "TrailNode")
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    private void showArrow()
    {
        foreach (Transform t in transform)
        {
            Debug.Log(t.name);
            if (t.name == "TrailNode")
            {
                t.gameObject.SetActive(true);
            }
        }
    }
}
