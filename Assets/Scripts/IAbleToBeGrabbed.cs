using UnityEngine;

public interface IAbleToBeGrabbed
{
    public void BecameGrabbed();
    public void BecamePlaced();
    public void Drag(Vector3 expectedDragPosition);
}
