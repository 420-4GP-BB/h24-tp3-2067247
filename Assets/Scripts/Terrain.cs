using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTerrain : MonoBehaviour
{
    //liste position d'arbre
    List<Vector3> listeArbres;
    //strategie de forestation
    StrategieArbre strategie;
    //le model à instancier
    [SerializeField] private GameObject arbre;
    //type de foret
    string typeForet;
    // positionRenard
    [SerializeField] private GameObject posRenard;


    // Start is called before the first frame update
    void Start()
    {    
//recuperer la valeur dans le singleton
  typeForet = ParametresParties.Instance.typeForet;
//switch case pour determiner la bonne instance de strategie
        switch (typeForet)
        {
            case "Grille":
                strategie = new StrategieGrille();
                break;
            case "Random":
                strategie = new StrategieRandom();
                break;
            case "Simulation":
                strategie = new StrategieSimulation();
                break;
           default:
                strategie = new StrategieGrille();
               break;

        }

        //boucle pour instancier les arbres
        listeArbres = strategie.ChoisirEmplacement();
        //il n'y aura jamais d'arbre sur la position initiale du renard.
        if(listeArbres.Contains(posRenard.transform.position)){
            listeArbres.Remove(posRenard.transform.position);
        }
        foreach (Vector3 position in listeArbres)
        {//chaque arbre devrait avoir une rotation sur l'axe des y au hasard
            Quaternion quat = Quaternion.Euler(0, Random.Range(-180, 180), 0);
            Instantiate(arbre, position, quat);
        }
    }   
}
