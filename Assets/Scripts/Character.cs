using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform head;
    private float minimumVert = -45.0f;
    private float maximumVert = 45.0f;
    private float _rotationX = 0;

    public void Move(Vector2 input)
    {
        Vector3 movement = new Vector3(input.x, 0, input.y).normalized * speed;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    public void Rotate(Vector2 input)
    {
        transform.Rotate(0, input.x, 0);
        _rotationX -= Input.GetAxis("Mouse Y");
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
        float delta = Input.GetAxis("Mouse X");
        float rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        head.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
    }
}
