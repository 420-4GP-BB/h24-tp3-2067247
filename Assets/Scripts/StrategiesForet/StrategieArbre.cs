using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategieArbre
{
    public string TagEviter = "SansArbre";
    public abstract Vector3 [] ChoisirEmplacement();
}
