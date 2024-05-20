using System;
using UnityEngine;
//cette classe est inpirée par la classe ChouCroissant
public class Oeuf : MonoBehaviour, IRamassable
{   //le prefab à instancier
    [SerializeField] private GameObject modelPoule;
    private int journeesDeVie = 0;
    private Soleil _soleil;
    //le temps que l'oeuf existe
    private float _tempsCroissance;

    public void Ramasser(Inventaire inventaireJoueur)
    {
        inventaireJoueur.Oeuf++;
        Destroy(gameObject);
    }

    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
    void Start()
    {
        _soleil = FindObjectOfType<Soleil>();  // Trouver l'objet Soleil dans la scène
        _tempsCroissance = 0.0f;  // Initialiser le temps écoulé
        journeesDeVie = 0;  // Initialiser les journées de vie


    }
    void Update()
    {
        _tempsCroissance += _soleil.DeltaMinutesEcoulees;
        if (_tempsCroissance >= ConstantesJeu.MINUTES_PAR_JOUR)
        {
            // gérer la fin d'une journée et la logique pour l'éclosion de l'oeuf
            JourneePassee();  
        }

    }
    //methode qui est appellée à chaque fin de journée
    private void JourneePassee()
    {
        if (_tempsCroissance >= ConstantesJeu.MINUTES_PAR_JOUR)
        {//le compteur est remis à zero
            _tempsCroissance = 0.0f;
            //le nombre de jour est incréementé
            journeesDeVie++;
        }
        if (journeesDeVie >= 3)
        {
            // Déterminer si loeuf va eclore ou disparaitre en générant un float random entre 0 et 1
            float prob = UnityEngine.Random.Range(0f, 1f); 

            if (prob <= 0.75f)
            {
                //l'oeuf pourrit donc le gameobject est juste détruit.
                Destroy(gameObject);
            }
            else
            {
                // L'oeuf éclôt et une nouvelle poule apparaît à sa position
                GameObject poule = Instantiate(modelPoule, transform.position, Quaternion.identity);
                //Detruire l'oeuf
                Destroy(gameObject);  
            }
        }

    }
}