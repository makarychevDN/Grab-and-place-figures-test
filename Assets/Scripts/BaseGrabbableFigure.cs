using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGrabbableFigure : MonoBehaviour, IAbleToBeGrabbed
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material placableMaterial;
    [SerializeField] private Material unplacableMaterial;
    [SerializeField] private Renderer figureRenderer;
    [SerializeField] private Collider figureCollider;
    [SerializeField] private List<string> availableSurfacesLayersToMagnetize;
    private HashSet<Collider> _conflictingColliders = new();
    private bool _magnetizedMod;

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
        if (CheckSurfaceIsAvailavleToMagnetize(raycastHit, head, maxDistancFromHeadeWhenPlaceable))
        {
            _magnetizedMod = false;
            transform.position = raycastHit.point;
            UpdateMaterial(unplacableMaterial);
        }
        else
        {
            _magnetizedMod = true;
            MagnetizeToSurface();
        }
    }

    public bool CanBePlaced()
    {
        return _magnetizedMod && ThereAreNoConflictingColliders;
    }

    private bool CheckSurfaceIsAvailavleToMagnetize(RaycastHit raycastHit, Transform head, float maximumDistance)
    {
        return raycastHit.collider != null 
            && Vector3.Distance(head.transform.position, raycastHit.point) < maximumDistance
            && availableSurfacesLayersToMagnetize.Contains(raycastHit.transform.gameObject.layer.ToString());
    }

    private void UpdateMaterial(Material material)
    {
        if (figureRenderer.material == material)
            return;

        figureRenderer.material = material;
    }

    protected bool ThereAreNoConflictingColliders => _conflictingColliders.Count == 0;

    protected abstract void MagnetizeToSurface();

    private void OnTriggerEnter(Collider other) => _conflictingColliders.Add(other);

    private void OnTriggerExit(Collider other) => _conflictingColliders.Remove(other);
}
