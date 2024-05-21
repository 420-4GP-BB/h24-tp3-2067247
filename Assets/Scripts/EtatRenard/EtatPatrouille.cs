using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPatrouille : EtatRenard
{
    //tableau des points de patrouille
    private GameObject[] _points;
    //index de patrouille
    private int _indexPatrouille;

    public EtatPatrouille(MouvementRenard renard, GameObject[] points) : base(renard)
    {
        _points = points;
        // Initialisation de l'index de patrouille à une valeur aléatoire
        _indexPatrouille = Random.Range(0, _points.Length);
   
    }

    public override void Enter()
    {
        //verifier qu'il ya bien des points de patrouille dans le tab
        if (_points.Length > 0)
        {//il se dirige vers le point à l'indice aléatoire
            AgentMouvement.destination = _points[_indexPatrouille].transform.position;
        }
    }

    public override void Handle(float deltaTime)
    {
        GameObject pouleEnVue = PouleVisible();
        // Vérifie si une poule est visible, si oui le renard entre dans l'état poursuite
        if (pouleEnVue != null)
        {
            Renard.ChangerEtat(Renard.Poursuite);
            return;
        }
        //si non, il continue sa patrouille
        if (!AgentMouvement.pathPending && AgentMouvement.remainingDistance <= 0.1f)
        {
            // génération de l'index de patrouille à une valeur aléatoire
            _indexPatrouille = Random.Range(0, _points.Length);
            AgentMouvement.destination = _points[_indexPatrouille].transform.position;
        }
    }

    public override void Leave()
    {
    }
}
