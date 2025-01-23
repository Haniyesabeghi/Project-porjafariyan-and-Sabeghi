using UnityEngine;
using UnityEngine.UI;

public class BallSinglePlayer : MonoBehaviour
{
    public Transform ball;
    public GameObject[] allPins;
    public GameObject powerBallPanel;
    public Slider ballPowerSlider;
    public Pins[] pins;

    private Rigidbody ballRigidBody;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public int round;
    private bool ballIsThrown = false;
    private bool isDragging = false;
    private float dragStartTime;


    public float movementSpeed = 20f; // سرعت حرکت توپ
    public float minX = 18f; // حداقل مقدار حرکت توپ در محور X
    public float maxX = 24.5f;


    public bool moveLeft = false;
    public bool moveRight = false;

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody>();
        ballRigidBody.useGravity = false;
        initialPosition = ball.position;
        initialRotation = ball.rotation;
        round = 0;
        ResetSlider();
    }

    void Update()
    {
        HandleMouseInput();
        HandleKeyboardMovement();
    }



    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // شروع کشیدن
        {
            OnMouseStart(Input.mousePosition);
        }
        if (Input.GetMouseButton(0)) // حرکت کشیدن
        {
            OnMouseMove(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0)) // پایان کشیدن
        {
            OnMouseEnd();
        }
    }

    // کنترل ورودی کیبورد برای حرکت افقی
    private void HandleKeyboardMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // حرکت به چپ
        {
            ballRigidBody.AddForce(Vector3.left * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // حرکت به راست
        {
            ballRigidBody.AddForce(Vector3.right * movementSpeed);
        }

        // محدود کردن حرکت توپ در محدوده افقی
        if (ballRigidBody.position.x < minX)
            ballRigidBody.position = new Vector3(minX, ballRigidBody.position.y, ballRigidBody.position.z);

        if (ballRigidBody.position.x > maxX)
            ballRigidBody.position = new Vector3(maxX, ballRigidBody.position.y, ballRigidBody.position.z);
    }

    // شروع کشیدن با ماوس
    void OnMouseStart(Vector3 mousePosition)
    {
        if (!ballIsThrown)
        {
            isDragging = true;
            dragStartTime = Time.time;
            powerBallPanel.SetActive(true); // نمایش پنل قدرت
        }
    }

    // حرکت کشیدن با ماوس
    void OnMouseMove(Vector3 mousePosition)
    {
        if (isDragging)
        {
            float dragDuration = (Time.time - dragStartTime) * 40000;
            ballPowerSlider.value = Mathf.Clamp01(dragDuration / 40000); // مقداردهی اسلایدر
        }
    }

    // پایان کشیدن با ماوس
    void OnMouseEnd()
    {
        if (!ballIsThrown && isDragging)
        {
            isDragging = false;
            float dragDuration = (Time.time - dragStartTime) * 40000;

            float force = Mathf.Clamp(dragDuration, 10000, 25000);
            ballRigidBody.AddForce(transform.forward * force, ForceMode.Force);
            ballRigidBody.useGravity = true;
            ballIsThrown = true;

            ballPowerSlider.value = Mathf.Clamp01(dragDuration / 40000);
            powerBallPanel.SetActive(false); // پنهان کردن پنل قدرت
        }
    }
    public int GetRound()
    {
        return round;
    }

    public void IncreaseRound()
    {
        round=+1;
    }

    public void ResetBall()
    {
        ballIsThrown = false;
        ball.position = initialPosition;
        ball.rotation = initialRotation;
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
        ballRigidBody.useGravity = false;
        powerBallPanel.SetActive(true);
        ResetSlider();
        UpdatePinsState();
    }

    public void ResetRound()
    {
        ResetBall();

       
            UpdatePinsState();
            //ResetPins();
        
        IncreaseRound();
    }

    private void UpdatePinsState()
    {
        int fallenCount = 0; // شمارنده پین‌هایی که افتاده‌اند

        for (int i = 0; i < allPins.Length; i++)
        {
            if (pins[i].pin_has_fallen())
            {
                allPins[i].SetActive(false); // غیرفعال کردن پین

                // افزایش شمارنده و چاپ پیام
                fallenCount++;
                Debug.Log($"Pin {i + 1} has fallen. Total fallen: {fallenCount}");
            }
        }

        // چاپ تعداد کل پین‌هایی که افتاده‌اند
        Debug.Log($"Total pins fallen: {fallenCount}");
    }

    //private void ResetPins()
    //{
    //    foreach (var pin in pins)
    //    {
    //        pin.pins_Reset();
    //    }
    //}

    private void ResetSlider()
    {
        ballPowerSlider.value = 0;
    }
}
