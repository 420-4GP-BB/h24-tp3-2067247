using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPoursuite : EtatRenard
{
    public EtatPoursuite(MouvementRenard renard, GameObject poule) : base(renard, poule)
    {

    }

    public override void Enter()
    {
        Animateur.SetBool("Walk", true);
        AgentMouvement.destination =Poule.transform.position; 
    }

    public override void Handle(float deltaTime)
    {
        bool attaque_requise = false;
        if (!JoueurVisible())
        {
            MouvementRenard mouvement = Renard.GetComponent<MouvementRenard>();
            mouvement.ChangerEtat(mouvement.Patrouille);
        }
        else
        {
            AgentMouvement.destination = Poule.transform.position;
            attaque_requise = Vector3.Distance(Renard.transform.position, Poule.transform.position) <= 3.0f;
        }

        if (attaque_requise)
        {

            GameObject.Destroy(Poule);
        }
    }

    public override void Leave()
    {
        Animateur.SetBool("Walk", false);

    }
}
