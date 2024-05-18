using UnityEngine;
/// <summary>
/// Classe inspéré de la classe EtatRamasserObjet
/// </summary>
public class EtatPousserObjet : EtatJoueur
{
    private IPoussable objetAPousser;
    private float tempsDePousse = 0.0f;
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

   
    public override void Enter()
    {
        Animateur.SetBool("Push", true);
    }

    public override void Exit()
    {
        Animateur.SetBool("Push", false);
    }

    public override void Handle()
    {
        tempsDePousse += Time.deltaTime;

       
        if (tempsDePousse >= 2.0f && !estPousse)
        {
            objetAPousser.Tomber(Sujet.gameObject,tempsDePousse);
            estPousse = true;
        }
        else if (tempsDePousse >= 4.0f)
        {
            Sujet.ChangerEtat(Sujet.EtatNormal);
        }
    }
}

