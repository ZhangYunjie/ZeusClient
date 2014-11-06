using UnityEngine;
using System.Collections.Generic;

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
    public SkillsController skills;
    public float fallTime;
    private float mCurrentFallTime;
 
    //the time the ball has to be "stopped before it registers"
    public  float requiredStoppedTime = 2.0f;
    private float mStoppedTime = 0f;
    public float minVelocity = 0.2f;        //the velocity we use to determine its stopped
    public float minAngularVelocity = 1.0f; //the angular velocity we use to help determine if the ball is stopped.

    public float speed = 200;
    private float distToGround;
    private Transform m_trailNode;
    private Transform m_trailArrow;
    private GameObject m_trailArrowMesh;
    public SkillStatus skillStatus = new SkillStatus();
    public int attack = 40;
    
    public Vector3 strech_power
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
        strech_power = new Vector3();
        distToGround = collider.bounds.extents.y;

        m_trailNode = transform.Find("TrailNode");
        m_trailArrow = m_trailNode.Find("TrailArrow");
        m_trailArrowMesh = GameObject.FindWithTag("TrailArrow");
        enableArrow(false);

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
                skillWait();
                enableCharacter(true);
                enableArrow(false);
                break;
            case PlayerMode.kModeStrech:
                enableArrow(true);
                break;
            case PlayerMode.kModeEmit:
                run();
                skillReady();
                enableCharacter(false);
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

    private void skillWait()
    {
        skills.mode = SkillsController.SkillMode.kModeWait;
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

    private void skillReady()
    { 
        skills.mode = SkillsController.SkillMode.kModeReady;
    }

    private void handleAction()
    {
        float speed = Mathf.Abs(rigidbody.velocity.sqrMagnitude);

        if (speed < minVelocity)
        {
            mStoppedTime += Time.deltaTime;
        }
   
        if (mStoppedTime > requiredStoppedTime)
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
        if (m_trailNode)
        {
            m_trailNode.gameObject.SetActive(_enabled);
        }
    }
  
    private void enableCharacter(bool _enabled)
    {
        transform.Find("Character").gameObject.SetActive(_enabled);
        if (_enabled)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
        }
    }

    public void updateArrow(Vector3 delta)
    {
        float x = delta.sqrMagnitude / 2f > 1 ? delta.sqrMagnitude / 2f : 1;
        x = x < 5 ? x : 5;
        m_trailArrow.localScale = new Vector3(x, 2, 2);
        RaycastHit hitInfo;
        if (Physics.Raycast(m_trailArrowMesh.transform.position, new Vector3(delta.x, 0, delta.z), out hitInfo))
        {
            float distance = hitInfo.distance;
            float new_length = m_trailArrowMesh.GetComponent<MeshFilter>().mesh.bounds.size.x * m_trailArrow.localScale.x / 2f;

            if (distance < new_length + 0.1f)
            {
                m_trailArrow.localScale = new Vector3((distance - 0.1f) / m_trailArrowMesh.GetComponent<MeshFilter>().mesh.bounds.size.x, 2, 2);
            }
        }
        transform.rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.z));
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

        reset();
    }

    private void reset()
    {
        skillStatus.reset();
    }

    void OnCollisionExit(Collision collisionInfo) {
        print("No longer in contact with " + collisionInfo.transform.name + ", " + collisionInfo.transform.tag);
        if (skillStatus.reflect_plus && collisionInfo.transform.tag != "Ground")
        {
            rigidbody.velocity = rigidbody.velocity * skillStatus.getReflectPlusRate();
        }
    }
}
