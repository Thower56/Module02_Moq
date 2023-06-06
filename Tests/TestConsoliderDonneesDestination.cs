namespace Tests;
using Moq;
using ClassesLib;

public class TestConsoliderDonneesDestination
{
    [Fact]
    public void TestLaConsolidationDesDonnees()
    {
        Mock<IDepotImportationAbonnes> mockImport = new Mock<IDepotImportationAbonnes>();
        Mock<IDepotAbonnes> mockDepot = new Mock<IDepotAbonnes>();

        mockImport.Setup(i => i.ObtenirAbonnes());
        mockDepot.Setup(d => d.ObtenirAbonnes());
        TraitementLotsConsolidationAbonnes traitement = new TraitementLotsConsolidationAbonnes(mockImport.Object, mockDepot.Object);
        traitement.ConsoliderDonneesDestination();

        mockImport.Verify(i => i.ObtenirAbonnes(), Times.Once);
        mockDepot.Verify(d => d.ObtenirAbonnes(), Times.Once);
        mockImport.VerifyNoOtherCalls();
    }

    [Fact]

    public void TestSiConsoliderAjoutUnAbonneAbsent()
    {
        Mock<IDepotAbonnes> mockDepot = new Mock<IDepotAbonnes>();
        Mock<IDepotImportationAbonnes> mockImpot = new Mock<IDepotImportationAbonnes>();
        Abonne TestAbonne = new Abonne();
        List<Abonne> mockList = new List<Abonne>();
        List<Abonne> mockList2 = new List<Abonne>();
       
        mockList.Add(TestAbonne);
        mockImpot.Setup(i => i.ObtenirAbonnes()).Returns(mockList);
        mockDepot.Setup(d => d.ObtenirAbonne(TestAbonne.AbonneId));

        TraitementLotsConsolidationAbonnes traitement = new TraitementLotsConsolidationAbonnes(mockImpot.Object, mockDepot.Object);
        traitement.ConsoliderDonneesDestination();

        mockImpot.Verify(i => i.ObtenirAbonnes(), Times.Once);
        mockDepot.Verify(d => d.ObtenirAbonne(TestAbonne.AbonneId), Times.Once);
        mockDepot.Verify(d => d.AjouterAbonne(TestAbonne), Times.Once);
        mockDepot.VerifyNoOtherCalls();
    }

    [Fact]

    public void TestLaSourceNeContientPasAbonneDoncEstDeactiver()
    {
        Mock<IDepotAbonnes> mockDepot = new Mock<IDepotAbonnes>();
        Mock<IDepotImportationAbonnes> mockImpot = new Mock<IDepotImportationAbonnes>();
        Abonne TestAbonne = new Abonne();
        List<Abonne> mockList = new List<Abonne>();
       
        mockList.Add(TestAbonne);
        mockDepot.Setup(d => d.ObtenirAbonnes()).Returns(mockList);

        TraitementLotsConsolidationAbonnes traitement = new TraitementLotsConsolidationAbonnes(mockImpot.Object, mockDepot.Object);
        traitement.ConsoliderDonneesDestination();

        mockDepot.Verify(d => d.ObtenirAbonnes(), Times.Once);
        mockDepot.Verify(d => d.DesactiverAbonne(TestAbonne.AbonneId), Times.Once);
        mockDepot.VerifyNoOtherCalls();
    }

    [Fact]

    public void TestLesDeuxListeContientLeMemeAbonne()
    {
        Mock<IDepotAbonnes> mockDepot = new Mock<IDepotAbonnes>();
        Mock<IDepotImportationAbonnes> mockImpot = new Mock<IDepotImportationAbonnes>();
        Abonne TestAbonne = new Abonne();
        Abonne TestAbonne2 = new Abonne();
        List<Abonne> mockList = new List<Abonne>();
        List<Abonne> mockList2 = new List<Abonne>();

        TestAbonne.AbonneId = 1;
        TestAbonne.Nom = "Alain";
        TestAbonne2.AbonneId = 1;
        TestAbonne2.Nom = "Bob";
        mockList.Add(TestAbonne);
        mockList2.Add(TestAbonne2);
        mockImpot.Setup(i => i.ObtenirAbonnes()).Returns(mockList);
        mockDepot.Setup(d => d.ObtenirAbonnes()).Returns(mockList2);
        mockDepot.Setup(d => d.ObtenirAbonne(TestAbonne.AbonneId)).Returns(TestAbonne2);

        TraitementLotsConsolidationAbonnes traitement = new TraitementLotsConsolidationAbonnes(mockImpot.Object, mockDepot.Object);
        traitement.ConsoliderDonneesDestination();

        mockImpot.Verify(i => i.ObtenirAbonnes(), Times.Once);
        mockDepot.Verify(d => d.ObtenirAbonne(TestAbonne2.AbonneId), Times.Once);
        mockDepot.Verify(d => d.MettreAjourAbonne(TestAbonne), Times.Once);
        mockDepot.VerifyNoOtherCalls();
    }

    [Fact]

    public void Test3AbonneUnExistePas1EstEgalEtUnAutrePrenom()
    {
        Mock<IDepotAbonnes> mockDepot = new Mock<IDepotAbonnes>();
        Mock<IDepotImportationAbonnes> mockImpot = new Mock<IDepotImportationAbonnes>();
        Abonne TestAbonne = new Abonne();
        Abonne TestAbonne2 = new Abonne();
        Abonne TestAbonne3 = new Abonne();
        List<Abonne> mockList = new List<Abonne>();
        List<Abonne> mockList2 = new List<Abonne>();

        TestAbonne.AbonneId = 1;
        TestAbonne.Nom = "Alain";
        TestAbonne2.AbonneId = 1;
        TestAbonne2.Nom = "Bob";
        TestAbonne3.AbonneId = 3;
        mockList.Add(TestAbonne);
        mockList.Add(TestAbonne3);
        mockList2.Add(TestAbonne);
        mockList2.Add(TestAbonne2);
        mockImpot.Setup(i => i.ObtenirAbonnes()).Returns(mockList);
        mockDepot.Setup(d1 => d1.ObtenirAbonnes()).Returns(mockList2);
        mockDepot.Setup(d2 => d2.ObtenirAbonne(TestAbonne2.AbonneId)).Returns(TestAbonne);
        mockDepot.Setup(d3 => d3.ObtenirAbonne(TestAbonne3.AbonneId));

        TraitementLotsConsolidationAbonnes traitement = new TraitementLotsConsolidationAbonnes(mockImpot.Object, mockDepot.Object);
        traitement.ConsoliderDonneesDestination();

        mockDepot.Verify(d1 => d1.MettreAjourAbonne(TestAbonne), Times.Once);
        mockDepot.Verify(d2 => d2.ObtenirAbonne(TestAbonne2.AbonneId), Times.Once);
        mockDepot.Verify(d3 => d3.ObtenirAbonne(TestAbonne3.AbonneId), Times.Once);
        mockDepot.Verify(d => d.AjouterAbonne(TestAbonne3), Times.Once);
        mockImpot.Verify(i => i.ObtenirAbonnes(), Times.Once);
        mockDepot.VerifyNoOtherCalls();
    }
}
