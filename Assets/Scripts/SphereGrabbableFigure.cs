using UnityEngine;

public class SphereGrabbableFigure : BaseGrabbableFigure
{
    private float _offcetToAvoidCollisionWithTargetSurface = 0.00001f;

    protected override void MagnetizeToSurface(RaycastHit raycastHit)
    {
        transform.position = raycastHit.point + raycastHit.normal * transform.localScale.y * (0.5f + _offcetToAvoidCollisionWithTargetSurface);

        if (ThereAreNoConflictingColliders)
            UpdateMaterial(placableMaterial);
        else
            UpdateMaterial(unplacableMaterial);
    }
}
