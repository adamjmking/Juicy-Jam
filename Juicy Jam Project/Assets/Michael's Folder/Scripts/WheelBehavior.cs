using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehavior : MonoBehaviour
{
    //Variables
    private bool isSpun;
    public float spinSpeed;
    public float lowerSpinMultiplicator;
    public float upperSpinMultiplicator;
    public bool Visible { get; set; }

    //Components
    private Canvas canvas;
    private Rigidbody2D wheelRigidbody;
    private RectTransform wheelTransform;

    //GameObjects
    private GameObject wheel;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        wheel = GameObject.Find("Wheel");
        wheelRigidbody = wheel.GetComponent<Rigidbody2D>();
        wheelTransform = wheel.GetComponent<RectTransform>();

        canvas.enabled = false;
        allowedToReadResult = false;
        isSpun = false;
        WheelUpdate();
    }

    //First few frames must be skipped since the wheel hasn't began to spin
    private bool allowedToReadResult;

    private void FixedUpdate()
    {
        if (isSpun && wheelRigidbody.angularVelocity > 5)
        {
            wheelRigidbody.AddTorque(-10);
        }
        if(isSpun && wheelRigidbody.angularVelocity < -5)
        {
            wheelRigidbody.AddTorque(8);
        }
        if (isSpun && allowedToReadResult && wheelRigidbody.angularVelocity == 0)
        {
            int[] options = { 1, 2, 3, 4 };
            WheelResultCalculator<int> wheelResultCalculator = new WheelResultCalculator<int>();

            Debug.Log(wheelResultCalculator.getResult(options, wheelTransform.rotation.eulerAngles.z- 90));

            isSpun = false;
        }
    }

    private void WheelUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            canvas.enabled = true;
        }
        else
        {
            Invoke(nameof(WheelUpdate), 0.1f);
        }
        
    }

    public void Spin()
    {
        isSpun = true;
        allowedToReadResult = false;
        Invoke(nameof(AllowedToReadResult), 0.1f);
        wheelTransform.Rotate(0, 0, Random.Range(1, 360), Space.Self);
        wheelRigidbody.AddTorque(spinSpeed);
    }

    public void AllowedToReadResult()
    {
        allowedToReadResult = true;
    }
}
