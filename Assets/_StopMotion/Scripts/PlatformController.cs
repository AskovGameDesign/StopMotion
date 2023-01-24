using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour 
{
    [SerializeField] private float movementForce = 3f;
    [SerializeField] private float jumpForce = 4f;

    [SerializeField] private float fallMultiplier = 3f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Groundcheck Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 groundCheckBoxSize = Vector3.one;

    [SerializeField] private int numberOfPlayerLives = 3;
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private Animator animator;

    #region private variables
    Rigidbody rb;
    AudioSource audioSource;
    
    bool isGrounded;

    Vector3 movementVector;
    float horizontalMovement;

    bool facingRight = false;
    #endregion


    // Use this for initialization
    void Awake () 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}

    private void Start()
    {
        GameManager.Instance.Greetings(this.gameObject);
    }

    // Update is called once per frame
    void Update () 
    {
        isGrounded = Physics.CheckBox(groundCheck.position, groundCheckBoxSize, groundCheck.rotation, 1 << LayerMask.NameToLayer("Ground"));
        
        movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // Flip //
        if (movementVector.x < 0f && facingRight == false) //horizontalMovement
            FlipPlayer();
        else if (movementVector.x > 0f && facingRight == true) //horizontalMovement
            FlipPlayer();

        if (animator)
            animator.SetFloat("Speed", horizontalMovement); // movementVector.normalized.magnitude

       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector3.up * jumpForce;

            if (jumpSound && audioSource)
                audioSource.PlayOneShot(jumpSound);
            
        }

        // The player is falling //
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
        // Check for quick release jump //
        else if (rb.velocity.y > 0f && !Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }
    }


    void FixedUpdate()
    {
        //rb.velocity = new Vector3(movementVector.x * movementForce, rb.velocity.y, movementVector.z * movementForce);
        rb.velocity = new Vector3(horizontalMovement * movementForce, rb.velocity.y, 0f);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;

        transform.localScale = localScale;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Gizmos.color = Color.green : Gizmos.color = Color.red;

        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize);

        if (!rb)
            return;
                
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, new Vector3(rb.velocity.x, rb.velocity.y, 0f));
        
    }
}
