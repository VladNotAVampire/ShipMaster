using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCameraScript camScr;

    [SerializeField] private KeyCode interactKey;
    [SerializeField] private Transform cam;
    [SerializeField] private float distance;
    [SerializeField] private float delay;

    private IInteractable obj;
    private string text = string.Empty;
    private bool ready = true;


    void Start()
    {
        GetComponent<PlayerController>().setBlock += b => enabled = b;
    }

    void LateUpdate()
    {
        Debug.DrawLine(cam.position, cam.position + cam.forward * (camScr.camDistance + distance), Color.red);
        if (ready)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, camScr.camDistance + distance))
            {
                if (hit.transform.tag == "Interactable")
                {
                    text = "Interact";
                    if (Input.GetKeyUp(interactKey))
                    {
                        obj = hit.collider.GetComponent<IInteractable>();

                        if (obj != null)
                        {
                            obj.Interact(gameObject);
                            ready = false;
                            Invoke("Delay", delay);
                        }
                    }
                }
                else text = string.Empty;
            }
            else text = string.Empty;
        }
    }

    void OnGUI()
    {
        if (text != string.Empty)
            GUI.Box(new Rect(Screen.width * 0.5f - 60, Screen.height * 0.5f + 40, 120, 60), text);
    }

    private void Delay()
    {
        ready = true;
    }
}