using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyUpdate.Models
{
    [Table("keysiscfg")]
    public class KeySisCfg
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("emp_codigo")]
        [Display(Name = "Código da Empresa")]
        public long EmpCodigo { get; set; }

        [Required]
        [StringLength(120)]
        [Column("emp_nome")]
        [Display(Name = "Empresa")]
        public string EmpNome { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [Column("tipo")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [Column("arquivo")]
        [Display(Name = "Arquivo")]
        public string Arquivo { get; set; } = string.Empty;

        [Column("ativo")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }
}
