using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EtatAction : EtatJoueur
{
    public override bool EstActif => true;
    public override bool DansDialogue => false;
    public override float EnergieDepensee => ConstantesJeu.COUT_MARCHER;


    private GameObject _destination;
    private NavMeshAgent _navMeshAgent;

    private Vector3 pointDestination;

    public EtatAction(ComportementJoueur sujet, GameObject destination) : base(sujet)
    {
        _destination = destination;
        _navMeshAgent = Sujet.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        Animateur.SetBool("Walking", true);
        ControleurMouvement.enabled = false;
        _navMeshAgent.enabled = true;

        Vector3 direction = _destination.transform.position - Sujet.transform.position;
        Quaternion rotationCible = Quaternion.LookRotation(direction);

        //Enclencher la coroutine pour effectuer la rotation graduelle sur 0.25 secondes
        Sujet.StartCoroutine(RotationGraduelle(rotationCible, 0.25f));
    }
    //cette méthode est inspirée de l'exerice 8 du module 3
    /// <summary>
    /// Méthode qui permet de faire une rotation graduelle du jour pour le diriger vers l'objet actionnable,
    /// avant qu'il commence de marcher
    /// </summary>
    /// <param name="rotationCible">la direction  de l'objet actionnable</param>
    /// <param name="dureeRotation">la durée de la rotation 0.25 secondes</param>
    /// <returns>la coroutine qui permet une roation graduelle</returns>
    private IEnumerator RotationGraduelle(Quaternion rotationCible, float dureeRotation)
    {//on prend garde en mémoire la roation initiale pour la réutiliser dans le slerp
        Quaternion rotationInitiale = Sujet.transform.rotation;
        //variable pour stocker le temps
        float tempsEcoule = 0f;
        //boucle pour la roation graduelle
        while (tempsEcoule < dureeRotation)
        {
            tempsEcoule += Time.deltaTime;
            float pourcentage = Mathf.Clamp01(tempsEcoule / dureeRotation);
            Sujet.transform.rotation = Quaternion.Slerp(rotationInitiale, rotationCible, pourcentage);
            yield return null;
        }

        // Une fois la rotation terminée, on définit la destination du NavMesh agent
        Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
        pointDestination = pointProche - (rotationCible * Vector3.forward) * 0.3f;
        _navMeshAgent.SetDestination(pointDestination);
    }

    // On doit se rendre au point pour faire l'action
    public override void Handle()
    {
        float distance = Vector3.Distance(pointDestination, Sujet.transform.position);
        if (!_navMeshAgent.pathPending && distance <= 0.3f)
        {
            _navMeshAgent.enabled = false;
            pointDestination.y = Sujet.transform.position.y;
            Sujet.transform.position = pointDestination;

            var actionnable = _destination.GetComponent<IActionnable>();
            if (actionnable != null)
            {
                Sujet.ChangerEtat(actionnable.EtatAUtiliser(Sujet));
            }
        }

    }


    public override void Exit()
    {
        ControleurMouvement.enabled = true;
        _navMeshAgent.enabled = false;
        Animateur.SetBool("Walking", false);
    }
}