using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    protected virtual void OnRabitHit(HeroRabbit rabit)
    {
    }

    public bool hideAnimation = false;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.hideAnimation)
        {
            HeroRabbit rabit = collider.GetComponent<HeroRabbit>();
            if (rabit != null)
            {
                this.OnRabitHit(rabit);
                CollectedHide();
            }
        }
    }
    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
