using UnityEngine;
using UnityEngine.AI;

public class MouvementPoulet : MonoBehaviour
{
    //cette zone s'étant sur toute la ferme 
     private UnityEngine.GameObject _zoneRelachement;
     private UnityEngine.GameObject joueur;
     private bool _suivreJoueur = true;
    //distance à respecter par les poules
    private float distanceDuJoueur = 2f;

    private NavMeshAgent _agent;
    private Animator _animator;

    private GameObject[] _pointsDeDeplacement;

    void Start()
    {
        _zoneRelachement = UnityEngine.GameObject.Find("ZoneRelachePoulet");
         joueur = UnityEngine.GameObject.FindGameObjectWithTag("Joueur");
        //Les poulet commence par suivre le joueur
        _suivreJoueur = true;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointsPoulet");
        _animator.SetBool("Walk", true);
        
    }


    void ChoisirDestinationAleatoire()
    {
        GameObject point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
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
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                ChoisirDestinationAleatoire();
            }
        }


       
    }

    void Initialiser()
    {
        // Position quand la poule arrive sur la ferme
        _agent.enabled = false;
        var point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
        _agent.enabled = true;
        _agent.destination = point.transform.position;

        gameObject.GetComponent<PondreOeufs>().enabled = true;

        ChoisirDestinationAleatoire();
    }
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