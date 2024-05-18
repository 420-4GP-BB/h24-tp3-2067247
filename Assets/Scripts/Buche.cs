using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// code inspiré du code de l'oeuf 
/// </summary>
public class Buche : MonoBehaviour, IRamassable
{//rammasse l'oeuf et met à jour l'inventaire
    public void Ramasser(Inventaire inventaireJoueur)
    {
        inventaireJoueur.Buches++;
        Destroy(gameObject);
    }
    //retourne l'etat ramasserObjet
    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}
