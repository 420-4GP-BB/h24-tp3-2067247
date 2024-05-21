using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbreCollationneux : MonoBehaviour
{
    //tableau contenant les prefabs des collations
    [SerializeField] private GameObject[] collations;
    //variable pour contenir la coroutine
    private Coroutine coroutine;

  
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {

        coroutine = StartCoroutine(genererCollation());


    }
    private void Update()
    {
 //verifie qu'il n,y a pas déjà une collation ou une coroutine de commencée
        if (transform.childCount == 0 && coroutine == null)
        {
            coroutine = StartCoroutine(genererCollation());
        }
    }

    /// <summary>
    /// coroutine pour générer les collation à chauqe 30 seconde si il yen a pas deja une
    /// PS: quand le joueur mange une collation, cela lui prend plus que 30secondes
    /// donc la prochaine lui tombe sur la tête.
    /// </summary>
    private IEnumerator genererCollation()
    {
        // attendre pour 30 secondes
        
        //générer un random entre 0 et 2
        int indice = Random.Range(0,2);

        // Verifier si l'arbre a déjà une collation non-mangée
        
            yield return new WaitForSeconds(30f);
            //instancier la collation en mettant l'arbre comme parent
            GameObject instantiatedObject = Instantiate(collations[indice], position, Quaternion.identity, transform);

            // ajuster la position de la collation
            instantiatedObject.transform.localPosition = new Vector3(1.5f, 3f, 0f);
        coroutine = null;
        
    }
}
