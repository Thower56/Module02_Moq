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
            Dictionary<int, Abonne> importationAbonne = new Dictionary<int, Abonne>();
            Dictionary<int, Abonne> destinationAbonne = new Dictionary<int, Abonne>();

            foreach(Abonne d in m_depotDestination.ObtenirAbonnes())
            {
                destinationAbonne.Add(d.AbonneId, d);
            }

            foreach(Abonne s in m_depotSource.ObtenirAbonnes())
            {
                importationAbonne.Add(s.AbonneId, s);

                if(!destinationAbonne.ContainsKey(s.AbonneId))
                {
                    m_depotDestination.AjouterAbonne(s);
                }
                if(destinationAbonne.ContainsKey(s.AbonneId))
                {
                    if(!s.Equals(destinationAbonne[s.AbonneId]))
                    {
                        m_depotDestination.MettreAjourAbonne(s);
                    }
                }
            }
            
            foreach(KeyValuePair<int, Abonne> a in destinationAbonne)
            {
                if(!importationAbonne.ContainsKey(a.Key))
                {
                    m_depotDestination.DesactiverAbonne(a.Value.AbonneId);
                }
            }
        }
    }
}