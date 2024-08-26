using UnityEngine;

public class Character : MonoBehaviour, IMovable, IAbleToGrab
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform head;
    private float _minHeadRotationAngle = -60.0f;
    private float _maxHeadRotationAngle = 60.0f;
    private float _headRotationAngle = 0;
    private IAbleToBeGrabbed _grabbedFigure;

    public bool CheckIsGrabbingNow => _grabbedFigure != null;

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
        Vector3 point = new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f);
        Ray ray = Camera.main.ScreenPointToRay(point);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var grabbableFigure = hit.transform.GetComponent<IAbleToBeGrabbed>();
            if (grabbableFigure != null)
            {
                grabbableFigure.BecameGrabbed();
                _grabbedFigure = grabbableFigure;
            }
        }
    }

    public void Place()
    {
        _grabbedFigure.BecamePlaced();
        _grabbedFigure = null;
    }
}
