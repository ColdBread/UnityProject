using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Collectable {

    protected override void OnRabitHit(HeroRabbit rabit)
    {
        LevelController.current.addFruits(1);
        this.CollectedHide();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
