using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//code inspiré de l'exercie 4 du module 5
public abstract class EtatRenard 
{
    //script du renard
    public MouvementRenard Renard
    {
        set;
        get;
    }
  //variable pour stocker la poule a chasser si elle est visible
    public GameObject PouleAchasser
    {
        set;
        get;
    }
    //agent nav mesh
    public NavMeshAgent AgentMouvement
    {
        set;
        get;
    }
    //l'animateur n'est pas utilisé car le renard ne change pas d'animation, il ne fait que courir
  //constructeur de la classe de base
    public EtatRenard(MouvementRenard renard)
    {
        Renard = renard;
        AgentMouvement = renard.GetComponent<NavMeshAgent>();
       
    }
    /// <summary>
    /// Méthode qui retourne la poule que le renard voit parmis les poules de le scene
    /// </summary>
    /// <returns>La poule que le renard a vu</returns>
    protected GameObject PouleVisible()
    {
        RaycastHit hit;
        //lecture des poules a chque fois que cette methode est appellée au cas ou le fermier achete une nouvelle, ou un oeuf eclot ou que le renard en a déjà mangé une
        GameObject[] Poules = GameObject.FindGameObjectsWithTag("Poule");
        foreach (GameObject poule in Poules)
        {
            // PATCH: On place les y au même niveau pour éviter les problème. 
            Vector3 positionPoule = new Vector3(poule.transform.position.x, 0.5f, poule.transform.position.z);
            Vector3 positionRenard = new Vector3(Renard.transform.position.x, 0.5f, Renard.transform.position.z);
            Vector3 directionPoule = positionPoule - positionRenard;
            //verifier si la poule est à 5 unités ou moins
            if (Vector3.Distance(positionRenard, positionPoule) <= 5.0f)
            {
                // Regarde s'il y a un obstacle entre le renard et la poule
                if (Physics.Raycast(positionRenard, directionPoule, out hit))
                {
                    if (hit.transform == poule.transform)
                    {
                        // Il n'y a pas d'obstacle, on vérifie l'angle
                        float angle = Vector3.Angle(Renard.transform.forward, directionPoule);
                        if (angle <= 40.0f)
                        {
                            PouleAchasser = poule;
                            // On retourne la poule à chasser
                            return poule;
                        }
                    }
                }
            }
        }
            // sinon on retourne rien
            return null;
        
    }

    public abstract void Enter();
    public abstract void Handle(float deltaTime);
    public abstract void Leave();
}
