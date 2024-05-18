public static class ConstantesJeu
{
    // �a repr�sente la quantit� d'�nergie que le joueur perd � chaque minute
    // dans le jeu
    public const float COUT_MARCHER = 0.001f;
    public const float COUT_COURIR = 0.005f;
    public const float COUT_PLANTER = 0.002f;
    public const float COUT_POUSSER = 0.005f;
    public const float COUT_CUEILLIR = 0.001f;
    public const float COUT_IMMOBILE = 0.0001f;

    // Ce que le joueur peut gagner en �nergie
    public const float GAIN_ENERGIE_MANGER_OEUF = 0.25f;
    public const float GAIN_ENERGIE_MANGER_CHOU = 0.35f;
    public const float GAIN_ENERGIE_SOMMEIL = 0.005f;    // Le joueur gagne de l'�nergie en dormant

    // Constantes relatives au temps dans une journ�e
    public const float MINUTES_PAR_JOUR = 1440.0f;
}
