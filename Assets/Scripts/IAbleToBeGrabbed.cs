using UnityEngine;

public interface IAbleToBeGrabbed
{
    public void BecameGrabbed();
    public void BecamePlaced();
    public void Drag(RaycastHit raycastHit, Transform head, float distanceFromHeadWhenUnplacable, float maxDistancFromHeadeWhenPlaceable);
    public void RotateAroundYAxis(float rotationInput);
    public bool CanBePlaced();
}
