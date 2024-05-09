public class ParametresParties
{
    public static ParametresParties Instance { get; private set; } = new ParametresParties();

    public string NomJoueur { get; set; } = "Mathurin";
    public int OrDepart { get; set; } = 200;
    public int OeufsDepart { get; set; } = 5;
    public int SemencesDepart { get; set; } = 5;
    public bool fermier { get; set; } = true;
    public string typeForet { get; set; } = "Grille";

    ///// <summary>
    ///// Nombre de jours n�cessaires � un chou pour �tre pr�ts
    ///// 0 = le chou est d�j� pr�t d�s qu'on le plante
    ///// </summary>
    public int TempsCroissance { get; set; } = 3;

    ///// <summary>
    ///// Nombre de jours pendant lesquels on peut cueillir un chou pr�t
    ///// Plus cette valeur est petite, plus on doit se d�p�cher avant qu'ils ne soient plus bons
    ///// </summary>
    public int DelaiCueillete { get; set; } = 5;

    private ParametresParties()
    {
    }
}