using UnityEngine;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D body;
    public bool grounded;
    public float horizontalInput;
    private Animator anim;
    public GameObject win;
    public bool isKnockedBack = false;

    private float attackCooldown = 0.3f; // seconds between attacks
    private float lastAttackTime;

    private bool isAttacking;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isKnockedBack)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }

        horizontalInput = Input.GetAxis("Horizontal");

        // Movement only if not in attack (optional)
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Jumping
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded)
        {
            jump();
        }

        // Sprite facing direction
        if (horizontalInput > 0.0f)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
        else if (horizontalInput < 0.0f)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }

        // Attempt Attack
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            TryAttack();
        }

        // Animator states
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    // Called by animation event
    public void StartAttack()
    {
        isAttacking = true;
    }

    // Called by animation event
    public void EndAttack()
    {
        isAttacking = false;
    }

    private void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown && canAttack())
        {
            anim.SetTrigger("attack");
            lastAttackTime = Time.time;
        }
    }

    private void jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x,8);
        anim.SetTrigger("J");
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("arrow"))
        {
            Destroy(other.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("arrow"))
        {
            grounded = true;
        }
        if (collision.gameObject.CompareTag("temple"))
        {
            winf();
        }
    }

    // Can only attack when standing still on ground
    public bool canAttack()
    {
        return grounded && Mathf.Abs(horizontalInput) == 0;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void winf()
    {
        win.SetActive(true);
        speed = 0f;
        anim.SetBool("run", false);
        body.linearVelocity = Vector2.zero;
        enabled = false;
    }
}
