using UnityEngine;
using System.Collections;

public class DetectAttackController : MonoBehaviour {
    public EnemyController enemyController;
    PlayerController playerController;
	// Use this for initialization
	void Start () {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player")
        {
            enemyController.decreaseBlood(playerController.attack);
        }
        
    }
}
