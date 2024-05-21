using System;
using Unity.VisualScripting;
using UnityEngine;

public class ChouPret : MonoBehaviour, IRamassable
{
    private ChouMesh3D _chouMesh3D;
    private void Start()
    {
        _chouMesh3D = GetComponent<ChouMesh3D>();
        _chouMesh3D.ObjetPret.SetActive(true);
    }

    public void Ramasser(Inventaire inventaireJoueur)
    {
        // Cueillir
        _chouMesh3D.CacherObjets();
        inventaireJoueur.Choux++;
        //on rajoute le component <EmplacementChouVide> car il est detruit quand on plante une graine.
        gameObject.AddComponent<EmplacementChouVide>();
        Destroy(this);
    }

    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}