# ⚡ RESUMO - Ajustes para Tabela Manifest Real

## ✅ O que foi ajustado?

Todos os arquivos foram **atualizados** para corresponder à estrutura real da sua tabela:

### Campos Novos
- ✅ **manifest_id** (BIGINT) - ID do manifest
- ✅ **path** (VARCHAR 255) - Caminho do arquivo
- ✅ **size** (BIGINT) - Tamanho do arquivo

### Campos Removidos
- ❌ **arquivo** → Agora é **path**
- ❌ **criado_em** → Não existe na tabela
- ❌ **atualizado_em** → Não existe na tabela

---

## 🎯 Grid Atualizado

| Coluna       | Exibição                           |
|--------------|-------------------------------------|
| Manifest ID  | Badge azul com o ID                |
| Arquivo      | Nome extraído do path              |
| SHA256       | Hash completo                      |
| Tamanho      | Formatado (B, KB, MB, GB)          |
| RegSvr32     | Toggle switch editável             |
| Core         | Toggle switch editável             |
| Ativo        | Toggle switch editável             |
| Ações        | Botão excluir                      |

---

## 📤 Upload Atualizado

Agora o formulário de upload requer:

1. **Manifest ID** (obrigatório) - Digite o ID do manifest
2. **Arquivo** (obrigatório) - Selecione do computador

O sistema calcula automaticamente:
- ✅ SHA256 do arquivo
- ✅ Tamanho (size) em bytes
- ✅ Nome do arquivo (path)

---

## 🔧 Integração (3 Passos)

### 1️⃣ ApplicationDbContext.cs

```csharp
public DbSet<Manifest> Manifests { get; set; } = null!;

// No OnModelCreating:
modelBuilder.Entity<Manifest>(entity =>
{
	entity.ToTable("manifest");
	entity.HasKey(e => e.Id);
	entity.HasIndex(e => e.ManifestId).HasDatabaseName("idx_manifest_id");
	entity.HasIndex(e => e.Ativo).HasDatabaseName("idx_ativo");

	entity.Property(e => e.Regsvr32).HasConversion<sbyte>();
	entity.Property(e => e.Core).HasConversion<sbyte>();
	entity.Property(e => e.Ativo).HasConversion<sbyte>();
});
```

### 2️⃣ _Layout.cshtml

```html
<!-- No <head> -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css">

<!-- No menu -->
<li class="nav-item">
	<a class="nav-link" asp-controller="Manifest" asp-action="Index">
		<i class="bi bi-file-earmark-zip me-1"></i>Arquivos
	</a>
</li>
```

### 3️⃣ Compilar

```bash
dotnet build
dotnet run
```

---

## 📁 Arquivos Atualizados

✅ **Models/Manifest.cs** - Campos ajustados
✅ **Controllers/ManifestController.cs** - Lógica atualizada
✅ **Views/Manifest/Index.cshtml** - Interface com Manifest ID
✅ **Data/ApplicationDbContext.EXAMPLE.cs** - Configuração correta

---

## 🎨 Recursos

- ✅ Upload com Manifest ID
- ✅ Cálculo automático de SHA256 e tamanho
- ✅ Grid com todas as informações
- ✅ Edição inline (RegSvr32, Core, Ativo)
- ✅ Exclusão com confirmação
- ✅ Tamanho formatado automaticamente
- ✅ Design igual à tela de Empresas

---

## 🚀 Como Usar

1. Acesse: `/Manifest`
2. Clique em "Adicionar Arquivo"
3. Digite o **Manifest ID** (ex: 1)
4. Selecione um arquivo
5. Clique em "Adicionar Arquivo"
6. Edite os campos com os switches
7. Exclua com o botão da lixeira

---

## 📖 Documentação Completa

- **AJUSTES_TABELA_REAL.md** - Explicação detalhada de todos os ajustes
- **Data/ApplicationDbContext.EXAMPLE.cs** - Exemplo de configuração

---

**✅ Sistema 100% compatível com sua tabela manifest!**

Estrutura: `id | manifest_id | path | sha256 | size | regsvr32 | core | ativo`
