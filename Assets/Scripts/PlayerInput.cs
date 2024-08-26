using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private IMovable movable;
    [SerializeField] private float sensitivityHorizontal;
    [SerializeField] private float sensitivityVertical;

    private void Awake()
    {
        movable = GetComponent<IMovable>();
    }

    void Update()
    {
        if (movable == null)
            return;

        movable.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        movable.Rotate(new Vector2(Input.GetAxis("Mouse X") * sensitivityHorizontal, Input.GetAxis("Mouse Y") * sensitivityVertical));
    }
}
