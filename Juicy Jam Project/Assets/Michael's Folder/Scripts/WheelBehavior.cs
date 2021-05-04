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
    public int currentTier;

    //Components
    private Canvas canvas;
    private Rigidbody2D wheelRigidbody;
    private RectTransform wheelTransform;

    //Scripts
    public SFXManager sfx;

    //GameObjects
    private GameObject wheel;
    private GameObject wheelParent;
    [SerializeField] private GameObject[] buttonList;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        wheel = GameObject.Find("Wheel");
        wheelRigidbody = wheel.GetComponent<Rigidbody2D>();
        wheelTransform = wheel.GetComponent<RectTransform>();
        wheelParent = wheel.transform.parent.gameObject;
        sfx = FindObjectOfType<SFXManager>();

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
            float rotation = wheelTransform.eulerAngles.z;
            currentTier = (int)(((rotation) / (360f / 3)) + 1);
            buttonList[currentTier - 1].SetActive(true);
            wheelParent.SetActive(false);

            isSpun = false;
        }
    }

    private void WheelUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            if (!canvas.enabled)
            {
                canvas.enabled = true;
            }
        }

        Invoke(nameof(WheelUpdate), 0.1f);
    }

    public void Spin()
    {
        isSpun = true;
        allowedToReadResult = false;
        Invoke(nameof(AllowedToReadResult), 0.1f);
        wheelTransform.Rotate(0, 0, Random.Range(1, 360f), Space.Self);
        wheelRigidbody.AddTorque(spinSpeed);
        sfx.PlaySound(2); //Plays wheel spin sfx
    }

    public void AllowedToReadResult()
    {
        allowedToReadResult = true;
    }

    public void ClickPowerUp(int buttonNumber)
    {
        Powerups.UpdatePowerUpValues(currentTier, buttonNumber);
        wheelParent.SetActive(true);
        buttonList[currentTier - 1].SetActive(false);
        canvas.enabled = false;
        sfx.PlaySound(1); //Plays powerup sfx
    }

}
