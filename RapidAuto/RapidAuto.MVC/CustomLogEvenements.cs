namespace RapidAuto.MVC
{
    public class CustomLogEvenements
    {
        // Code pour recherche d'un véhicule
        public const int Recherche = 90;
        
        // Codes pour les actions (Détail/Ajout/Edit/Delete) sur les véhicules
        public const int LectureDUnVehicule = 100;
        public const int EnregistrementVehicule = 101;
        public const int ModificationVehicule = 102;
        public const int SuppressionVehicule = 103;

        // Code pour l'ajout d'une commande
        public const int EnregistrementCommande = 201;

        //Codes pour les évènements des services
        public const int LogCommandeService = 300;
        public const int LogFichierService = 301;
        public const int LogUtilisateurService = 302;
        public const int LogVehiculeService = 303;

    }
}
