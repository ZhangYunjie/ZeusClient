using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public GameObject healthPanelPrefab;
    public GameObject hp_point;
    public Camera uiCamera;
    GameObject healthPanel;
    public int bloodMax = 100;
    int currentBlood = 100;
    UISlider bloodSlider;
    // Use this for initialization
	void Start () {
        healthPanel = (GameObject)Instantiate(healthPanelPrefab, getHealthBarPosition(), new Quaternion()); 
        healthPanel.transform.parent = GameObject.Find("UI Root").transform;
        healthPanel.transform.localScale = new Vector3(1, 1, 1);
        bloodSlider = healthPanel.GetComponentInChildren<UISlider>(); 
        currentBlood = bloodMax;
	}
	
	// Update is called once per frame
	void Update () {
        healthPanel.transform.position = getHealthBarPosition();
        bloodSlider.value = currentBlood * 1.0f / bloodMax;

        if (currentBlood == 0)
        {
            dead();
        }
	}

    Vector3 getHealthBarPosition(){
        Vector3 pos = hp_point.transform.position;
        pos = Camera.main.WorldToViewportPoint(pos);
        pos = uiCamera.ViewportToWorldPoint(pos);
        pos.z = 0;

        return pos;
    }

    public int decreaseBlood(int damage){
        if(damage < 0) return -1;
        if (currentBlood - damage <= 0)
            currentBlood = 0;
        else
            currentBlood = currentBlood - damage;
        return currentBlood;
    }

    public int getCurrentBlood(){
        return currentBlood;
    }
                              

    void dead(){
        Destroy(healthPanel);
        Destroy(gameObject);
    }

}
