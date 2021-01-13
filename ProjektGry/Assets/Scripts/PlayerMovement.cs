using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump"))
            animator.SetTrigger("IsHit");

        animator.SetFloat("Speed",movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        if (movement.x < 0)
            sr.flipX = true;
        else if (movement.x > 0)
            sr.flipX = false;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
