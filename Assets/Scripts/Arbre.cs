using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour, IPoussable
{
    //prefab de la buche à instancier
   [SerializeField] private GameObject logPrefab; 
    /// <summary>
    /// methode de l'interface IPoussable
    /// </summary>
    /// <param name="sujet">joueur</param>
    /// <returns>l'etat EtatPousserObjet</returns>
    public EtatJoueur EtatAUtiliser(ComportementJoueur sujet)
    {
        return new EtatPousserObjet(sujet, this);
    }
    // retourne toujours vrai
    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
    //methode qui est appellée par l'etat
    public void Tomber(GameObject joueur, float DureeTombee)
    {
        StopCoroutine(FaireTomber(joueur, DureeTombee));
        StartCoroutine(FaireTomber(joueur, DureeTombee));
    }

    /// <summary>
    /// methode de la coroutine pour faire tomber 
    /// </summary>
    /// <param name="joueur">gameObject sujet, il est affecter dans l'etat EtatPousserObjet</param>
    /// <param name="dureeTombee"> la dure de la chute après la pousse </param>
    /// <returns>un IEnumerator pour la coroutine</returns>
    private IEnumerator FaireTomber(GameObject joueur, float dureeTombee)
    {
        // Calcul de la vitesse de chute
        float vitesseTombee = 90.0f / dureeTombee;
        float tempsEcoule = 0f;

        // rotation graduelle de l'arbre  
        while (tempsEcoule < dureeTombee)
        {
            //formule obtenue de l'enoncé de l'issue
            transform.Rotate(joueur.transform.right, Time.deltaTime * vitesseTombee, Space.World);
            tempsEcoule += Time.deltaTime;
            yield return null;
        }

        //S'assurer  que l'arbre soit complétement tombé
        transform.Rotate(joueur.transform.right, (dureeTombee - tempsEcoule) * vitesseTombee, Space.World);

        // attendre pour une seconde avant de baisser l'arbre dans le sol
        yield return new WaitForSeconds(1f);

        //calcul de la direction pour poser la buche dans le feuillage
        Vector3 direction = (joueur.transform.position - transform.position).normalized;

        // la hauteur de l'arbre
        float hauteur = -3f;

        // calcul de la position de la buche
        Vector3 positionBuche = transform.position + direction * hauteur;
        positionBuche.y = 0.26f; 
            //rotation de la buche pour pas qu'elle soit debout
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        //instanciation de la buche à la bonne position et avec la bonne rotation
        GameObject log = Instantiate(logPrefab, positionBuche,rotation);


        //descente de l'arbre pendnat 5 secondes avant sa disparition
        float dureDescente = 0.5f;
        tempsEcoule = 0f;
        Vector3 positionInitiale = transform.position;
        Vector3 positionFinale = positionInitiale - new Vector3(0, 1f, 0); 

        while (tempsEcoule < dureDescente)
        {
            transform.position = Vector3.Lerp(positionInitiale, positionFinale, tempsEcoule / dureDescente);
            tempsEcoule += Time.deltaTime;
            yield return null;
        }

        // S'assurer que l'arbre est completement sous le sol
        transform.position = positionFinale;

        // disparition de l'arbre
         Destroy(gameObject);
    }



}
