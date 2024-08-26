using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float sensitivityHorizontal;
    [SerializeField] private float sensitivityVertical;
    private IMovable _movable;
    private IAbleToGrab _ableToGrab;

    private void Awake()
    {
        _movable = GetComponent<IMovable>();
        _ableToGrab = GetComponent<IAbleToGrab>();
    }

    void Update()
    {
        if (_movable != null)
        {
            _movable.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            _movable.Rotate(new Vector2(Input.GetAxis("Mouse X") * sensitivityHorizontal, Input.GetAxis("Mouse Y") * sensitivityVertical));
        }

        if (_ableToGrab != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_ableToGrab.IsGrabbingNow)
                    _ableToGrab.Place();
                else
                    _ableToGrab.Grab();
            }

            _ableToGrab.RotateGrabbedObject(Input.GetAxisRaw("Mouse ScrollWheel"));
        }
    }
}
