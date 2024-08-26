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
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void BecamePlaced()
    {
        figureCollider.isTrigger = false;
        figureRenderer.material = defaultMaterial;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void Drag(RaycastHit raycastHit, Transform head, float distanceFromHeadWhenUnplacable, float maxDistancFromHeadeWhenPlaceable)
    {
        if (CheckSurfaceIsAvailavle(raycastHit, head, maxDistancFromHeadeWhenPlaceable))
        {
            transform.position = raycastHit.point;
        }
        else
        {
            transform.position = head.position + head.forward * distanceFromHeadWhenUnplacable;
        }
    }

    private bool CheckSurfaceIsAvailavle(RaycastHit raycastHit, Transform head, float maximumDistance)
    {
        return raycastHit.collider != null && Vector3.Distance(head.transform.position, raycastHit.point) < maximumDistance;
    }

    private void MagnetizeToSurface()
    {

    }
}
