using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionnaireInterface : MonoBehaviour
{
    [SerializeField] private Button _boutonDemarrer;

    enum Difficulte
    {
        Facile,
        Moyen,
        Difficile
    }
    enum Personnage
    {
        Fermier,
        Fermiere
    }
    enum Foret
    {
        Grille,
        Random,
        Simulation
    }

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

    [SerializeField] private TMP_InputField nomJoueur;
    [SerializeField] private TMP_Text presentation;

    [SerializeField] private int[] valeursFacile;
    [SerializeField] private int[] valeursMoyen;
    [SerializeField] private int[] valeursDifficile;

    [SerializeField] private TMP_Text[] valeursDepart;
    [SerializeField] private TMP_Dropdown difficulteDropdown;
    [SerializeField] private TMP_Dropdown personnageDropdown;
    [SerializeField] private TMP_Dropdown foretDropdown;

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
    public void ChangerForet()
    {
        foret = (Foret)foretDropdown.value;

        switch (foret)
        {
            case Foret.Grille:
                typeForet = "Grille";
               
                break;
            case Foret.Random:
                typeForet = "Random";
                break;
            case Foret.Simulation:
                typeForet = "Simulation";
                break;
        }
    }



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

    private void MettreAJour(int[] valeurs)
    {
        for (int i = 0; i < valeursDepart.Length; i++)
        {
            valeursDepart[i].text = valeurs[i].ToString();
        }
    }

    public void ChangerNomJoueur()
    {
        presentation.text = $"\u266A \u266B Dans la ferme \u00e0  {nomJoueur.text} \u266B \u266A";
    }
}