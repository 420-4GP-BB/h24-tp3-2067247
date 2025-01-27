using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategieGrille : StrategieArbre
{
    //liste des emplacement des arbre
    private List<Vector3> listeEmplacement = new List <Vector3>();
    //espace entre les arbres
    private float espacement= 6.7f;

    public override List<Vector3> ChoisirEmplacement()
    {//definition des zones à eviter
        float x1 = 49.0f, x2 = 62.7f;
        float z1 = -62.8f, z2 =- 44.4f;
        //définition des zones à éviter
        float x3 = -62f, x4 = -39.2f;
        float z3 = -27.8f, z4 = 1f;

        //génération des postion des arbre en evitant les zone à éviter
        for (int x = -9; x < 10; x++)
        {
            for (int z = -9; z < 10; z++)
            {
                Vector3 position = new Vector3(x * espacement, 0, z * espacement);
                if (!(position.x >= x1 && position.x <= x2 && position.z >= z1 && position.z <= z2))
                {
                    if (!(position.x > x3 && position.x < x4 && position.z > z3 && position.z < z4))
                    {
                        listeEmplacement.Add(position);

                    }
                }
            }
        }
        return listeEmplacement;
    }

    

}


