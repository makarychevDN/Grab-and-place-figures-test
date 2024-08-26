using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Character character;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    void Update()
    {
        character.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        character.Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
    }
}
