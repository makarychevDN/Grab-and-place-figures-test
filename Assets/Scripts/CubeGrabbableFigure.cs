using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class CubeGrabbableFigure : DefaultGrabbableFigure
{
    [SerializeField] private GameObject surfaceToPlaceOtherCubesOnTheTop;

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
}
