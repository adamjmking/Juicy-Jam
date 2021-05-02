using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public float playerSpeed;
    public float dashSpeed;
    public float dashTime;
    [SerializeField] private bool dashing;

    //Components
    Rigidbody2D rb;

    //Vectors
    Vector2 mInput, dashInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing)
        {
            mInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            mInput.Normalize();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = mInput * playerSpeed;
    }

    private void ResetDash()
    {
        dashing = false;
    }
}
