using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable {

    protected override void OnRabitHit(HeroRabbit rabit)
    {
        LevelController.current.addCoins();
        this.CollectedHide();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
