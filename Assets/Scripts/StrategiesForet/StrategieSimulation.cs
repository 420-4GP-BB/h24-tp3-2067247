using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StrategieSimulation : StrategieArbre
{
    private ZoneForet zone1 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone1);
    private ZoneForet zone2 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone2);
    private ZoneForet zone3 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone3);
    private ZoneForet zone4 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone4);
    private ZoneForet zone5 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone5);
    private ZoneForet zone6 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone6);
    private ZoneForet[] listeZones;
    private System.Random _random = new System.Random();

    public override List<Vector3> ChoisirEmplacement()
    {
        listeZones=new []{ zone1,zone2,zone3,zone4,zone5,zone6};
        List<Vector3> liste = new List<Vector3>();
        foreach(ZoneForet zone in listeZones)
        {
            Vector3[,] grillePos = SeparerZoneEnGrille(zone);
            bool[,] grille = DeclancherGenerations(zone);
            liste.AddRange(ComparerGrilles(grillePos, grille));
        }
        return liste;

    }

    public bool[,] RemplirIntialement(ZoneForet zone)
    {
        bool[,] grille = new bool[zone.ligne, zone.colonne];
        //Boucle sur chaque element de grille
        for (int i = 0; i < grille.GetLength(0); i++)
        {
            for (int j = 0; j < grille.GetLength(1); j++)
            {

                grille[i, j] = _random.Next(1, 11) <= 7;
            }
        }
        return grille;
    }
    public bool[,] DeclancherGenerations(ZoneForet zone)
    {
        bool[,] grille = RemplirIntialement(zone);
        int li = grille.GetLength(0);
        int col = grille.GetLength(1);
        bool[,] nvGrille = new bool[li, col];

        for(int indice =0; indice < 10; indice++)
        {

            for (int i = 0; i < li; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int nombreVoisin = 0;
                    for (int vi = -1; vi <= 1; vi++)
                    {
                        for (int vj = -1; vj <= 1; vj++)
                        {
                            if (vi == 0 && vj == 0)
                            {
                                continue;
                            }
                            int ni = i + vi;
                            int nj = j + vj;

                            if (ni >= 0 && ni < li && nj >= 0 && nj < col)
                            {
                                if (grille[ni, nj])
                                    nombreVoisin += 1;
                            }

                        }
                    }
                    int[] voisinsValides = { 3, 4, 6, 7, 8 };
                    if (voisinsValides.Contains(nombreVoisin))
                    {
                        nvGrille[i, j] = true;
                    }
                    else
                    {
                        nvGrille[i, j] = false;
                    }
                }
            }
           // Imprimergrille(nvGrille);
            grille = nvGrille;
        }

       
        return nvGrille;
    }

   

    public static Vector3[,] SeparerZoneEnGrille(ZoneForet zone)
    {
        // Calculer la taille de chaque cellule sur x et z
        float largeurCellule = (zone.xMax - zone.xMin) / zone.colonne;
        float profondeurCellule = (zone.zMax - zone.zMin) / zone.ligne;

        // Créer un tableau 2D pour stocker les positions des cellules
        Vector3[,] grille = new Vector3[zone.ligne, zone.colonne];

        for (int i = 0; i < zone.ligne; i++)
        {
            for (int j = 0; j < zone.colonne; j++)
            {
                // Calculer la position à l'intersection des axes
                float x = zone.xMin + j * largeurCellule;
                float z = zone.zMin + i * profondeurCellule;

                // décalage aléatoire à x et z pour de paraitre comme une grille
                x += Random.Range(-1.25f, 1.25f);
                z += Random.Range(-1.25f, 1.25f);

                // Assigner la position calculée à la grille avec décalage
                grille[i, j] = new Vector3(x ,0, z );
            }
        }

        return grille;
    }
    public List<Vector3> ComparerGrilles(Vector3[,] grillePos, bool[,] grilleVerite)
    {
        List<Vector3> liste = new List<Vector3>();
        for (int i = 0; i < grilleVerite.GetLength(0); i++)
        {
            for (int j = 0; j < grilleVerite.GetLength(1); j++)
            {

                if (grilleVerite[i, j])
                {
                    liste.Add(grillePos[i, j]);
                }
            }
        }
        return liste;
    }
}


