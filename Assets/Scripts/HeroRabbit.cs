using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour
{

    public float speed = 1;
    Rigidbody2D myBody = null;
    Transform heroParent = null;
    Animator animator;
    public static HeroRabbit lastRabit = null;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    public float DyingTime = 1f;
    float time_to_wait;
    private bool isDying = false;
    

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
        this.heroParent = this.transform.parent;
        time_to_wait = DyingTime;
    }

    void Awake(){
        lastRabit = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying) {
            time_to_wait -= Time.deltaTime;
            if(time_to_wait <= 0) {
                isDying = false;
                animator.SetBool("die",false);
                time_to_wait = DyingTime;
                LevelController.current.onRabitDeath(this);
            }
        }
    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }

    void FixedUpdate()
    {
        if(!isDying){
        Run();

        Jump();
        }
    }

    public void Die()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("die", true);
        this.isDying = true;
    }

    private void Run()
    {
        float value = Input.GetAxis("Horizontal");

        animator = GetComponent<Animator>();

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
    }

    private void Jump()
    {
        Vector3 from = transform.position + Vector3.up * 0.6f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
            isGrounded = false;
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);

        
        
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }

        Animator animator = GetComponent<Animator>();
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
    }

    bool isBig;
    public bool IsBig
    {
        get
        {
            return isBig;
        }
        set
        {
            if (value)
            {
                this.transform.localScale = new Vector3(2, 2, 2);
            }
            else
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            isBig = value;
        }
    }
}
