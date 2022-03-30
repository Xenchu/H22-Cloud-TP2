using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidAuto.Commandes.API.Model
{
    public class Commande
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Attention, veuillez entrer un numéro identifiant d'utilisateur !")]
        [Display(Name = "Numéro identifiant d'utilisateur")]
        public string? NumeroIdentifiantUtilisateur { get; set; }

        [Required(ErrorMessage = "Attention, ce champ est obligatoire !")]
        [Display(Name = "Id du véhicule")]
        public int IdVehicule { get; set; }
    }
}
