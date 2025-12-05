using gestion_de__ficheir;

class Program
{
    static void Main(string[] args)
    {
        using (Bibliotheque biblio = new Bibliotheque())
        {
            
            bool continuer = true;

        while (continuer)
        {
            Console.WriteLine("\n=============== Bibliotheque ================");
            Console.WriteLine("1- Ajouter un document ");
            Console.WriteLine("2- Afficher tous les documents ");
            Console.WriteLine("3- Rechercher par mot-cle ( Titre - Auteur ) ");
            Console.WriteLine("4- Supprimer un document ");
            Console.WriteLine("5- Sauvgarder dans un fichier ");
            Console.WriteLine("6- Charger depuis un fichier ");
            Console.WriteLine("7- Quitter ");
            Console.Write("\nVotre choix: ");
            
            string choix = Console.ReadLine();
            try
            {
                switch (choix)
                {
                    case "1":
                        AjouterDocument(biblio);
                        break;
                    case "2":
                        biblio.AfficheTous();
                        break;
                    case "3":
                        RchercherDocument(biblio);
                        break;
                    case "4":
                        SupprimerDocument(biblio);
                        break;
                    case "5":
                        SauvgarderBibliotherque(biblio);
                        break;
                    case "6":
                        ChargerBibliotheque(biblio);
                        break;
                    case "7":
                        continuer = false;
                        Console.WriteLine("Au revoir ...");
                        break;
                    default:
                        Console.WriteLine("Choix invalide!");
                        break;
                    
                }
            }
            catch (DocumentNonTrouveExeption e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
            }
        }
        
        }
        Console.WriteLine($"\n Programme termine. Appuyez sur une touche ...");
        Console.ReadKey();
    }
    
    static void AjouterDocument(Bibliotheque biblio)
    {
        Console.WriteLine("Type de document :");
        Console.WriteLine("1- Livre");
        Console.WriteLine("2- Magazine ");
        Console.WriteLine("3- Document PDF ");
        Console.Write("Choix:");
        string type = Console.ReadLine();
        
        Console.Write("Titre : ");
        string titre =  Console.ReadLine();
        Console.Write("Auteur : ");
        string auteur =  Console.ReadLine();
        Console.Write("Annee : ");
        int annee = int.Parse(Console.ReadLine()) ;

        Document doc = null;

        switch (type)
        {
            case "1":
                Console.Write("Nombre de pages :");
                int pages = int.Parse(Console.ReadLine());
                doc = new Livre(titre, auteur, annee, pages);
                break;
            case "2":
                Console.Write("Numero :");
                int numero = int.Parse(Console.ReadLine());
                doc = new Magazine(titre, auteur, annee, numero);
                break;
            case "3":
                Console.Write("Taille en Mo :");
                int taille = int.Parse(Console.ReadLine());
                doc = new DocumentPDF(titre, auteur, annee, taille);
                break;
        }

        if (doc != null)
        {
            biblio.AjouterDocument(doc);
        }

    }

    static void RchercherDocument(Bibliotheque biblio)
    {
        Console.WriteLine("Mot-cle recherche :");
        string motCle = Console.ReadLine();
        biblio.RechherDocument(motCle);
    }
    static void SupprimerDocument(Bibliotheque biblio)
    {
        Console.WriteLine("ID de document pour supprimer :");
        Guid id = Guid.Parse(Console.ReadLine());
        biblio.SupprimerDocument(id);
    }
    static void SauvgarderBibliotherque(Bibliotheque biblio)
    {
        Console.WriteLine("Nom du fichier (ex : C/Desktop/Bibliotheque.txt):");
        string fichier = Console.ReadLine();
        biblio.Sauvgarder(fichier);
    }
    static void ChargerBibliotheque(Bibliotheque biblio)
    {
        Console.WriteLine("Nom du fichier a charger :");
        string fichier = Console.ReadLine();
        biblio.Charger(fichier);
    }
}