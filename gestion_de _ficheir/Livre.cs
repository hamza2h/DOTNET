namespace gestion_de__ficheir;

public class Livre: Document
{
   public int NombrePages { get; set; }
   public string Lv = "Livre";
   public Livre(string titre, string Auteur, int Annee, int nombrePages):base(titre,Auteur,Annee)
   {
      NombrePages = nombrePages;
   }
   public override void AfficherDetails()
   {
      Console.WriteLine($"[{Lv}]{Titre}-{Auteur} ({Annee})-{NombrePages} pages -ID {Id}");
   }

   public override string ToCSV()
   {
      return $"{Lv}|{Id}|{Titre}|{Auteur}|{Annee}|{NombrePages}";
   }
}