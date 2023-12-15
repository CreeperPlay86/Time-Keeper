using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Transform cam;

    public CharacterController controller;
    
    public Animator anim;
    
    public float speed = 6f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;

    public float Timer = 5f * 60f;
    public Text timerText;

    public GameObject Blade;

    public float Health = 50f;

    public Text HealthText;

    void Awake()
    {
        // Создание единственного экземпляра PlayerController при старте игры
        instance = this;
    }

    private void Start()
    {
        timerText.text = Timer.ToString();
        HealthText.text = Health.ToString();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        print(Health);
        HealthText.text = Health.ToString();
        if (Health <= 0)
        {
            print("DIE");
            SceneManager.LoadScene(3);
        }

        Timer -= Time.deltaTime;
        timerText.text = "До разрь|ва: " + Mathf.Round(Timer).ToString();

        if(Timer <= 0)
        {
            print("EXIT!");
            SceneManager.LoadScene(3);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);


        }

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("isRuning", true);
        }
        else
        {
            anim.SetBool("isRuning", false);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("JUMP", true);
        }
        else
        {
            anim.SetBool("JUMP", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    void Attack()
    {
        Collider[] coliders = Physics.OverlapBox(transform.position + Vector3.forward, Vector3.one * 2f, Quaternion.identity);


        if (coliders != null)
        {
            foreach (Collider col in coliders)
            {
                if (col.GetComponent<Enemy>() != null)
                {
                    Destroy(col.gameObject);
                    Health += 5;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.CompareTag("Player") && other.CompareTag("Finish") && InteractableItem.totalInteractionCount == InteractableItem.maxInteractions)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            InteractableItem.totalInteractionCount -= InteractableItem.maxInteractions;
        }

        if(this.CompareTag("Player") && other.CompareTag("Enemy") && Health > 0)
        {
            Health -= 2;
        }
    }
}
