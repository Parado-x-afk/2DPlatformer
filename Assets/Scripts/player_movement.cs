using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float jumpForce = 8f;

    private Rigidbody2D body;
    private Animator anim;

    public bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded)
        {
            jump();
        }

        if (horizontalInput > 0.0f)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
        else if (horizontalInput < 0.0f)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        anim.SetTrigger("J");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
