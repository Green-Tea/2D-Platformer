using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 2.5f;
    [SerializeField] float jumpSpeed = 6.0f;
    Vector2 moveInput;
    Rigidbody2D rgbd2D;

    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    public GameObject bullet;
    public Transform bulletHole;
    public int force = 100;
    public bool isFacingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        rgbd2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        Run();
        FlipSprite();
        myAnimator.SetBool("isGrounded", IsGrounded());

        if (direction > 0){
            isFacingRight = true;
        }else if (direction < 0){
            isFacingRight = false;
        }
    }

    void OnFire(InputValue value)
    {
        myAnimator.SetTrigger("shoot");
        GameObject go = Instantiate(bullet, bulletHole.position, bullet.transform.rotation);
        if (isFacingRight)
        {
            go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
        } else {
            go.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        if (IsGrounded() || !IsTouchingSides())
        {
            Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rgbd2D.velocity.y);
            rgbd2D.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed)
            {
                myAnimator.SetBool("isRunning", true);
            }
            else
            {
                myAnimator.SetBool("isRunning", false);
            }
        }
    }


    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rgbd2D.velocity.x), 1f);
        }
    }

    void OnJump(InputValue value)
    {
        if(!IsGrounded() || IsTouchingSides()) { return; }
        if(value.isPressed)
        {
            rgbd2D.velocity += new Vector2(0, jumpSpeed);
        }
        
    }

    bool IsGrounded()
    {
        return myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsTouchingSides()
    {
        CapsuleCollider2D myCapsuleCollider = GetComponent<CapsuleCollider2D>();

        float radius = myCapsuleCollider.size.x * Mathf.Abs(transform.localScale.x) / 2f;
        float halfHeight = (myCapsuleCollider.size.y - (myCapsuleCollider.size.x * Mathf.Abs(transform.localScale.x))) / 2f;
        Vector2 bottomCenter = (Vector2)transform.position + myCapsuleCollider.offset + new Vector2(0f, -halfHeight);

        // Check if the left or right edges are touching anything
        Vector2 left = bottomCenter + new Vector2(-radius, 0f);
        Vector2 right = bottomCenter + new Vector2(radius, 0f);
        Collider2D[] collidersLeft = Physics2D.OverlapAreaAll(left, right, LayerMask.GetMask("Ground"));
        Collider2D[] collidersRight = Physics2D.OverlapAreaAll(left + new Vector2(radius * 2f, 0f), right + new Vector2(radius * 2f, 0f), LayerMask.GetMask("Ground"));

        bool isTouchingLeft = false;
        foreach (Collider2D collider in collidersLeft)
        {
            if (collider.gameObject != gameObject)
            {
                isTouchingLeft = true;
                break;
            }
        }

        bool isTouchingRight = false;
        foreach (Collider2D collider in collidersRight)
        {
            if (collider.gameObject != gameObject)
            {
                isTouchingRight = true;
                break;
            }
        }

        return isTouchingLeft || isTouchingRight;
    }

}
