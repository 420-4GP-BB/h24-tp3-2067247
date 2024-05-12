using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    StrategieArbre strategie = new StrategieGrille();
    [SerializeField] private GameObject arbre;
    Vector3[] tabArbre;
    
    // Start is called before the first frame update
    void Start()
    {
        tabArbre = strategie.ChoisirEmplacement();
        foreach (Vector3 position in tabArbre)
        {
            Instantiate(arbre, position, Quaternion.identity);
        }
    
        

    }

    // Update is called once per frame
    void Update()
    {
        
       
    }


   
}
