using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPoursuite : EtatRenard
{
    public EtatPoursuite(MouvementRenard renard) : base(renard)
    {
    }

    public override void Enter()
    {

      
    }

    public override void Handle(float deltaTime)
    {//verifie si la poule est nulle
        if (PouleAchasser == null)
        {//verife si il voit une poule 
            PouleAchasser = PouleVisible();
            if (PouleAchasser == null)
            {//si elle est encore nulle il change d'etat pour aller en patrouille
                Renard.ChangerEtat(Renard.Patrouille);
                return;
            }
        }
        //sinon il poursuit la poule
        AgentMouvement.destination = PouleAchasser.transform.position;
        //si il est a 1 unité ou moins,le bool vaut vrai
        bool attaque_requise = Vector3.Distance(Renard.transform.position, PouleAchasser.transform.position) <= 1.0f;
        //si le bool vaut vrai, il mange la poule (on détruit le gameObject)
        if (attaque_requise)
        {
            GameObject.Destroy(PouleAchasser);
            PouleAchasser = null;
            //il change d'état pour revenir en patrouille
            Renard.ChangerEtat(Renard.Patrouille);
        }
    }

    public override void Leave()
    {


    }
}
