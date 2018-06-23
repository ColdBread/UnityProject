using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : Collectable {

	public Crystal color;

    protected override void OnRabitHit(HeroRabbit rabit)
    {
        LevelController.current.addCrystals(this.color);
        this.CollectedHide();
    }

	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
