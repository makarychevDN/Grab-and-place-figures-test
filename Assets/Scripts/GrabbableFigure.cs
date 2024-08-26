using UnityEngine;

public class GrabbableFigure : MonoBehaviour, IAbleToBeGrabbed
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material placableMaterial;
    [SerializeField] private Material unplacableMaterial;
    [SerializeField] private Renderer figureRenderer;

    public void BecameGrabbed()
    {
        figureRenderer.material = placableMaterial;
    }

    public void BecamePlaced()
    {
        throw new System.NotImplementedException();
    }
}
