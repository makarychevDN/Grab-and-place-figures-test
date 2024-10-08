using UnityEngine;

public interface IAbleToGrab
{
    public bool IsGrabbingNow { get; }
    public void Grab();
    public void Place();
    public void RotateGrabbedObject(float inputValue);
}
