using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionnaireInterface : MonoBehaviour
{
    [SerializeField] private Button _boutonDemarrer;
    //enumeration pour les niveau
    enum Difficulte
    {
        Facile,
        Moyen,
        Difficile
    }
    //enum pour les personnages disponible
    enum Personnage
    {
        Fermier,
        Fermiere
    }
    // enum pour les types de foret
    enum Foret
    {
        Grille,
        Random,
        Simulation
    }
    // variables pour stocker les données
    private Difficulte difficulte;
    private Personnage personnage;
    private Foret foret;
    public bool fermier
    {
        private set;
        get;
    }
    public string typeForet 
    {
        private set;
        get;
    }
    //text filed sur le caneva
    [SerializeField] private TMP_InputField nomJoueur;
    [SerializeField] private TMP_Text presentation;
    //tableaux contenant les valeurs des ressources selon le niveau
    [SerializeField] private int[] valeursFacile;
    [SerializeField] private int[] valeursMoyen;
    [SerializeField] private int[] valeursDifficile;
    //tableau des valeures de depart
    [SerializeField] private TMP_Text[] valeursDepart;
    //les dropdown 
    [SerializeField] private TMP_Dropdown difficulteDropdown;
    [SerializeField] private TMP_Dropdown personnageDropdown;
    [SerializeField] private TMP_Dropdown foretDropdown;
    //Gameobject des personnage
    [SerializeField] private GameObject personnageFermier;
    [SerializeField] private GameObject personnageFermiere;
    

    // Start is called before the first frame update
    void Start()
    {
        fermier = true;
        nomJoueur.text = "Mathurin";
        ChangerNomJoueur();
        personnageFermier.SetActive(true);
        personnageFermiere.SetActive(false);
        difficulte = Difficulte.Facile;
        MettreAJour(valeursFacile);
    }

    void Update()
    {
        _boutonDemarrer.interactable = nomJoueur.text != string.Empty;
    }

    public void ChangerDifficulte()
    {
        difficulte = (Difficulte)difficulteDropdown.value;

        switch (difficulte)
        {
            case Difficulte.Facile:
                MettreAJour(valeursFacile);
                break;
            case Difficulte.Moyen:
                MettreAJour(valeursMoyen);
                break;
            case Difficulte.Difficile:
                MettreAJour(valeursDifficile);
                break;
        }
    }
    // methode pour changer le gameobject du personnage affiché
    public void ChangerPersonnage()
    {
        personnage = (Personnage)personnageDropdown.value;

        if (personnage==Personnage.Fermier)
        {
            personnageFermier.SetActive(true);
            personnageFermiere.SetActive(false);
            fermier = true;

        }
        else
        {
            personnageFermiere.SetActive(true);
            personnageFermier.SetActive(false);
            fermier = false;
        }
    }
    //methode liée au drop down Foret
    public void ChangerForet()
    {
       
        foret = (Foret)foretDropdown.value;

        switch (foret)
        {
            case Foret.Random:
                typeForet = "Random";
                break;
            case Foret.Simulation:
                typeForet = "Simulation";
                break;
            default:
                typeForet = "Grille";
                break;
        }
    }


    //methode pour demarer la partie, en mettant à jour les données du singleton
    public void DemarrerPartie()
    {
        int[] valeursActuelles = null;
        switch (difficulte)
        {
            case Difficulte.Facile:
                valeursActuelles = valeursFacile;
                break;
            case Difficulte.Moyen:
                valeursActuelles = valeursMoyen;
                break;
            case Difficulte.Difficile:
                valeursActuelles = valeursDifficile;
                break;
        }

        ParametresParties.Instance.NomJoueur = nomJoueur.text;
        ParametresParties.Instance.OrDepart = valeursActuelles[0];
        ParametresParties.Instance.OeufsDepart = valeursActuelles[1];
        ParametresParties.Instance.SemencesDepart = valeursActuelles[2];
        ParametresParties.Instance.TempsCroissance = valeursActuelles[3];
        ParametresParties.Instance.DelaiCueillete = valeursActuelles[4];
        //importer les valeurs des paramètres dans l'instance du singleton
        ParametresParties.Instance.fermier = fermier;
        ParametresParties.Instance.typeForet = typeForet;


        if (nomJoueur.text != string.Empty)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ferme");
        }
    }

    public void QuitterJeu()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    //mise à jour des valeurs de départ
    private void MettreAJour(int[] valeurs)
    {
        for (int i = 0; i < valeursDepart.Length; i++)
        {
            valeursDepart[i].text = valeurs[i].ToString();
        }
    }
    //methode pour changer le nom du joueur
    public void ChangerNomJoueur()
    {
        presentation.text = $"\u266A \u266B Dans la ferme \u00e0  {nomJoueur.text} \u266B \u266A";
    }
}