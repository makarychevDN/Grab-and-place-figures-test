using UnityEngine;

public class GrabbableFigure : MonoBehaviour, IAbleToBeGrabbed
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material placableMaterial;
    [SerializeField] private Material unplacableMaterial;
    [SerializeField] private Renderer figureRenderer;
    [SerializeField] private Collider figureCollider;

    public void BecameGrabbed()
    {
        figureCollider.isTrigger = true;
        figureRenderer.material = placableMaterial;
    }

    public void BecamePlaced()
    {
        figureCollider.isTrigger = false;
        figureRenderer.material = defaultMaterial;
    }

    public void Drag(Vector3 expectedDragPosition)
    {
        transform.position = expectedDragPosition;
    }
}
