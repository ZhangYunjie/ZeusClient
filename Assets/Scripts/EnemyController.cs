using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public GameObject healthPanelPrefab;
    public GameObject hp_point;
    public Camera uiCamera;
    GameObject healthPanel;
    public float bloodMax = 100.0f;
    float currentBlood;
    UISlider bloodSlider;
    // Use this for initialization
	void Start () {
        healthPanel = (GameObject)Instantiate(healthPanelPrefab); 
        healthPanel.transform.parent = GameObject.Find("UI Root").transform;
        healthPanel.transform.localScale = new Vector3(1, 1, 1);
        bloodSlider = healthPanel.GetComponentInChildren<UISlider>(); 
        currentBlood = bloodMax;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = hp_point.transform.position;
        pos = Camera.main.WorldToViewportPoint(pos);
        pos = uiCamera.ViewportToWorldPoint(pos);
        pos.z = 0;
        pos.y = pos.y;
        healthPanel.transform.position = pos;
        bloodSlider.value = currentBlood / bloodMax;

        if (currentBlood == 0)
        {
            Destroy(healthPanel);
            Destroy(gameObject);
        }
	}

    public void decreaseBlood(int damage){
        if(damage < 0) return;
        if (currentBlood - damage <= 0)
            currentBlood = 0;
        else
            currentBlood = currentBlood - damage;
    }
}
