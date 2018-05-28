using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable {


    protected override void OnRabitHit(HeroRabbit rabit)
    {
        rabit.IsBig = true;
        this.CollectedHide();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
