using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private Camera cam ;
    [SerializeField] private float zoomValue;

    private Vector3 newPosition;

    private Collider2D[] colliders;
    public LayerMask minion;
    public TextMeshProUGUI UITextRef;
    private GameObject targetRef=null;
    public string value;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {

        HandleMovementInput();
        if (targetRef != null)
        {

            targetRef.SendMessage("DeliverHunger",1);
            UITextRef.text = value;
        }

    }
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.up * cameraSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.up * -cameraSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -cameraSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * cameraSpeed);
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        

        if(Input.GetAxis("Mouse ScrollWheel")>0f && cam.orthographicSize>10f)
        {

            cam.orthographicSize += -zoomValue;

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.orthographicSize += zoomValue;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Vector3 camPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            colliders = Physics2D.OverlapPointAll(camPos);
            if (colliders.Length != 0 && colliders[0].gameObject.layer == LayerMask.NameToLayer("minion"))
            {
                Debug.Log("opa minion");
                targetRef = colliders[0].gameObject;
            }
            
        }

    }


     
}
