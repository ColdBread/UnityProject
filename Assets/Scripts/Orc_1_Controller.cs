using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_1_Controller : MonoBehaviour {

	public float speed = 1;
    Rigidbody2D myBody = null;
	Animator animator;
	public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
	Mode mode = Mode.GoToA;
	

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
		animator = this.GetComponent<Animator>();
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
	}
	
	// Update is called once per frame
	void Update () {
		RunUpdate();
		HitUpdate();
	}

	bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

	void HitUpdate(){
		Vector3 my_pos = this.transform.position;
		Vector3 rabit_pos = HeroRabbit.lastRabit.transform.position;
		my_pos.z = 0;
		rabit_pos.z = 0;
		if (Vector3.Distance(my_pos, rabit_pos) < 1f) {
			if(Mathf.Abs(rabit_pos.y - my_pos.y) > 1.5f 
				&& Mathf.Abs(rabit_pos.y - my_pos.y) < 2f){
					this.Die();
			} else {
				animator.SetTrigger("Attack");
				HeroRabbit.lastRabit.Die();
			}
		}
	}

	void Die() {
		
	}

	void RunUpdate(){
		
		float value = this.getDirection();
		if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }
	}

	float getDirection(){
		Vector3 my_pos = this.transform.position;
		Vector3 rabit_pos = HeroRabbit.lastRabit.transform.position;
		if (rabit_pos.x > Mathf.Min(pointA.x, pointB.x) 
			&& rabit_pos.x < Mathf.Max (pointA.x, pointB.x)){
				mode = Mode.Attack;
			} else {
				if(mode == Mode.Attack)
					mode = Mode.GoToA;
			}
		if(mode == Mode.GoToA){
			if(isArrived(my_pos,pointA)){
				mode = Mode.GoToB;
			} else {
				return -1;
			}
			
		} else if (mode == Mode.GoToB){
			if(isArrived(my_pos,pointB)){
				mode = Mode.GoToA;
			} else {
				return 1;
			}
		} else if (mode == Mode.Attack){
			if(my_pos.x < rabit_pos.x) {
				return 1;
			} else {
				return -1;
			}
		}
		return 0;
	}
}
