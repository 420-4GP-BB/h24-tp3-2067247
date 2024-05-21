using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MouvementRenard : MonoBehaviour
{//Les points de patrouille de renard
    private GameObject[] _pointsPatrouille;
    //l'etat courant du renard
    private EtatRenard _etat;
    //declaration etat Patrouille 
    public EtatPatrouille Patrouille
    {
        private set;
        get;
    }
    // declaration etatPoursuite
    public EtatPoursuite Poursuite
    {
        private set;
        get;
    }

    void Start()
    {
        //lecture des points de patrouille
        _pointsPatrouille = GameObject.FindGameObjectsWithTag("PointRenard");
        //creation des etats du renard
        Patrouille = new EtatPatrouille(this, _pointsPatrouille);
        Poursuite = new EtatPoursuite(this);
        //affection de l'etat de patrouille en premier
        _etat = Patrouille;
        _etat.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        _etat.Handle(Time.deltaTime);
    }
    //methode pour naviguer entre les etats
    public void ChangerEtat(EtatRenard nouvelEtat)
    {
        _etat.Leave();
        _etat = nouvelEtat;
        _etat.Enter();
    }

}

