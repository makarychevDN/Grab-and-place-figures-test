using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform head;
    private float _minHeadRotationAngle = -45.0f;
    private float _maxHeadRotationAngle = 45.0f;
    private float _headRotationAngle = 0;

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
        _headRotationAngle -= Input.GetAxis("Mouse Y");
        _headRotationAngle = Mathf.Clamp(_headRotationAngle, _minHeadRotationAngle, _maxHeadRotationAngle);
        float delta = Input.GetAxis("Mouse X");
        float rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        head.transform.localEulerAngles = new Vector3(_headRotationAngle, 0, 0);
    }
}
