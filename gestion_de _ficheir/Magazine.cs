namespace gestion_de__ficheir;

public class Magazine :Document
{
    public int Numero { get; set; }
    public string Mg = "Magazine";
    
    public Magazine(string titre, string Auteur, int Annee, int numero):base(titre,Auteur,Annee)
    {
        Numero = numero;
    }
    public override void AfficherDetails()
    {
        Console.WriteLine($"[{Mg}]{Titre}-{Auteur} ({Annee})-N: {Numero} -ID {Id}");
    }

    public override string ToCSV()
    {
        return $"{Mg}|{Id}|{Titre}|{Auteur}|{Annee}|{Numero}";
    }
}