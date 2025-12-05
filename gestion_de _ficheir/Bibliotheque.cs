namespace gestion_de__ficheir;

public class Bibliotheque :IDisposable
{
    private List<Document> _documents = new List<Document>();
    private bool _disposed = false;
    private FileStream logStream = null;

    public void AjouterDocument(Document document)
    {
        _documents.Add(document);
    }

    public void SupprimerDocument(Guid id)
    {
        // var doc = _documents.Find(x => x.Id == id);
        // if (doc != null)
        // {
        //     throw new DocumentNonTrouveExeption($"Document avec ID {id} non trouve");
        // }
        //
        // _documents.Remove(doc);
        
        foreach (var doc in _documents)
        {
            if (doc.Id == id)
            {
                _documents.Remove(doc);
                Console.WriteLine("le document est supprimer");
                return;
            }
            else
            {
                throw new DocumentNonTrouveExeption($"Document avec ID {id} non trouve");
            }
        }
     
    }

    public void RechherDocument(string motCle)
    {
        // var results = _documents.Where(d =>
        //     d.Titre.Contains(motCle, StringComparison.OrdinalIgnoreCase) ||
        //     d.Auteur.Contains(motCle, StringComparison.OrdinalIgnoreCase)
        // ).ToList();
        List<Document> results = new List<Document>();
        foreach (var doc in _documents)
        {
            if (doc.Titre.Contains(motCle)||doc.Auteur.Contains(motCle))
            {
                results.Add(doc);
            }
        }
        if (results.Count == 0)
        {
            throw new DocumentNonTrouveExeption($"Document non trouve avec le mot-cle {motCle}");
        }
        Console.WriteLine($"{results.Count} documents trouves --");
        foreach (var doc in results)
        {
            doc.AfficherDetails();
        }
    }

    public void AfficheTous()
    {
        if (_documents.Count == 0)
        {
            Console.WriteLine("Bibliotheque est vide !");
            return;
        }
        Console.WriteLine($"{_documents.Count} documents trouves --");
        foreach (var doc in _documents)
        {
            doc.AfficherDetails();
        }
        
    }

    public void Sauvgarder(string cheminFichier)
    {
        FileStream fs = null;
        StreamWriter Write = null;
        try
        {
            using (fs = new FileStream(cheminFichier, FileMode.Open))
            using (Write = new StreamWriter(fs))
            {
                foreach (var doc in _documents)
                {
                    Write.WriteLine(doc.ToCSV());
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erreur lors de souvgarde :{ex.Message}");
        }
        finally
        {
            Write?.Dispose();
            fs?.Dispose();
            Console.WriteLine("Flux de sauvegarde fermes .");
        }
    }

    public void Charger(string cheminFichier)
    {
        FileStream fs = null;
        StreamReader reader = null;
        try
        {
            if (!File.Exists(cheminFichier))
            {
                throw new DocumentNonTrouveExeption($"Le fichier {cheminFichier} n existe pas");
            }

            _documents.Clear(); // remove old data -> charge new ones from file.xtx 
            using (fs = new FileStream(cheminFichier, FileMode.Open))
            using (reader = new StreamReader(fs))
            {
                String Ligne;
                while ((Ligne = reader.ReadLine()) != null)
                {
                    try
                    {
                        var parts = Ligne.Split('|');
                        string type = parts[0];
                        Guid id = Guid.Parse(parts[1]);
                        string titre = parts[2];
                        string auteur = parts[3];
                        int annee = int.Parse(parts[4]);

                        Document doc = null;

                        switch (type)
                        {
                            case "Livre":
                                int NombrePages = int.Parse(parts[5]);
                                doc = new Livre(titre, auteur, annee, NombrePages);
                                break;
                            case "Magazine":
                                int Numero = int.Parse(parts[5]);
                                doc = new Magazine(titre, auteur, annee, Numero);
                                break;
                            case "PDF":
                                int TailleEnMo = int.Parse(parts[5]);
                                doc = new DocumentPDF(titre, auteur, annee, TailleEnMo);
                                break;
                            default:
                                Console.WriteLine($"Type de document inconnu : {type}");
                                continue;
                        }

                        if (doc != null)
                        {
                            doc.Id = id;
                            _documents.Add(doc);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Erreur de format sur la ligne : {Ligne}-{e.Message}");
                    }
                }
            }

            Console.WriteLine($"bibliotheque chargee depuis -{cheminFichier} ({_documents.Count} documents))");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"fichier non trouve {e.Message}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"Erreur dacces au fichier :{e.Message}");
        }
        finally
        {
            reader?.Dispose();
            fs?.Dispose();
            Console.WriteLine("Flux de chargement fermes .");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_documents!=null)
                {
                    _documents.Clear();
                    _documents = null;
                }

                if (logStream != null)
                {
                    logStream.Dispose();
                    logStream = null;
                }
                Console.WriteLine("les resources ete liberees");
            }

            _disposed = true;
        }
    }

    ~Bibliotheque()
    {
        Dispose(false);
    }
}