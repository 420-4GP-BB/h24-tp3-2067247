using System.Collections;
using System.Net;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestChoux
{
    private GameObject chou, soleil;
    private Inventaire inventaire;

    [SetUp]
    public void CreerObjets()
    {
        soleil = new GameObject("Directional Light");
        soleil.AddComponent<Light>();
        soleil.AddComponent<Soleil>();
        chou = GameObject.Instantiate(PrefabUtility.LoadPrefabContents("Assets/Prefabs/Chou.prefab"));

        var joueur = new GameObject("Joueur");
        inventaire = joueur.AddComponent<Inventaire>();
    }

    [TearDown]
    public void DetruireObjets()
    {
        GameObject.Destroy(soleil);
        GameObject.Destroy(chou);
        GameObject.Destroy(inventaire.gameObject);
    }

    [UnityTest]
    public IEnumerator TestChouCueillir()
    {
        // ====== EXEMPLE DE TEST DÉJÀ FONCTIONNEL ======
        // Valide ce qui se passe quand on plante un chou, qu'on attend 3 jours, puis qu'on le cueille.
        // On vérifie que le nombre de choux


        // ARRANGE: dans le SetUp + ici
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        // Trois jours pour pousser :
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        var chouPret = chou.GetComponent<ChouPret>();

        chouPret.Ramasser(inventaire);
        yield return null;

        // ASSERT
        Assert.AreEqual(inventaire.Choux, 1);
    }

    [UnityTest]
    public IEnumerator TestChouPerdGraine()
    {
        // TODO: Tester que quand on vient de planter un chou, l'inventaire a une graine en moins
        //
        // Faites un :         yield return null;
        // après avoir planté le chou, question de simuler qu'au moins 1 frame s'est écoulée avant que
        // vous fassiez votre test

       // ARRANGE: dans le SetUp + ici
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;
        // ASSERT
        Assert.AreEqual(inventaire.Graines, 0);
    }

    [UnityTest]
    public IEnumerator TestChouJourneesPassees()
    {
        // TODO: Tester qu'au bout de 3 jours, le chou est prêt à se faire cueillir
        //
        // Faites un :         yield return null;
        // après chaque appel de la méthode JourneePassee(); du composant ChouCroissant, question de simuler
        // qu'au moins 1 frame s'écoule entre chaque appel
        // ARRANGE: dans le SetUp + ici
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        // Trois jours pour pousser :
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        var chouPret = chou.GetComponent<ChouPret>();

        yield return null;

        // ASSERT
        // le chou "prêt" est bien actif après 3 jours, l'énoncé pour ce test est ambigu.
        Assert.IsTrue(chouPret.isActiveAndEnabled);
    }

    [UnityTest]
    public IEnumerator TestChouReplanter()
    {
        // TODO: Vérifier qu'on peut replanter un deuxième chou sur le même emplacement
        // après l'avoir cueilli

        // ARRANGE: dans le SetUp + ici
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 2;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        // Trois jours pour pousser :
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        var chouPret = chou.GetComponent<ChouPret>();

        chouPret.Ramasser(inventaire);
        yield return null;
        emplacement = chou.GetComponent<EmplacementChouVide>();
        emplacement.Planter(inventaire);
        yield return null;

        // ASSERT
        //verifier qu'il y a bien un chou croissant apres avoir planté une graine
        Assert.IsTrue(chou.GetComponent<ChouCroissant>() != null);

    }
}
