using System.ComponentModel.DataAnnotations;

namespace KeyUpdate.Models.ViewModels
{
    public class ArquivosPermitidosViewModel
    {
        public long? EmpresaSelecionadaId { get; set; }
        public string EmpresaSelecionadaNome { get; set; } = string.Empty;
        public List<EmpresaItemViewModel> Empresas { get; set; } = new();
        public List<ArquivoManifestItemViewModel> ArquivosDisponiveis { get; set; } = new();
        public List<KeySisCfgItemViewModel> ArquivosPermitidos { get; set; } = new();
    }

    public class EmpresaItemViewModel
    {
        public long Id { get; set; }
        public long EmpCodigo { get; set; }
        public string EmpNome { get; set; } = string.Empty;
    }

    public class ArquivoManifestItemViewModel
    {
        public long Id { get; set; }
        public string Arquivo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public long ManifestId { get; set; }
    }

    public class KeySisCfgItemViewModel
    {
        public long Id { get; set; }
        public long EmpCodigo { get; set; }
        public string EmpNome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Arquivo { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
