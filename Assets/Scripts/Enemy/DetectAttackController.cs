using UnityEngine;
using System.Collections;

public class DetectAttackController : MonoBehaviour {
    public EnemyController enemyController;
    PlayerController playerController;
    AudioSource attackAudio;
    AudioSource deadAudio;
	// Use this for initialization
	void Start () {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        AudioSource[] audios = GetComponents<AudioSource>();
        attackAudio = audios [0];
        deadAudio = audios [1];

        if (enemyController.getCurrentBlood() - playerController.attack <= 0)
            collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate(){       
        if (enemyController.getCurrentBlood() - playerController.attack <= 0)
            collider.isTrigger = true;
    }

    void OnCollisionEnter(Collision collision) {
        //FIXME player stop at collisionStay, next attack no damage
        if (collision.gameObject.tag == "Player")
        {
            attackAudio.Play();
            StartCoroutine(decreaseBlood(attackAudio));
        }
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            deadAudio.Play();
            StartCoroutine(decreaseBlood(deadAudio));
        }
    }

    private IEnumerator decreaseBlood(AudioSource currentAudio)
    {   
        while (currentAudio.isPlaying) {
            yield return new WaitForEndOfFrame();
        }
        
        enemyController.decreaseBlood(playerController.attack);
    }
}
