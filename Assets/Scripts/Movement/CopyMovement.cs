using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMovement : MonoBehaviour
{
    #region variables
    [SerializeField] private LayerMask jumpableGround;

    public BoxCollider2D gcheck;
    private BoxCollider2D coll;
    private Rigidbody2D rb2d;
    private Animator anim;
    public Transform Indicator;

    public AudioSource source;
    public AudioClip clipJump;
    public AudioClip clipDoubleJump;
    public AudioClip clipLand;
    public AudioClip clipCharge;
    public AudioClip clipLandTwo;


    float charger = 2f;
    float timerLeft;
    float timerRight;

    bool Firstjump = false;
    bool Secondjump = false;
    bool InAir = false;
    bool CanJump = true;
    bool CantJump = false;
    bool CanDoubleJump = false;

    bool bounceFromLeftWall = false;
    bool bounceFromRightWall = false;

    public bool landSound;

    public bool isGrounded;

    #endregion
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        #region GroundAirCheck

        bool IsGrounded()
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        }
        bool CollidingWallLeft()
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, 0.1f, jumpableGround);
        }
        bool CollidingWallRight()
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, 0.1f, jumpableGround);
        }
        if (CollidingWallLeft())
        {
            timerLeft += 1;
            if(timerLeft > 0)
            {
                bounceFromLeftWall = true;
            }
        }
        else{timerLeft = 0;}

        if (CollidingWallRight())
        {
            timerRight += 1;
            if (timerRight > 0)
            {
                bounceFromRightWall = true;
            }
        }
        else{timerRight = 0;}


        if (IsGrounded())
        {
            InAir = false;

            if (landSound == true)
            {
                source.PlayOneShot(clipLandTwo);
                landSound = false;
            }
        }
        else
        {
            InAir = true;
        }
        #endregion
        #region ChargesJump
        // charges thrust
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            source.PlayOneShot(clipCharge);
            if (charger < 2.6)
            {
                charger += (Time.deltaTime * 1);
            }
            
            if(charger > 2.6)
            {
                Firstjump = true;
                CanJump = false;
                source.Stop();
            }
        }
        #endregion
        #region Prevents sliding
        float A = 80;
        float B = 280;
        if (Indicator.transform.rotation.eulerAngles.z >= A && Indicator.transform.rotation.eulerAngles.z <= B)
        {
            CantJump = true;
        }
        else
        {
            CantJump = false;
        }
        #endregion
        #region Jumps
        // initiates first jump
        if (Input.GetKeyUp(KeyCode.Space) && IsGrounded() && CanJump && !CantJump && !CollidingWallLeft() && !CollidingWallRight())
        {
            Firstjump = true;
            CanJump = false;
        }
        // initiates second jump
        if (Input.GetKeyDown(KeyCode.Space) && InAir && CanDoubleJump)
        {
            Secondjump = true;
        }
        #endregion
    }
    void FixedUpdate()
    {
        #region Jumps
        //First Jump
        if (Firstjump)
        {
            float JumpForce = 10000 * charger;
            rb2d.AddForce(Indicator.transform.up * Time.deltaTime * JumpForce);
            source.PlayOneShot(clipJump);
            Firstjump = false;
            landSound = true;
            charger = 2f;
            StartCoroutine(WaitDoubleJump());
            StartCoroutine(WaitForJump());
        }
        //Second Jump
        if (Secondjump)
        {
            float JumpForce = 20000;
            rb2d.velocity = new Vector2(0f, 0f);//changes velocity to 0
            rb2d.AddForce(Indicator.transform.up * Time.deltaTime * JumpForce);
            source.PlayOneShot(clipDoubleJump);
            Secondjump = false;
            landSound = true;
            CanDoubleJump = false;
        }
        #endregion
        #region LeftToRightMovement
        //moves the player left
        if (Input.GetKey(KeyCode.A) && !InAir)
        {
            anim.SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = false;
            transform.position = new Vector2(transform.position.x + -0.1f, transform.position.y);
        }

        //moves the player right
        if (Input.GetKey(KeyCode.D) && !InAir)
        {
            anim.SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
        }
        if (Input.GetKey(KeyCode.C))
        {
            anim.SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        #endregion
        #region BounceWall
        if (bounceFromLeftWall)
        {
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.AddForce(Vector2.right, ForceMode2D.Impulse);
            if(InAir == true)
            {
                source.PlayOneShot(clipLand);
            }
            bounceFromLeftWall = false;
        }
        if (bounceFromRightWall)
        {
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.AddForce(Vector2.left, ForceMode2D.Impulse);
            if (InAir == true)
            {
                source.PlayOneShot(clipLand);
            }
            bounceFromRightWall = false;
        }
        #endregion
    }

    #region IEnumerators
    IEnumerator WaitDoubleJump()
    {
        yield return new WaitForSeconds(0.3f);
        CanDoubleJump = true;
    }
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(1f);
        CanJump = true;
    }
    #endregion
}
