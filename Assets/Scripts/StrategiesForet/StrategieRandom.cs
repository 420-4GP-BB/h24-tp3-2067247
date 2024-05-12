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
    /// <summary>
    /// Réécriture de la methode abstraire pour populer les arbre par hazard
    /// </summary>
    /// <returns>une liste de Vector 3 contenat la position des arbres</returns>
    public override List<Vector3> ChoisirEmplacement()
    {
        List<Vector3> listeZone1 = PlacerParZone(-62.2f, -43.2f, -62.2f, -30.8f,20,10);
        List<Vector3> listeZone2 = PlacerParZone(-39.1f, 44.8f, -62.2f, -19.3f, 250,10);
        List<Vector3> listeZone3 = PlacerParZone(-62.2f, -43.2f, -0.3f, 60.5f, 20,10);
        List<Vector3> listeZone4 = PlacerParZone(-43.2f, 61.9f, -13.9f, 61f, 250,10);
        List<Vector3> listeZone5 = PlacerParZone(48.1f, 54.9f, -44.4f, -17.3f, 10, 5);
        List<Vector3> listeZone6 = PlacerParZone(60.29f, 62.55f, -44.59f, -14.73f, 10, 5);

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
    /// <param name="essaisMax"> le nombre d'interation max pour eviter une boucle infinie</param>
    /// <returns></returns>
    public List<Vector3> PlacerParZone(float xMin, float xMax, float zMin, float zMax,int nombreArbre,int essaisMax)
    {
        List<Vector3> listeZone = new List<Vector3>(); 
        

        for (int i = 0; i < nombreArbre; i++)
        {
            bool positionTrouve = false;
            Rect rect = new Rect();
            int essais= 0;

            while (!positionTrouve && essais< essaisMax)
            {
                //generation de postions au hazard
                float x = GetRandom(xMin, xMax);
                float z = GetRandom(zMin,zMax);
                float largeur = 4f;  
                float longueur = 4.95f;  

                rect = new Rect(x, z, largeur, longueur);

                positionTrouve = true;

                // Verifier si les arbres se chevauchent
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
                listeZone.Add(new Vector3(rect.xMin, 0, rect.yMin)); // Use xMin and yMin for accurate position
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
