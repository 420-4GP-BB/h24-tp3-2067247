using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collation : MonoBehaviour, IRamassable
{//script comportement joueur pour acceder à la methode mangerCollation et augmenter l'energie
    private ComportementJoueur _joueur;
    //utilise letat EtatRamasserOBjet
    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
    //l'inventaire n'est pas utilisé car la collation est juste mangée
    public void Ramasser(Inventaire inventaireJoueur)
    {
        _joueur.MangerCollation();
        Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        _joueur = UnityEngine.GameObject.FindGameObjectWithTag("Joueur").GetComponent<ComportementJoueur>();
    }


    
}
