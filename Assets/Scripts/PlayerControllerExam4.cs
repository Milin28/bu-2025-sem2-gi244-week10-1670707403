using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam4 : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private bool isOnGround = true;
    private int jumpCount = 0; 

    private Animator playerAnim;
    private AudioSource playerAudio;

    private InputAction dashAction;
    public bool isDashing = false;
    public int hp = 3; 


    public bool gameOver = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    
    void Start()
    {
        Physics.gravity *= gravityModifier;
        dashAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashAction.IsPressed() && !gameOver)
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }
        if (jumpAction.triggered && jumpCount < 2 && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++; 

            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0; 

            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            
            hp--;
            Debug.Log("HP: " + hp);
            explosionParticle.Stop();
            explosionParticle.Clear();
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSfx);

            Destroy(collision.gameObject);

            if (hp <= 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
            }


        }
    }

}