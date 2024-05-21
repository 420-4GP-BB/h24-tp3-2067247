using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MouvementRenard : MonoBehaviour
{
    private GameObject[] _pointsPatrouille;
    private GameObject[] poules;
    private NavMeshAgent _agent;
    private int _indexPatrouille;
    private Animator _animator;

    private EtatRenard _etat;

    public EtatPatrouille Patrouille
    {
        private set;
        get;
    }

    public EtatPoursuite Poursuite
    {
        private set;
        get;
    }

    void Start()
    {

        _pointsPatrouille = GameObject.FindGameObjectsWithTag("PointRenard");
        poules = GameObject.FindGameObjectsWithTag("Poule");
        Patrouille = new EtatPatrouille(this, _pointsPatrouille);
        Poursuite = new EtatPoursuite(this);
        _etat = Patrouille;
        _etat.Enter();
        _agent = GetComponent<NavMeshAgent>();
        _indexPatrouille = 0;
        _agent.destination = _pointsPatrouille[_indexPatrouille].transform.position;
      
        
    }

    // Update is called once per frame
    void Update()
    {
        _etat.Handle(Time.deltaTime);
    }

    public void ChangerEtat(EtatRenard nouvelEtat)
    {
        _etat.Leave();
        _etat = nouvelEtat;
        _etat.Enter();
    }

}

