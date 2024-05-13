using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategieSimulation : StrategieArbre
{
    private ZoneForet zone1 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone1);
    private ZoneForet zone2 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone2);
    private ZoneForet zone3 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone3);
    private ZoneForet zone4 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone4);
    private ZoneForet zone5 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone5);
    private ZoneForet zone6 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone6);
    private System.Random _random = new System.Random();
    public override List<Vector3> ChoisirEmplacement()
    {
        PlacerParZone(zone2);
        return null;
    }

    public List<Vector3> PlacerParZone(ZoneForet zone)
    {


        bool[,] grille = new bool[zone.ligne,zone.colonne];
        //Boucle sur chaque element de grille
        for (int i = 0; i < grille.GetLength(0); i++) 
        {
            for (int j = 0; j < grille.GetLength(1); j++) 
            {
               
                grille[i, j] = _random.Next(1,11) <= 7;
            }
        }
        Imprimergrille(grille, zone.ligne, zone.colonne);
        return null;
    }

    void Imprimergrille(bool[,] grid, int rows, int cols)
    {
        string row = "";
        for (int i = 0; i < rows; i++)
        {
            
            for (int j = 0; j < cols; j++)
            {
                row += grid[i, j] ? "X " : "  ";
            }
            row += "\n";
            
        }
        Debug.Log(row);
    }

}
