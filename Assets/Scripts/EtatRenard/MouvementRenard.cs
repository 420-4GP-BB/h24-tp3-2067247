using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MouvementRenard : MonoBehaviour
{
    [SerializeField] private GameObject[] _pointsPatrouille;
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
        _agent = GetComponent<NavMeshAgent>();
        _indexPatrouille = 0;
        _agent.destination = _pointsPatrouille[_indexPatrouille].transform.position;
        _animator = GetComponent<Animator>();
        _animator.SetBool("walk", true);
        GameObject joueur = GameObject.Find("Joueur");
        Patrouille = new EtatPatrouille(this, joueur, _pointsPatrouille);
        Poursuite = new EtatPoursuite(this, joueur);

        _etat = Patrouille;
        _etat.Enter();
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

