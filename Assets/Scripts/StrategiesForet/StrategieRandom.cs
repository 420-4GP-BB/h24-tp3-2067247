using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StrategieRandom : StrategieArbre
{
    // Liste pour héberger les emplacements finaux des arbres.
    private List<Vector3> listeEmplacement = new List<Vector3>();
    // Liste pour héberger les rectangles à comparer
    private List<Rect> listeRect = new List<Rect>();
    // Random pour générer les positions au hazard
    private System.Random _random = new System.Random();

    ZoneForet zone1 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone1);
    ZoneForet zone2 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone2);
    ZoneForet zone3 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone3);
    ZoneForet zone4 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone4);
    ZoneForet zone5 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone5);
    ZoneForet zone6 = ZoneManager.GetZone(ZoneManager.ZoneType.Zone6);
    /// <summary>
    /// Réécriture de la methode abstraire pour populer les arbre par hazard
    /// </summary>
    /// <returns>une liste de Vector 3 contenant la position des arbres</returns>
    public override List<Vector3> ChoisirEmplacement()
    {
       
        List<Vector3> listeZone1 = PlacerParZone(zone1, 10);
        List<Vector3> listeZone2 = PlacerParZone(zone2, 10);
        List<Vector3> listeZone3 = PlacerParZone(zone3, 10);
        List<Vector3> listeZone4 = PlacerParZone(zone4, 10);
        List<Vector3> listeZone5 = PlacerParZone(zone5, 5);
        List<Vector3> listeZone6 = PlacerParZone(zone6, 5);

        listeEmplacement.AddRange(listeZone1);
        listeEmplacement.AddRange(listeZone2);
        listeEmplacement.AddRange(listeZone3);
        listeEmplacement.AddRange(listeZone4);
        listeEmplacement.AddRange(listeZone5);
        listeEmplacement.AddRange(listeZone6);
        

        return listeEmplacement;

    }
    /// <summary>
    /// Methode pour générer les emplacements des arbres de chauqe zone au hazard sans que ceux-ci ne soient collés
    /// </summary>
    /// <param name="xMin">l'axe x minimum délimitant la zone</param>
    /// <param name="xMax">l'axe x maximum délimitant la zone</param>
    /// <param name="zMin">l'axe z minimum délimitant la zone</param>
    /// <param name="zMax">l'axe z maximum délimitant la zone</param>
    /// <param name="nombreArbre">le nombre d'arbre à générer dans la zone</param>
    /// <param name="essaisMax"> le nombre d'itération max pour eviter une boucle infinie</param>
    /// <returns></returns>
    private List<Vector3> PlacerParZone(ZoneForet zone ,int essaisMax)
    {
        List<Vector3> listeZone = new List<Vector3>(); 
        

        for (int i = 0; i < zone.nombreArbre; i++)
        {
            bool positionTrouve = false;
            Rect rect = new Rect();
            int essais= 0;

            while (!positionTrouve && essais< essaisMax)
            {
                //generation de postions au hazard
                float x = GetRandom(zone.xMin, zone.xMax);
                float z = GetRandom(zone.zMin,zone.zMax);
                float largeur = 4f;  
                float longueur = 4.95f;  
                // creation d'un rect utilisant les valeur générées
                rect = new Rect(x, z, largeur, longueur);
                positionTrouve = true;

                // Verifier si le nouvel arbre chevauche un arbre existant
                foreach (Rect existingRect in listeRect)
                {
                    if (rect.Overlaps(existingRect))
                    {
                        positionTrouve = false;
                        break;
                    }
                }
                essais++;
            }

            if (positionTrouve)
            {
                listeRect.Add(rect);
                listeZone.Add(new Vector3(rect.xMin, 0, rect.yMin));
            }
        }

        return listeZone;
    }


  
    /// <summary>
    /// J'ai utilisé chat gpt pour m'aider à trouver un random pour un float
    /// </summary>
    /// <param name="min"> le float minimum</param>
    /// <param name="max"> le float maximum</param>
    /// <returns> un float ransom entre le min et le max</returns>
    private float GetRandom(float min, float max)
    {
        return (float)(_random.NextDouble() * (max - min) + min);
    }

}  
