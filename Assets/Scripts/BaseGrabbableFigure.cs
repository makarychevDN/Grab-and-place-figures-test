using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGrabbableFigure : MonoBehaviour, IAbleToBeGrabbed
{
    [Header("Links")]
    [SerializeField] protected Renderer figureRenderer;
    [SerializeField] protected Collider figureCollider;

    [Header("Materials")]
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material placableMaterial;
    [SerializeField] protected Material unplacableMaterial;

    [Header("Layers")]
    [SerializeField] protected List<string> availableSurfacesLayersToMagnetize;

    protected HashSet<Collider> _conflictingColliders = new();
    protected bool _magnetizedMod;

    public virtual void BecameGrabbed()
    {
        figureCollider.isTrigger = true;
        figureRenderer.material = placableMaterial;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public virtual void BecamePlaced()
    {
        figureCollider.isTrigger = false;
        figureRenderer.material = defaultMaterial;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void Drag(RaycastHit raycastHit, Transform head, float distanceFromHeadWhenUnplacable, float maxDistancFromHeadeWhenPlaceable)
    {
        if (CheckSurfaceIsAvailavleToMagnetize(raycastHit, head, maxDistancFromHeadeWhenPlaceable))
        {
            _magnetizedMod = true;
            MagnetizeToSurface(raycastHit);
        }
        else
        {
            _magnetizedMod = false;
            transform.position = head.position + head.forward * distanceFromHeadWhenUnplacable;
            UpdateMaterial(unplacableMaterial);
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
            && availableSurfacesLayersToMagnetize.Contains(LayerMask.LayerToName(raycastHit.transform.gameObject.layer));
    }

    protected void UpdateMaterial(Material material)
    {
        if (figureRenderer.material == material)
            return;

        figureRenderer.material = material;
    }

    protected bool ThereAreNoConflictingColliders => _conflictingColliders.Count == 0;

    protected abstract void MagnetizeToSurface(RaycastHit raycastHit);

    private void OnTriggerEnter(Collider other) => _conflictingColliders.Add(other);

    private void OnTriggerExit(Collider other) => _conflictingColliders.Remove(other);
}
