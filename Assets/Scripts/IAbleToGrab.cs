using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbleToGrab
{
    public void Grab();
    public void Place();
    public bool CheckIsGrabbingNow { get; }
}
