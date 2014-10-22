using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIProgressBar))] 
public class RotateSliderController : MonoBehaviour {

    public enum RotateSliderMode
    {
        kModeHide = 0,
        kModeScan,
        kModeStop,
        NULL,
    };

    public RotateSliderMode mode  // current mode
    {
        get;
        set;
    }

    UIProgressBar mBar;
    public float value;
    int direction;
    public float clockwiseRotateBoundry = 0.3f;
    public float counterClockwiseRotateBoundry = 0.7f;
    bool is_scan = false;

	// Use this for initialization
	void Start () {
        mBar = GetComponent<UIProgressBar>();
        value = mBar.value;
        direction = 1;
        is_scan = false;
        mode = RotateSliderMode.kModeHide;
        mBar.alpha = 0;
	}

    void resetParams(){
        is_scan = false;
        value = 0;
        mBar.value = 0;
        direction = 1;
    }

    void disactivate(){
        mBar.alpha = 0;
    }

    void activate(){
        mBar.alpha = 1;
    }
	
	// Update is called once per frame
	void Update () {
        switch (mode)
        {
            case RotateSliderMode.kModeHide:
                disactivate();
                resetParams();
                break;
            case RotateSliderMode.kModeScan:
                activate();
                is_scan = true;
                break;
            case RotateSliderMode.kModeStop:
                activate();
                is_scan = false;
                break;
            default:
                break;
        }
	}

    void FixedUpdate(){
        if(is_scan) scan();
    }

    void scan(){
        value += direction * Time.deltaTime;
        if (value >= 1)
        {
            value = 1;
            direction = - direction;
        } else if (value <= 0)
        {
            value = 0;
            direction = - direction;
        }
        mBar.value = value;
    }

    public float getRotateDirection()
    {
        if (value <= clockwiseRotateBoundry)
            return 1;
        else if (value >= counterClockwiseRotateBoundry)
            return -1;
        else
            return 0;
    }
}
