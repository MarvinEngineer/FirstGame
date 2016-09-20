using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    public float rotateSpeed = 100f;
    public float jumpSpeed = 20f;
    public float gravity = 20f;
    public float currentJumpSpeed = 0f;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float _horizontalRotate = Input.GetAxis("Mouse X");
        float _verticalRotate = Input.GetAxis("Mouse Y");

        
        //Горизонтальный поворот
        transform.Rotate(0, _horizontalRotate * rotateSpeed * Time.deltaTime, 0, Space.World );
        //Вертикальный поворот
        transform.Rotate(new Vector3(-_verticalRotate,0,0), rotateSpeed * Time.deltaTime, Space.Self);

        //Движение
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        else
        {
            currentJumpSpeed = moveDirection.y;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            moveDirection.y = currentJumpSpeed - gravity * Time.deltaTime;
        }
        controller.Move(moveDirection * Time.deltaTime);

    }
}
