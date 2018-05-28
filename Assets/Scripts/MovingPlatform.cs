using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    public float speed = 1;
    Rigidbody2D myBody = null;
    public float pausetime = 2;
    float time_to_wait;
    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        time_to_wait = pausetime;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

    bool going_to_a = false;
    
    bool pause = false;
    

	// Update is called once per frame
	void Update () {
        
        Vector3 my_pos = this.transform.position;
        Vector3 target;
        if (going_to_a)
        {
            target = this.pointA;
        }
        else
        {
            target = this.pointB;
        }
        if (isArrived(my_pos,target))
        {
            myBody.velocity = Vector3.zero;
            pause = true;
            
            time_to_wait -= Time.deltaTime;
            if (time_to_wait <= 0)
            {
                pause = false;
                going_to_a = !going_to_a;
                time_to_wait = pausetime;
            }
        }

        if (!pause)
        {
            Vector3 destination = target - my_pos;
            destination.z = 0;

            float spd = (float)(speed * Time.deltaTime);
            myBody.velocity = destination.normalized;
            myBody.velocity.Scale(new Vector3(spd, spd, 0));
        }
	}
}
