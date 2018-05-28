using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

    protected override void OnRabitHit(HeroRabbit rabit)
    {
        if (rabit.IsBig)
        {
            rabit.IsBig = false;
        }
        else
        {
            rabit.Die();
        }
        this.CollectedHide();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
