namespace gestion_de__ficheir;

public class DocumentPDF : Document
{
    public Double TailleEnMo { get; set; }
    public string Pdf = "PDF";
    public DocumentPDF(string titre, string Auteur, int Annee, int tailleEnMo):base(titre,Auteur,Annee)
    {
        TailleEnMo = tailleEnMo;
    }
    public override void AfficherDetails()
    {
        Console.WriteLine($"[{Pdf}]{Titre}-{Auteur} ({Annee})-{TailleEnMo} Mo -ID {Id}");
    }

    public override string ToCSV()
    {
        return $"{Pdf}|{Id}|{Titre}|{Auteur}|{Annee}|{TailleEnMo}";
    }
}