using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    //[SerializeField] RectTransform _forward, _right, _up;

    [SerializeField] private float _movingVelocity;
    [SerializeField] private float _rotationVelocity;

    private Camera _camera;

    private float xRot = 0f, yRot = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRot = transform.rotation.eulerAngles.y;
        _camera = Camera.main;
        Cursor.visible = false;
    }

    private void Update()
    {
        xRot += Input.GetAxis("Mouse X") * _rotationVelocity * Time.deltaTime;
        yRot += -Input.GetAxis("Mouse Y") * _rotationVelocity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f);

        if (Input.GetKey(KeyCode.W))
            transform.Translate(transform.forward * _movingVelocity * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(-transform.forward * _movingVelocity * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(transform .right * _movingVelocity * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(-transform.right * _movingVelocity * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Space))
            transform.Translate(transform.up * _movingVelocity * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.LeftShift))
            transform.Translate(-transform.up * _movingVelocity * Time.deltaTime, Space.World);


        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
