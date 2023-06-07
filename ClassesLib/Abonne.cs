namespace ClassesLib;
public class Abonne
{
    private int m_AbonneId;
    private string m_Nom;
    private string m_Prenom;
    private bool m_Actif;

    public int AbonneId 
    {
        set
        {
            this.m_AbonneId = value;
        }
        get{ return this.m_AbonneId;}
    }

    public string Nom
    {
        set
        {
            this.m_Nom= value;
        }
        get{ return this.m_Nom;}
    }

    public string Prenom
    {
        set
        {
            this.m_Prenom = value;
        }
        get{ return this.m_Prenom;}
    }

    public bool Actif
    {
        set
        {
            this.m_Actif = value;
        }
        get{ return this.m_Actif;}
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.m_AbonneId, this.m_Nom, this.m_Prenom, this.m_Actif);
    }

    public override bool Equals(object? obj)
    {
        if(obj is null || this.GetType().Equals(obj.GetType())) return false;
    
        Abonne a = (Abonne) obj;
        return this.GetHashCode() == a.GetHashCode();
    }
}
