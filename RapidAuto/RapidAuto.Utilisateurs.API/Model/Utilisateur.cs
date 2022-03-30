using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidAuto.Utilisateurs.API.Model
{
    public class Utilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Attention, ce champ est obligatoire !")]
        [MaxLength(250, ErrorMessage = "La taille maximale du champ {0} est de {1} caractères.")]
        [DataType(DataType.Text)]
        public string? Prenom { get; set; }

        [Required(ErrorMessage = "Attention, ce champ est obligatoire !")]
        [MaxLength(250, ErrorMessage = "La taille maximale du champ {0} est de {1} caractères.")]
        [DataType(DataType.Text)]
        public string? Nom { get; set; }

        [Display(Name = "Adresse civique")]
        [Required(ErrorMessage = "Attention, ce champ est obligatoire !")]
        [MaxLength(250, ErrorMessage = "La taille maximale du champ {0} est de {1} caractères.")]
        [DataType(DataType.Text)]
        public string? Adresse { get; set; }

        [Display(Name = "Numéro de téléphone")]
        [Required(ErrorMessage = "Attention, ce champ est obligatoire !")]
        [RegularExpression(@"[0-9]{3}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Veuillez respecter le format suivant: XXX-XXX-XXXX")]
        [DataType(DataType.PhoneNumber)]
        public string? NumeroTelephone { get; set; }

        [Display(Name = "Identifiant de l'utilisateur")]
        [DataType(DataType.Text)]
        public string? IdentifiantUtilisateur { get; set; }
    }
}
