using UnityEngine;

public class GroundDetector : MonoBehaviour 
{
    [SerializeField]PlayerController controller;

    [SerializeField] float dragWhenGrounded;

    Rigidbody rBody;

    private void Start()
    {
        rBody = controller.GetComponent<Rigidbody>();
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.transform.root != controller.transform.root)
        {
            controller.isGrounded = true;
            rBody.drag = dragWhenGrounded;
        }      
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.root != controller.transform.root) 
        {
            controller.isGrounded = false;
            //controller.AddForceInMovingDirectoty();
            rBody.drag = 0;
        }
    }
}
