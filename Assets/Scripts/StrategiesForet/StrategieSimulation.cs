using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StrategieSimulation : StrategieArbre
{
    //Les zones qui divisent le terrain
    private ZoneForet zone1 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone1);
    private ZoneForet zone2 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone2);
    private ZoneForet zone3 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone3);
    private ZoneForet zone4 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone4);
    private ZoneForet zone5 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone5);
    private ZoneForet zone6 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone6);
    //tableau 1d qui contient les zones
    private ZoneForet[] listeZones;

 /// <summary>
 /// Méthode principale de la stratégie
 /// </summary>
 /// <returns> Elle retourne une liste contenant toutes les positions des arbres </returns>
    public override List<Vector3> ChoisirEmplacement()
    {
        listeZones=new []{ zone1,zone2,zone3,zone4,zone5,zone6};
        List<Vector3> liste = new List<Vector3>();
        foreach(ZoneForet zone in listeZones)
        {
            Vector3[,] grillePos = SeparerZoneEnGrille(zone);
            bool[,] grille = DeclancherGenerations(zone);
            liste.AddRange(ConvertirGrilles(grillePos, grille));
        }
        return liste;

    }
    /// <summary>
    /// Méthode pour remplir initialement le tableau de vérité, chaque case a 70% de chance d'etre true
    /// </summary>
    /// <param name="zone"> prend la zone en parametre</param>
    /// <returns>retourne un tableau 2d de bool</returns>
    public bool[,] RemplirIntialement(ZoneForet zone)
    {
        bool[,] grille = new bool[zone.ligne, zone.colonne];
        //Boucle sur chaque element de grille
        //boucle sur chaque ligne
        for (int i = 0; i < grille.GetLength(0); i++)
        {
            //boucle sur chque colonne
            for (int j = 0; j < grille.GetLength(1); j++)
            {
                //true si le random est plus petit ou egal a 7
                grille[i, j] = Random.Range(1, 11) <= 7;
            }
        }
        return grille;
    }
    /// <summary>
    /// Méthode pour déclancher les 10 génération de la simulation de foret, J'ai utilisé chat gpt pour m'aider à circuler entre les voisins
    /// </summary>
    /// <param name="zone">une zone de terrain</param>
    /// <returns>un tableau de verité  </returns>
    public bool[,] DeclancherGenerations(ZoneForet zone)
    {
        bool[,] grille = RemplirIntialement(zone);
        int li = grille.GetLength(0);
        int col = grille.GetLength(1);
        bool[,] nvGrille = new bool[li, col];
        //boucle pendant 10 iteration pour simuler 10 générations
        for(int indice =0; indice < 10; indice++)
        {
            //boucle pour naviguer les ligne 
            for (int i = 0; i < li; i++)
            {//boucle pour naviguer les colonnes
                for (int j = 0; j < col; j++)
                {//initialisation du nombre de voisins à 0
                    int nombreVoisin = 0;
                    //Boucler dans les voisins
                    //les lignes des voisins
                    for (int vi = -1; vi <= 1; vi++)
                    {
                        //les colonnes des voisins
                        for (int vj = -1; vj <= 1; vj++)
                        {
                            //ne prend pas en compte la valeur de la case elle même
                            if (vi == 0 && vj == 0)
                            {
                                continue;
                            }
                            //calcul des lignes et les colonnes 
                            int ni = i + vi;
                            int nj = j + vj;
                            // verifier que les voisin sont dans la grille
                            if (ni >= 0 && ni < li && nj >= 0 && nj < col)
                            {
                                if (grille[ni, nj])
                                    nombreVoisin += 1;
                            }

                        }
                    }
                    int[] voisinsValides = { 3, 4, 6, 7, 8 };
                    //verifier le nombre des voisins
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
       //initialiser la grille a la nouvelle grille, pour que la prochaine iteraction prenne en compte la nouvelle grille
            grille = nvGrille;
        }

       
        return grille;
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

                // décalage aléatoire à x et z pour éviter de paraitre comme une grille
                x += Random.Range(-1.25f, 1.25f);
                z += Random.Range(-1.25f, 1.25f);

                // Assigner la position calculée à la grille avec décalage
                grille[i, j] = new Vector3(x ,0, z );
            }
        }

        return grille;
    }
    /// <summary>
    /// Méthode qui converti le tableau de verité en les positions corrspondantes
    /// </summary>
    /// <param name="grillePos">prend la grille de toutes les position</param>
    /// <param name="grilleVerite">grille de verité</param>
    /// <returns>une liste de poitions d'arbres</returns>
    public List<Vector3> ConvertirGrilles(Vector3[,] grillePos, bool[,] grilleVerite)
    {
        List<Vector3> liste = new List<Vector3>();
        for (int i = 0; i < grilleVerite.GetLength(0); i++)
        {
            for (int j = 0; j < grilleVerite.GetLength(1); j++)
            {
                //les deux tableaux devraient être des memes dimension,donc la case vaut vrai, la position correspondante est ajouté à la liste.
                if (grilleVerite[i, j])
                {
                    liste.Add(grillePos[i, j]);
                }
            }
        }
        return liste;
    }
}


