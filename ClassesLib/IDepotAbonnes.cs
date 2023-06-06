namespace ClassesLib
{
    public interface IDepotAbonnes
    {
        void AjouterAbonne(Abonne p_abonne);
        IEnumerable<Abonne> ObtenirAbonnes();
        Abonne ObtenirAbonne(int p_abonneId);
        void MettreAjourAbonne(Abonne p_abonne);
        void DesactiverAbonne(int p_abonneId);
    }
}