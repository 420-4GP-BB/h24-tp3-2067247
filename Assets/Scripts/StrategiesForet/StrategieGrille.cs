using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strategie : StrategieArbre
{
    public override Vector3[] ChoisirEmplacement()
    {
        Vector3[] tabEmplacement = new Vector3[750];
        for(int i=0; i < tabEmplacement.Length; i++)
        {

 for (int x = 0; x < 30; x++)
        {
            for (int z = 0; z < 32; z++)
            {
                // Calculate the position to place the grid object
                Vector3 position = new Vector3(x * 1f, 0, z * 1f);

              
            }
        }


        }
       

        return tabEmplacement;
    }

    
}
