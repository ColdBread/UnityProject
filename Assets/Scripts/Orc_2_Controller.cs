using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_2_Controller : MonoBehaviour {

    public float speed = 1;
    public float death_time = 0.1f;
    public float attack_time = 2f;
    Rigidbody2D myBody = null;
    Animator animator;
    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    Mode mode = Mode.GoToA;
    bool isDying = false;
    bool attacking = false;
    SpriteRenderer sr;


    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying)
        {
            RunUpdate();
            HitUpdate();
        }
        else
        {
            Vector2 vel = myBody.velocity;
            vel.x = 0;
            myBody.velocity = vel;
        }
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

    void HitUpdate()
    {
        Vector3 my_pos = this.transform.position;
        Vector3 rabit_pos = HeroRabbit.lastRabit.transform.position;
        my_pos.z = 0;
        rabit_pos.z = 0;
        if (Mathf.Abs(rabit_pos.x - my_pos.x) < 1f)
        {
            if (Mathf.Abs(rabit_pos.y - my_pos.y) > 1f)
            {
                if (Mathf.Abs(rabit_pos.y - my_pos.y) < 1.7f)
                {
                    StartCoroutine(Die());
                }
            }
            else if (!attacking)
            {

                animator.SetTrigger("Attack");
                StartCoroutine(reloadAttack());
                HeroRabbit.lastRabit.Die();
            }
        }
    }

    IEnumerator reloadAttack()
    {
        attacking = true;
        yield return new WaitForSeconds(attack_time);
        attacking = false;
    }

    IEnumerator Die()
    {
        animator.SetBool("Die", true);
        isDying = true;
        yield return new WaitForSeconds(death_time);
        animator.SetBool("Die", false);
        Destroy(this.gameObject);
    }

    void RunUpdate()
    {

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
        
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }
    }

    float getDirection()
    {
        Vector3 my_pos = this.transform.position;
        Vector3 rabit_pos = HeroRabbit.lastRabit.transform.position;
        if (Mathf.Abs(rabit_pos.x - my_pos.x) < 5.0f)
        {
            mode = Mode.Attack;
        }
        else
        {
            if (mode == Mode.Attack)
                mode = Mode.GoToA;
        }
        if (attacking)
        {
            return 0;
        }

        if (mode == Mode.GoToA)
        {
            if (isArrived(my_pos, pointA))
            {
                mode = Mode.GoToB;
            }
            else
            {
                return -1;
            }

        }
        else if (mode == Mode.GoToB)
        {
            if (isArrived(my_pos, pointB))
            {
                mode = Mode.GoToA;
            }
            else
            {
                return 1;
            }
        }
        else if (mode == Mode.Attack)
        {
            if (my_pos.x < rabit_pos.x)
            {
                sr.flipX = true;
                return 0;
            }
            else
            {
                sr.flipX = false;
                return -1;
            }
        }

        return 0;
    }
}
