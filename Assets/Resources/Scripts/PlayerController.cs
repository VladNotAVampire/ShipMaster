using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action<bool> setBlock;

    public float speed;
    public float jumpForce;
    public float mouseSensivity;
    public bool isGrounded = true;
    [Range(0, 1)] public float fallModificatorAccelerationTime;

    public PlayerCameraScript camScript;

    [SerializeField] KeyCode goForward;
    [SerializeField] KeyCode goBack;
    [SerializeField] KeyCode goLeft;
    [SerializeField] KeyCode goRight;
    [SerializeField] KeyCode run;
    [SerializeField] KeyCode jump;

    [SerializeField] Transform cam;  //cam holder
    public Transform _cam; //real cam

    Rigidbody rBody;

    float currentSpeed;
    float fallModificator;
    const float fallModificatorMinimum = 0.1f;

    bool isGoingForward;
    bool isGoingBack;
    bool isGoingLeft;
    bool isGoingRight;
    bool isRuning;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        _cam = cam.GetChild(0);
        camScript = GetComponent<PlayerCameraScript>();

        setBlock += b => enabled = b;
    }

    void Update()
    {
        isRuning = Input.GetKey(run);
        isGoingForward = Input.GetKey(goForward);
        isGoingBack = Input.GetKey(goBack);
        isGoingLeft = Input.GetKey(goLeft);
        isGoingRight = Input.GetKey(goRight);

        if (isGoingForward && isGoingBack)
        {
            isGoingForward = false;
            isGoingBack = false;
        }

        if (isGoingLeft && isGoingRight)
        {
            isGoingLeft = false;
            isGoingRight = false;
        }

        //currentSpeed = isGrounded ? speed : speed * 0.1f;

        if (Input.GetKeyDown(jump)) Jump();                                                     //jump

        if (!isGrounded)
        {
            fallModificator = Mathf.SmoothStep(fallModificator, fallModificatorMinimum, fallModificatorAccelerationTime);
        }
        else
        {
            fallModificator = Mathf.SmoothStep(fallModificator, 1, fallModificatorAccelerationTime);
        }
    }

    void FixedUpdate()
    {                                                                                             //moving player
        if (isGoingForward)
            transform.position += transform.forward * (isRuning ? currentSpeed * 1.5f * Time.fixedDeltaTime : currentSpeed * Time.deltaTime);

        if (isGoingBack) transform.position -= transform.forward * currentSpeed * Time.fixedDeltaTime;
        if (isGoingLeft) transform.position -= transform.right * currentSpeed * Time.fixedDeltaTime;
        if (isGoingRight) transform.position += transform.right * currentSpeed * Time.fixedDeltaTime;

        currentSpeed = isGrounded ? speed : speed * fallModificator;
    }

    void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rBody.AddForce(Vector3.up * jumpForce * 50);
            AddForceInMovingDirection();
            fallModificator = fallModificatorMinimum;
        }
    }

    public void AddForceInMovingDirection()
    {
        float x = isGoingRight ? currentSpeed * rBody.mass :
                                                        (isGoingLeft ? -currentSpeed * rBody.mass : 0);
        float z = isGoingForward ?
                                isRuning ? currentSpeed * rBody.mass * 1.5f : currentSpeed * rBody.mass :
                                                                                                     isGoingBack ? -currentSpeed * rBody.mass : 0;

        z *= fallModificator;
        x *= fallModificator;

        Vector3 jumpDir = transform.right * x + transform.forward * z;
        rBody.AddForce(jumpDir * 50);
    }

    public void SetBlock(bool block) { SetBlock(block, camScript.enabled); }

    public void SetBlock(bool block, bool cam)
    {
        setBlock(block);
        camScript.enabled = cam;
    }

    public void GetKeys(out KeyCode _goForward,
                        out KeyCode _goBack,
                        out KeyCode _goLeft,
                        out KeyCode _goRight,
                        out KeyCode _run,
                        out KeyCode _jump)
    {
        _goForward = goForward;
        _goBack = goBack;
        _goLeft = goLeft;
        _goRight = goRight;
        _run = run;
        _jump = jump;
    }

}
