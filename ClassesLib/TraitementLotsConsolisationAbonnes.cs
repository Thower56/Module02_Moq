namespace ClassesLib
{
    public class TraitementLotsConsolidationAbonnes
    {
        IDepotImportationAbonnes m_depotSource;
        IDepotAbonnes m_depotDestination;

        public TraitementLotsConsolidationAbonnes(IDepotImportationAbonnes p_depotSource, IDepotAbonnes p_depotDestination)
        {
            this.m_depotSource = p_depotSource;
            this.m_depotDestination = p_depotDestination;
        }

        public void ConsoliderDonneesDestination()
        {
            bool sansRetour = true;
            foreach(Abonne a in m_depotSource.ObtenirAbonnes())
            {
                Abonne resultat = m_depotDestination.ObtenirAbonne(a.m_AbonneId);
                if(resultat != null)
                {
                    if(!resultat.Equals(a))
                    {   
                        m_depotDestination.MettreAjourAbonne(a);
                        sansRetour = false;
                    }
                    else
                    {
                        m_depotDestination.MettreAjourAbonne(a);
                        sansRetour = false;
                    }
                }
                else
                {
                    m_depotDestination.AjouterAbonne(a);
                    sansRetour = false;
                }
            }

            if(sansRetour)
            {
                m_depotDestination.ObtenirAbonnes()
                    .Where(d => !m_depotSource.ObtenirAbonnes().Contains(d))
                    .ToList()
                    .ForEach(d => m_depotDestination.DesactiverAbonne(d.m_AbonneId)); 
            }
            
        }
    }
}