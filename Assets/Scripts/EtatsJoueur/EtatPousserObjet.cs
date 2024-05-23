using UnityEngine;
/// <summary>
/// Classe inspéré de la classe EtatRamasserObjet
/// </summary>
public class EtatPousserObjet : EtatJoueur
{
    //Seulement les arbres qui sont générés avec le patron strategie
    private IPoussable objetAPousser;
    //le temps que le fermier passe à pousser
    private float tempsDePousse = 0.0f;
    //bool pour vérifier si l'arbre a été poussé ou pas encore
    private bool estPousse;

    public override bool EstActif => true;
    public override bool DansDialogue => false;
    public override float EnergieDepensee => ConstantesJeu.COUT_POUSSER;
    //constructeur de l'etat
    public EtatPousserObjet(ComportementJoueur sujet, IPoussable gameObject) : base(sujet)
    {
        objetAPousser = gameObject;
        estPousse = false;
    }

   //activer l'nimation de pousser 
    public override void Enter()
    {
        Animateur.SetBool("Push", true);
    }
    //desactiver l'animation de pousser
    public override void Exit()
    {
        Animateur.SetBool("Push", false);
    }

    public override void Handle()
    {
        tempsDePousse += Time.deltaTime;

       //le joueur pousse pendant 2 secondes
        if (tempsDePousse >= 2.0f && !estPousse)
        {
            //appel de la methode pour faire tomber l'arbre
            objetAPousser.Tomber(Sujet.gameObject,tempsDePousse);
            estPousse = true;
        }
        else if (tempsDePousse >= 4.0f)
        {
            Sujet.ChangerEtat(Sujet.EtatNormal);
        }
    }
}

