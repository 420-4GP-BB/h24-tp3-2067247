using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementPoulet : MonoBehaviour
{
    //cette zone s'étend sur toute la ferme
     private UnityEngine.GameObject _zoneRelachement;
     private UnityEngine.GameObject joueur;
     private bool _suivreJoueur = true;
    //distance à respecter par les poules
    private float distanceDuJoueur = 2f;
    private Soleil _soleil;
    private GameObject pointSpecial;

    private NavMeshAgent _agent;
    private Animator _animator;
    //J'ai du transformer le array en list car on a besoin qu'ele soit dynamique pour ajouter et retirer le point spécial
    private List<GameObject> _pointsDeDeplacement = new List<GameObject>();

    void Start()
    {
        _zoneRelachement = UnityEngine.GameObject.Find("ZoneRelachePoulet");
        pointSpecial= UnityEngine.GameObject.Find("PointRenardSpecial");
        _soleil = UnityEngine.GameObject.Find("Directional Light").GetComponent<Soleil>();
        joueur = UnityEngine.GameObject.FindGameObjectWithTag("Joueur");
        //Les poulet commence par suivre le joueur
        _suivreJoueur = true;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _pointsDeDeplacement.AddRange(GameObject.FindGameObjectsWithTag("PointsPoulet"));
        _animator.SetBool("Walk", true);
        
    }


    void ChoisirDestinationAleatoire()
    {
        GameObject point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Count)];
        _agent.SetDestination(point.transform.position);
    }

    void Update()
    {
       
        if (_suivreJoueur)
         {
            // calcul de la postion à la laquel le poulet doit se rendre en gardant une distance de sécurité avec le joueur
            Vector3 directionAvecJoueur = Quaternion.AngleAxis(180f, Vector3.up) * joueur.transform.forward;
            Vector3 positionCible = joueur.transform.position - directionAvecJoueur.normalized * distanceDuJoueur;

            // Calcul de la distance entre le joueur et le poulet
            float distanceActuelle = Vector3.Distance(transform.position, joueur.transform.position);

            // Le poulet continue de suivre le joueur si la distance de securité n'est pas atteinte
            if (distanceActuelle > distanceDuJoueur)
            {
                _agent.SetDestination(positionCible);
            }
            else
            {
                //le poulet arrete de suivre le joueur quand il est trop proche
                _agent.SetDestination(transform.position);
            }

        }
        else
        {//quand le poulet arrive sur la ferme , il appelle la methode pour generer les points
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                ChoisirDestinationAleatoire();
            }
        }


       
    }
    //J'ai mis l'ajout/ retrait du point dans un fixed update car on a pas besoin de verifier aussi fréquemment que dans une update normal
    private void FixedUpdate()
    {//le point special est ajouté seulement si il est le temps du renard
        if (_soleil.EstNuitRenard)
        {// il est ajouté seulement si il n'existe pas deja
            if (!_pointsDeDeplacement.Contains(pointSpecial))
            {
                _pointsDeDeplacement.Add(pointSpecial);
      
            }
                
        }
        else
        {//il est retiré durant la journée, seulement si il existe
            if (_pointsDeDeplacement.Contains(pointSpecial))
            {
                _pointsDeDeplacement.Remove(pointSpecial);
              
            }
        }
    }

    void Initialiser()
    {
        // Position quand la poule arrive sur la ferme
        _agent.enabled = false;
        var point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Count)];
        _agent.enabled = true;
        _agent.destination = point.transform.position;

        gameObject.GetComponent<PondreOeufs>().enabled = true;

        ChoisirDestinationAleatoire();
    }
    //trigger pour que les poules arretent de suivre le joueur quand elles arrivent à la ferme
    private void OnTriggerEnter(Collider other)
    {
        // verifier que le trigger vient de la zone de relachement
        if (other.gameObject == _zoneRelachement)
        {
            Initialiser();
            _suivreJoueur = false;
        }
    }
}