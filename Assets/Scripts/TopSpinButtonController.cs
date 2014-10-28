using UnityEngine;
using System.Collections;

public class TopSpinButtonController : MonoBehaviour {
    private GameObject player;
    private SkillsController skillsController;
    
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        skillsController = GameObject.FindGameObjectWithTag("Skills").GetComponent<SkillsController>();
    }
    
    // Update is called once per frame
    void Update () {
    }
    
    void OnClick(){
        skillsController.exec("topspin");
    }
}
