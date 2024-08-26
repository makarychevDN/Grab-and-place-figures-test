using UnityEngine;

public class Character : MonoBehaviour, IMovable, IAbleToGrab
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform head;
    private float _minHeadRotationAngle = -60.0f;
    private float _maxHeadRotationAngle = 60.0f;
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
        _headRotationAngle -= input.y;
        _headRotationAngle = Mathf.Clamp(_headRotationAngle, _minHeadRotationAngle, _maxHeadRotationAngle);
        head.transform.localEulerAngles = new Vector3(_headRotationAngle, 0, 0);
    }

    public void Grab()
    {
        print("Grab!!!");
    }

    public void Place()
    {
        throw new System.NotImplementedException();
    }
}
