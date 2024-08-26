using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class CubeGrabbableFigure : BaseGrabbableFigure
{
    protected override void MagnetizeToSurface(RaycastHit raycastHit)
    {
        transform.position = raycastHit.point + raycastHit.normal * 3;
    }
}
