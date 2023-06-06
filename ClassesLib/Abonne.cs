namespace ClassesLib;
public class Abonne
{
    public int m_AbonneId {get; set;}
    public string m_Nom{get;set;}
    public string m_Prenom{get;set;}

    private bool m_Actif;

    public override bool Equals(object? obj)
    {
        if(obj is null || this.GetType().Equals(obj.GetType())) return false;
    
        Abonne a = (Abonne) obj;
        return a.m_Nom == this.m_Nom && a.m_AbonneId == this.m_AbonneId && a.m_Prenom == this.m_Prenom;
    }
}
