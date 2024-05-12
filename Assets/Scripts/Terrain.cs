using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    //StrategieArbre strategie = new StrategieGrille();
    StrategieArbre strategie = new StrategieRandom();
    [SerializeField] private GameObject arbre;
    
    // Start is called before the first frame update
    void Start()
    {
         
        foreach (Vector3 position in strategie.ChoisirEmplacement())
        {
            Instantiate(arbre, position, Quaternion.identity);
        }
    
        

    }

    // Update is called once per frame
    void Update()
    {
        
       
    }


   
}
