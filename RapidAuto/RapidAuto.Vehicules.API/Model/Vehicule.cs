using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RapidAuto.Vehicules.API.Model
{
    public class Vehicule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [DataType(DataType.Text)]
        public string? Constructeur { get; set; }

        [Display(Name = "Modèle")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [MaxLength(30, ErrorMessage = "La taille maximale du champ est de 30 caractères !")]
        public string? Modele { get; set; }

        [Display(Name = "Année")]
        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [Range(1900, 2500, ErrorMessage = "Veuillez entrer une année valide !")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Veuillez respecter le format: XXXX !")]
        public int Annee { get; set; }

        [Display(Name = "Type de véhicule")]
        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [DataType(DataType.Text)]
        public string? TypeVehicule { get; set; }

        [Display(Name = "Nombre de sièges")]
        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [Range(0, 12, ErrorMessage = "Veuillez entrer un nombre de sièges valide !")]
        public int NombreDeSieges { get; set; }

        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "La taille maximale du champ est de 30 caractères !")]
        public string? Couleur { get; set; }

        [Display(Name = "Numéro d'identification du véhicule (NIV)")]
        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [DataType(DataType.Text)]
        [MinLength(17, ErrorMessage = "Le NIV doit avoir exactement 17 caractères")]
        [MaxLength(17, ErrorMessage = "Le NIV doit avoir exactement 17 caractères")]
        public string? NIV { get; set; }

        [Display(Name = "Image du véhicule 1")]
        //[Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [JsonIgnore]
        [NotMapped]
        public IFormFile? Image1 { get; set; }

        [Display(Name = "Image du véhicule 2")]
        //[Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [JsonIgnore]
        [NotMapped]
        public IFormFile? Image2 { get; set; }

        [Display(Name = "Nom de l'image 1")]
        public string? NomImage1 { get; set; }

        [Display(Name = "Nom de l'image 2")]
        public string? NomImage2 { get; set; }


        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [MaxLength(350, ErrorMessage = "La taille maximale du champ est de 350 caractères !")]
        public string? Description { get; set; }

        [Display(Name = "Disponibilité")]
        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        public bool EstDisponible { get; set; }

        [Required(ErrorMessage = "Attention, le champ est obligatoire !")]
        [Range(0, 9999999.99, ErrorMessage = "Veuillez entrer un prix valide !")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Veuillez respecter le format: 12.34 !")]
        public double Prix { get; set; }

        [Display(Name = "Code Unique")]
        public string? CodeUnique { get; set; }
    }
}
