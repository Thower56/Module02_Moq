namespace ClassesLib;
public class Abonne
{
    public int m_AbonneId {get; set;}
    public string m_Nom{get;set;}
    public string m_Prenom{get;set;}
    private bool m_Actif;

    public override int GetHashCode()
    {
        return HashCode.Combine(this.m_AbonneId, this.m_Nom, this.m_Prenom);
    }

    public override bool Equals(object? obj)
    {
        if(obj is null || this.GetType().Equals(obj.GetType())) return false;
    
        Abonne a = (Abonne) obj;
        return this.GetHashCode() == a.GetHashCode();
    }
}
