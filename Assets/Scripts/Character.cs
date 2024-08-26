using UnityEngine;

public class Character : MonoBehaviour, IMovable, IAbleToGrab
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform head;
    private float _minHeadRotationAngle = -60.0f;
    private float _maxHeadRotationAngle = 60.0f;
    private float _headRotationAngle = 0;
    private float _distanceOfUnplacableDraggedObject = 3;
    private float _maximumDistanceOfDraggedPlacableObject = 5;
    private IAbleToBeGrabbed _grabbedFigure;

    public bool IsGrabbingNow => _grabbedFigure != null;

    public void Move(Vector2 input)
    {
        Vector3 movement = new Vector3(input.x, 0, input.y).normalized * speed;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);

        if (IsGrabbingNow)
            UpdatePositionOfGrabbedObject(_grabbedFigure);
    }

    public void Rotate(Vector2 input)
    {
        transform.Rotate(0, input.x, 0);
        _headRotationAngle -= input.y;
        _headRotationAngle = Mathf.Clamp(_headRotationAngle, _minHeadRotationAngle, _maxHeadRotationAngle);
        head.transform.localEulerAngles = new Vector3(_headRotationAngle, 0, 0);

        if (IsGrabbingNow)
            UpdatePositionOfGrabbedObject(_grabbedFigure);
    }

    private void UpdatePositionOfGrabbedObject(IAbleToBeGrabbed ableToBeGrabbed)
    {
        Physics.Raycast(RaycastFromMiddleOfScreen(), out RaycastHit hit);
        ableToBeGrabbed.Drag(hit, head, _distanceOfUnplacableDraggedObject, _maximumDistanceOfDraggedPlacableObject);
    }

    public void Grab()
    {
        if (Physics.Raycast(RaycastFromMiddleOfScreen(), out RaycastHit hit))
        {
            var grabbableFigure = hit.transform.GetComponentInParent<IAbleToBeGrabbed>();
            if (grabbableFigure != null)
            {
                grabbableFigure.BecameGrabbed();
                _grabbedFigure = grabbableFigure;
            }
        }
    }

    private Ray RaycastFromMiddleOfScreen()
    {
        Vector3 point = new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f);
        Ray ray = Camera.main.ScreenPointToRay(point);
        return ray;
    }

    public void Place()
    {
        if (!_grabbedFigure.CanBePlaced())
            return;

        _grabbedFigure.BecamePlaced();
        _grabbedFigure = null;
    }
}
