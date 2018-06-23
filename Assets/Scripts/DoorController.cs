using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public string scene_name = "Level1";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider)
    {
        
            HeroRabbit rabit = collider.GetComponent<HeroRabbit>();
            if (rabit != null)
            {
                SceneManager.LoadScene(scene_name);
                
            }
        
    }
}
