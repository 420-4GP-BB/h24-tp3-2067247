using System;
using System.Collections.Generic;
/// <summary>
/// Classe qui représente les zone à populer
/// </summary>
public class ZoneForet
{
    //l'axe x minimum délimitant la zone
    public float xMin
	{
		private set;
		get;
	}
    //l'axe x maximum délimitant la zone
    public float xMax
    {
        private set;
        get;
    }
   // l'axe z minimum délimitant la zone
    public float zMin
    {
        private set;
        get;
    }
   // l'axe z maximum délimitant la zone
    public float zMax
    {
        private set;
        get;
    }
    //le nombre d'arbre à générer dans la zone
    public int nombreArbre
    {
        private set;
        get;
    }

    //les lignes de la grille pour la simulation
    public int ligne
    {
        private set;
        get;
    }
    //les colonnes de la grille pour la simulation
    public int colonne
    {
        private set;
        get;
    }

   

    /// <summary>
    /// Contructeur de la classe, il remplit la liste automatiquement.
    /// </summary>
    /// <param> Les paramètres sont les mêmes attributs ci-haut</param>
   
    public ZoneForet(float XMin, float XMax, float ZMin, float ZMax, int nbArbre,int Ligne, int Colonne)
    {
        xMin = XMin;
        xMax = XMax;
        zMin = ZMin;
        zMax = ZMax;
        nombreArbre = nbArbre;
        ligne = Ligne;
        colonne = Colonne;
    }



}

