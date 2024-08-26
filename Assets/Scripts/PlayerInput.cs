using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private float sensitivityHorizontal;
    [SerializeField] private float sensitivityVertical;

    void Update()
    {
        character.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        character.Rotate(new Vector2(Input.GetAxis("Mouse X") * sensitivityHorizontal, Input.GetAxis("Mouse Y") * sensitivityVertical));
    }
}
