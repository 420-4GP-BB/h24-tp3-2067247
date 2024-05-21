using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Soleil _soleil;
    [SerializeField] private GameObject fermier;
    [SerializeField] private GameObject fermiere;
    // positionRenard
    [SerializeField] private GameObject posRenard;
    //model du renard
    [SerializeField] private GameObject PrefabRenard;
    private GameObject Renard;
    private ComportementJoueur _joueur;
    private const float DISTANCE_ACTION = 3.0f;

    private Inventaire _inventaireJoueur;
    private EnergieJoueur _energieJoueur;
    private ChouMesh3D[] _chous;
    public int NumeroJour = 1;

    private void Awake()
    {
        if (ParametresParties.Instance.fermier)
        {
            fermiere.SetActive(false);
        }
        else
        {
            fermier.SetActive(false);
        }
    }
    void Start()
    {
        
        
        _joueur = GameObject.FindGameObjectWithTag("Joueur").GetComponent<ComportementJoueur>();
        _inventaireJoueur = _joueur.GetComponent<Inventaire>();
        _energieJoueur = _joueur.GetComponent<EnergieJoueur>();
        _chous = FindObjectsByType<ChouMesh3D>(FindObjectsSortMode.None);

        // Patron de conception: Observateur
        FindObjectOfType<Soleil>().OnJourneeTerminee += NouvelleJournee;

        _energieJoueur.OnEnergieVide += EnergieVide;
    }

    void NouvelleJournee()
    {
        NumeroJour++;

        GameObject[] poules = GameObject.FindGameObjectsWithTag("Poule");
        foreach (var poule in poules)
        {
            poule.GetComponent<PondreOeufs>().DeterminerPonte();
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuConfiguration");
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = 45;
        }
        else
        {
            Time.timeScale = 1;
        }

        // L'?tat du joueur peut affecter le passage du temps (ex.: Dodo: tout va vite, menus: le temps est stopp?, etc)
        Time.timeScale *= _joueur.GetComponent<ComportementJoueur>().MultiplicateurScale;

       
    }
    //J'ai L,instanciation/destruction du renard dans un fixed update car on a pas besoin de verifier aussi fréquemment que dans une update normal

    private void FixedUpdate()
    {
        if (_soleil.EstNuitRenard)
        {
            //Verifier si c'est la nuit et que le renard n'est pas déjà instancié
            if (Renard == null)
            {
                // Instancier le renard à la position de départ sans rotation
                Renard = Instantiate(PrefabRenard, posRenard.transform.position, Quaternion.identity);
            }
        }
        else
        {
            // Verifier si ce n'est pas la nuit et que le renard existe 
            if (Renard != null)
            {
                // Détruire l'objet renard
                Destroy(Renard);
            }
        }
    }

    /// <summary>
    /// Partie perdue quand l'?nergie tombe ? z?ro
    /// </summary>
    private void EnergieVide()
    {
        _joueur.ChangerEtat(new EtatDansMenu(_joueur));
        GestionnaireMessages.Instance.AfficherMessageFin(
            "Plus d'?nergie!",
            "Vous n'avez pas r?ussi ? vous garder en vie, vous tombez sans connaissance au milieu du champ." +
            "Un loup passe et vous d?guste en guise de d?ner. Meilleure chance la prochaine partie!");
        Time.timeScale = 0;
    }
}