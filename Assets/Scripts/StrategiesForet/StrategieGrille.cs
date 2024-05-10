using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategieGrille : StrategieArbre
{
    public string TagAEviter = "SansArbre";
    private int MaxEmplacements = 750;  // Maximum number of positions to store
    private float espacement= 6.7f;

    public override Vector3[] ChoisirEmplacement()
    {
        Vector3[] tabEmplacement = new Vector3[MaxEmplacements];
        float x1 = 49.0f, x2 = 62.7f;
        float z1 = -62.8f, z2 =- 44.4f;

        float x3 = -62f, x4 = -39.2f;
        float z3 = -27.8f, z4 = 1f;

        int count = 0;  
        for (int x = -9; x < 10; x++)
        {
            for (int z = -9; z < 10; z++)
            {
                Vector3 position = new Vector3(x * espacement, 0, z * espacement);
                if (!(position.x >= x1 && position.x <= x2 && position.z >= z1 && position.z <= z2))
                {
                    if ( !( position.x > x3 && position.x < x4 && position.z > z3 && position.z < z4))
                    {
                        tabEmplacement[count] = position;
                        count++;

                        if (count >= MaxEmplacements)
                            return tabEmplacement;
                    }
                    
                }
            }
        }


        if (count < MaxEmplacements)
        {
            Vector3[] tempArray = new Vector3[count];
            Array.Copy(tabEmplacement, tempArray, count);
            return tempArray;
        }

        return tabEmplacement;
    }

    

}


