using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyUpdate.Models
{
    [Table("manifest")]
    public class Manifest
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("manifest_id")]
        [Display(Name = "Manifest ID")]
        public long ManifestId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("path")]
        [Display(Name = "Arquivo")]
        public string Path { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        [Column("sha256")]
        [Display(Name = "SHA256")]
        public string Sha256 { get; set; } = string.Empty;

        [Required]
        [Column("size")]
        [Display(Name = "Tamanho")]
        public long Size { get; set; }

        [Column("regsvr32")]
        [Display(Name = "Regsvr32")]
        public bool Regsvr32 { get; set; }

        [Column("core")]
        [Display(Name = "Core")]
        public bool Core { get; set; }

        [Column("ativo")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

        [NotMapped]
        public string NomeArquivo => string.IsNullOrWhiteSpace(Path) ? string.Empty : System.IO.Path.GetFileName(Path);

        [NotMapped]
        public string TamanhoFormatado
        {
            get
            {
                string[] unidades = { "B", "KB", "MB", "GB", "TB" };
                double valor = Size;
                int indice = 0;

                while (valor >= 1024 && indice < unidades.Length - 1)
                {
                    valor /= 1024;
                    indice++;
                }

                return $"{valor:0.##} {unidades[indice]}";
            }
        }
    }
}
