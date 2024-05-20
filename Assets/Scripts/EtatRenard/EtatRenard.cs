using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//code inspiré de l'exercie 4 du module 5
public abstract class EtatRenard 
{
    public MouvementRenard Renard
    {
        set;
        get;
    }
    public GameObject Poule
    {
        set;
        get;
    }

    public NavMeshAgent AgentMouvement
    {
        set;
        get;
    }

    public Animator Animateur
    {
        set;
        get;
    }

    public EtatRenard(MouvementRenard renard, GameObject poule)
    {
        Renard = renard;
        Poule = poule;
        AgentMouvement = renard.GetComponent<NavMeshAgent>();
        Animateur = renard.GetComponent<Animator>();
    }

    protected bool JoueurVisible()
    {
        bool visible = false;
        RaycastHit hit;


        // PATCH: On place les y au m�me niveau pour �viter les probl�me. 
        Vector3 positionJoueur = new Vector3(Poule.transform.position.x, 0.5f, Poule.transform.position.z);
        Vector3 positionRenard = new Vector3(Renard.transform.position.x, 0.5f, Renard.transform.position.z);
        Vector3 directionJoueur = positionJoueur - positionRenard;

        // Regarde s'il y a un obstacle entre le squelette et le joueur
        if (Physics.Raycast(positionRenard, directionJoueur, out hit))
        {
            if (hit.transform == Poule.transform)
            {
                // Il n'y a pas d'obstacle, on v�rifie l'angle
                float angle = Vector3.Angle(Renard.transform.forward, directionJoueur);
                visible = angle <= 40.0f;
            }
        }

        return visible;
    }

    public abstract void Enter();
    public abstract void Handle(float deltaTime);
    public abstract void Leave();
}
