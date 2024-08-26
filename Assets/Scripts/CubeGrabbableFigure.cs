using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class CubeGrabbableFigure : BaseGrabbableFigure
{
    [SerializeField] private GameObject surfaceToPlaceOtherCubesOnTheTop;
    private BoxCollider _boxCollider;
    private Vector3 _offcetToAvoidCollisionWithTargetSurface = Vector3.up * 0.00001f;

    private void Awake()
    {
        _boxCollider = figureCollider as BoxCollider;
    }

    public override void BecameGrabbed()
    {
        base.BecameGrabbed();
        surfaceToPlaceOtherCubesOnTheTop.SetActive(false);
    }

    public override void BecamePlaced()
    {
        base.BecamePlaced();
        surfaceToPlaceOtherCubesOnTheTop.SetActive(true);
    }

    protected override void MagnetizeToSurface(RaycastHit raycastHit)
    {
        transform.position = raycastHit.point + raycastHit.normal * _boxCollider.size.y * 0.5f + _offcetToAvoidCollisionWithTargetSurface;

        if(ThereAreNoConflictingColliders)
            UpdateMaterial(placableMaterial);
        else
            UpdateMaterial(unplacableMaterial);
    }
}
