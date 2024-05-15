using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Classe qui héberge les coordonnées des zonnes à forrester
/// </summary>
public class ZoneManager 
{

    /// <summary>
    /// Méthode qui retourne un object réprensantant la zone d'après l'identifiant de la zone
    /// </summary>
    /// <param name="nom">l'identifiant de la zone</param>
    /// <returns>retourn un object représentant la zone</returns>
    public static ZoneForet GetZone(ZoneType type)
    {
        switch (type)
        {
            case ZoneType.Zone1:
                return new ZoneForet(-62.2f, -43.2f, -62.2f, -30.8f, 20,8,4);
            case ZoneType.Zone2:
                return new ZoneForet(-39.1f, 44.8f, -62.2f, -19.3f, 250,8,14);
            case ZoneType.Zone3:
                return new ZoneForet(-62.2f, -43.2f, -0.3f, 60.5f, 20,18,4);
            case ZoneType.Zone4:
                return new ZoneForet(-43.2f, 61.9f, -5.9f, 61f, 250,10,15);
            case ZoneType.Zone5:
                return new ZoneForet(48.1f, 54.9f, -44.4f, -17.3f, 10,5,4);
            case ZoneType.Zone6:
                return new ZoneForet(60.29f, 62.55f, -44.59f, -14.73f, 10,2,4);
            default:
                return new ZoneForet(-39.1f, 44.8f, -62.2f, -19.3f, 250,22,15);
        }
    }

    public enum ZoneType
    {
        Zone1,
        Zone2,
        Zone3,
        Zone4,
        Zone5,
        Zone6
    }
    
    }


