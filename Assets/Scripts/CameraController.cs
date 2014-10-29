using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public enum kMode
    {
        kModeMoveFree = 0,
        kModeMoveFollow,
    }

    private kMode mCurrentMode;
    public GameObject player;
    public float thresHolder_Y;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        thresHolder_Y = 3f;
        offset = transform.position - player.transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
//        Vector3 newPos = player.transform.position + offset;
//        if (newPos.y < thresHolder_Y)
//        {
//            newPos.y = thresHolder_Y;
//        }
//        transform.position = newPos;
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");

        //transform.RotateAround(player.transform.position, Vector3.up, rotateSpeed * moveHorizontal * Time.deltaTime);
    }

    public void setMode(kMode mode)
    {
        mCurrentMode = mode;
    }

    public kMode getMode()
    {
        return mCurrentMode;
    }


}
