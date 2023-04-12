using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform cam;

	public float mouseSensitivity;
	public float moveSensitivity;

	public float rotationX;
	public float rotationY;

    // Start is called before the first frame update
    void Start()
    {
		cam = GameObject.FindGameObjectWithTag("MainCamera").transform;

		rotationX = cam.rotation.eulerAngles.x;
		rotationY = cam.rotation.eulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			rotationX += -Input.GetAxis("Mouse Y") * mouseSensitivity;
			rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;

			cam.eulerAngles = new Vector3(rotationX, rotationY,	0);
		}
		else { Cursor.lockState = CursorLockMode.None; }

		if(Input.GetKey(KeyCode.W))
		{
			cam.Translate(Vector3.forward * moveSensitivity * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A))
		{
			cam.Translate(Vector3.left * moveSensitivity * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.S))
		{
			cam.Translate(Vector3.back * moveSensitivity * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D))
		{
			cam.Translate(Vector3.right * moveSensitivity * Time.deltaTime);
		}
	}
}
