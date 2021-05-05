using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public float playerSpeed;
    public float dashSpeed;
    public float dashTime;
    public float distanceBetweenImages;
    public float timeSinceLastStaminaUpdate;
    public float restTime = 1.5f;
    public int stamina = 3;
    public int maxStamina = 3;
    public bool dashing;
    public bool ableToMove;
    public Vector3 spawnpoint;

    //Components
    Rigidbody2D rb;
    Transform playerTransform;
    Animator animator;

    //Vectors
    Vector2 moveInput;
    Vector2 dashDir;
    Vector2 knockbackVelocity;
    Vector3 lastImagePos;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        spawnpoint = playerTransform.position;
        Powerups.player = gameObject;
        ableToMove = true;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing) //Move normally if we aren't dashing
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();

            timeSinceLastStaminaUpdate += Time.deltaTime;
        }

        dashDir = moveInput; //Get the direction we were moving in
        if (Input.GetKeyDown(KeyCode.Space) && !dashing && stamina > 0) //If we haven't dashed and we press the spacebar
        {
            if (dashDir != Vector2.zero)
            {
                dashing = true;
                stamina -= 1;
                timeSinceLastStaminaUpdate = 0f;
                AfterImagePool.Instance.GetFromPool(); //Creates one after image
                lastImagePos = transform.position; //Set's the last image position to the current position

                Invoke(nameof(ResetDash), dashTime); //Set dashing to false after some time
            }
        }

        if (dashing)
        {
            MakeAfterImages();
            //Update sprites
            if (dashDir.x < 0)
            {
                animator.SetFloat("dashDir", -0.5f);
            }
            else if (dashDir.x > 0)
            {
                animator.SetFloat("dashDir", 0.5f);
            }
            else
            {
                if (dashDir.y > 0)
                {
                    animator.SetFloat("dashDir", 0.5f);
                }
                else
                {
                    animator.SetFloat("dashDir", -0.5f);
                }
            }
        }
        else
        {
            animator.SetFloat("dashDir", 0);
        }

        UpdateStamina();
    }

    private void FixedUpdate() //For physics calculations
    {
        if (ableToMove)
        {
            if (!dashing)
            {
                rb.velocity = moveInput * playerSpeed * Powerups.moveSpeedMult; //Moves the player
            }
            else
            {
                rb.velocity = dashDir * dashSpeed;
            }
        }
        if (knockbackVelocity != Vector2.zero)
        {
            rb.velocity += knockbackVelocity;
            knockbackVelocity = Vector2.MoveTowards(knockbackVelocity, Vector2.zero, (6f * Powerups.knockbackResistance) * Time.fixedDeltaTime);
        }
    }

    public void AddKnockback(Vector2 v)
    {
        knockbackVelocity += 5*v;
    }

    //Regain stamina on a set interval
    void UpdateStamina()
    {
        if (timeSinceLastStaminaUpdate > restTime / Powerups.staminaRegenMult)
        {
            stamina++;
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }

            timeSinceLastStaminaUpdate = 0f;
        }
    }

    public void UpdateMaxStamina(int extraStam)
    {
        maxStamina += extraStam;
    }

    void MakeAfterImages()
    {
        if (Vector3.SqrMagnitude(transform.position - lastImagePos) > distanceBetweenImages)
        {
            AfterImagePool.Instance.GetFromPool();
            lastImagePos = transform.position;
        }
    }

    void ResetDash()
    {
        dashing = false;
    }

    public void ReturnToSpawn()
    {
        playerTransform.position = spawnpoint; 
    }

    public void OnDestroy()
    {
        Powerups.player = null;
    }
}
