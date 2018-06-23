using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

	public float life_time;
	public float speed;
	float direction;
	Rigidbody2D myBody = null;
	SpriteRenderer sr;

	void Start () {
		StartCoroutine (destroyLater());
		myBody = this.GetComponent<Rigidbody2D>();
		sr = this.GetComponent<SpriteRenderer>();
	}

	public void launch(float direction){
		this.direction = direction;
	}

	IEnumerator destroyLater(){
		yield return new WaitForSeconds(life_time);
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		FlyUpdate();
	}

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

	void FlyUpdate(){
		float value = this.direction;
        Vector2 vel = myBody.velocity;
        vel.x = value * speed;
        myBody.velocity = vel;
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }
	}
}
