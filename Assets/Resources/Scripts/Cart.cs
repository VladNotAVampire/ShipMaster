using UnityEngine;
using System;

public class Cart : MonoBehaviour, IInteractable, IMount
{
    private KeyCode goForward;
    private KeyCode goBack;
    private KeyCode goLeft;
    private KeyCode goRight;
    private KeyCode run;
    private KeyCode jump;

    private bool isGoingForward;
    private bool isGoingBack;
    private bool isGoingLeft;
    private bool isGoingRight;
    private bool isRuning;

    public float speed ;
    public float handelability;

    [Range(0, 1)] public float acceleracionTime;
    [Range(0, 1)] public float rotateAccelerationTime;

    public Vector3 playerPose;
    public Vector3 landingPose;
    public Vector3 LandingPose {
        get { return transform.position + transform.forward * landingPose.z + transform.right * landingPose.x + transform.up * landingPose.y; } }

    GameObject _player;

    float currentSpeed;
    float currentRotateAngle;

    private ColliderEnablingController colliderController;

    public float Speed
    {
        get 
        {
            return speed;    
        }
        set 
        {
            speed = value;    
        }
    }

    public void Interact(GameObject player)
    {
        GetOn(player);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * currentSpeed * Time.fixedDeltaTime;
        Rotate();
    }

    private void Update()
    {
        isRuning       = Input.GetKey(run);
        isGoingForward = Input.GetKey(goForward);
        isGoingBack    = Input.GetKey(goBack);
        isGoingLeft    = Input.GetKey(goLeft);
        isGoingRight   = Input.GetKey(goRight);

        if (isGoingForward && isGoingBack)
        {
            isGoingForward = false;
            isGoingBack    = false;
        }

        if (isGoingLeft && isGoingRight)
        {
            isGoingLeft  = false;
            isGoingRight = false;
        }

        if (isGoingForward)
        {
            currentSpeed = isRuning ? Mathf.SmoothStep(currentSpeed, speed * 1.5f, acceleracionTime) : 
                                                                                                    Mathf.SmoothStep(currentSpeed, speed, acceleracionTime) ;
        }
        else if (isGoingBack)
        {
            currentSpeed = Mathf.SmoothStep(currentSpeed, -speed * 0.08f, acceleracionTime);
        }
        else
        {
            currentSpeed = Mathf.SmoothStep(currentSpeed, 0, acceleracionTime);
        }

        if (isGoingLeft)
        {
            currentRotateAngle = Mathf.SmoothStep(currentRotateAngle, -handelability, rotateAccelerationTime);
        }
        else if(isGoingRight)
        {
            currentRotateAngle = Mathf.SmoothStep(currentRotateAngle, handelability, rotateAccelerationTime);
        }
        else
        {
            currentRotateAngle = Mathf.SmoothStep(currentRotateAngle, 0, rotateAccelerationTime);
        }
        
        if (Input.GetKeyUp(KeyCode.E)) GetOff();
    }

    private void Rotate()
    {
        transform.Rotate(0, currentRotateAngle * Time.fixedDeltaTime * Mathf.Clamp(currentSpeed * 2 / speed, -1, 1), 0);
   }

    private void OnGUI()
    {
         GUI.Box(new Rect(Screen.width * 0.5f - 80, Screen.height * 0.5f + 40, 160, 60), "Press <E> to get off");
    }

    public void GetOn(GameObject player)
    {
        _player = player;
        player.transform.SetParent(transform);
        player.transform.localPosition = Vector3.zero;
        player.GetComponent<Rigidbody>().isKinematic = true;
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.GetKeys(out goForward, out goBack, out goLeft, out goRight, out run, out jump);
        controller.SetBlock(false);
        player.transform.localRotation = new Quaternion(0, 0, 0, 0);
        colliderController = new ColliderEnablingController(player, true);
        controller.camScript.camController = new CartCameraController(controller.camScript);
        enabled = true;
    }

    public void GetOff()
    {
        _player.transform.SetParent(null);
        _player.transform.position = LandingPose;
        _player.GetComponent<PlayerController>().SetBlock(true);
        _player.GetComponent<Rigidbody>().isKinematic = false;
        var camScr = _player.GetComponent<PlayerCameraScript>();
        camScr.camController = new StandartCameraController(camScr, 0, Quaternion.LookRotation(LandingPose - transform.position).eulerAngles.y);
        enabled = false;

        if (colliderController != null)
        {
            colliderController.EnableColliders();
            colliderController = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(LandingPose, 1);
    }
}

