using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPatrouille : EtatRenard
{
    private Transform[] _points;
    private int _indexPatrouille;
    public EtatPatrouille(MouvementRenard renard, GameObject poule, Transform[] points) : base(renard, poule)
    {
        _points = points;
        _indexPatrouille = 0;
    }

    public override void Enter()
    {
        Animateur.SetBool("Walk", true);
    }

    public override void Handle(float deltaTime)
    {
        if (!AgentMouvement.pathPending)
        {
            if (AgentMouvement.remainingDistance <= 0.1f)
            {
                AgentMouvement.destination = _points[_indexPatrouille].position;
                _indexPatrouille = (_indexPatrouille + 1) % _points.Length;
            }
        }

        if (JoueurVisible())
        {
            MouvementRenard mouvement = Renard.GetComponent<MouvementRenard>();
            mouvement.ChangerEtat(mouvement.Poursuite);
        }
    }

    public override void Leave()
    {
        throw new System.NotImplementedException();
    }
}
