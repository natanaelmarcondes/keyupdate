# ⚡ Quick Start - Manifest System

## 🎯 O que foi criado?

Um sistema completo de gerenciamento de arquivos com:
- ✅ Grid com listagem de arquivos
- ✅ Upload de arquivo com cálculo de SHA256
- ✅ Edição inline dos campos RegSvr32, Core e Ativo
- ✅ Exclusão de arquivos
- ✅ Design igual à tela de Empresas

---

## 🚀 3 Passos Rápidos

### 1️⃣ Atualizar ApplicationDbContext.cs

Adicione esta linha:
```csharp
public DbSet<Manifest> Manifests { get; set; } = null!;
```

E no `OnModelCreating`:
```csharp
modelBuilder.Entity<Manifest>(entity =>
{
	entity.ToTable("manifest");
	entity.HasKey(e => e.Id);
	entity.HasIndex(e => e.Sha256).IsUnique().HasDatabaseName("uq_manifest_sha256");
});
```

### 2️⃣ Atualizar _Layout.cshtml

No `<head>`:
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css">
<link rel="stylesheet" href="~/css/site.custom.css" asp-append-version="true" />
```

No menu (após Empresas):
```html
<li class="nav-item">
	<a class="nav-link text-dark" asp-area="" asp-controller="Manifest" asp-action="Index">
		<i class="bi bi-file-earmark-zip me-1"></i>Arquivos
	</a>
</li>
```

### 3️⃣ Criar Tabela no MySQL

Execute:
```sql
CREATE TABLE IF NOT EXISTS manifest (
	id BIGINT AUTO_INCREMENT PRIMARY KEY,
	arquivo VARCHAR(255) NOT NULL,
	sha256 VARCHAR(64) NOT NULL,
	regsvr32 BOOLEAN NOT NULL DEFAULT FALSE,
	core BOOLEAN NOT NULL DEFAULT FALSE,
	ativo BOOLEAN NOT NULL DEFAULT TRUE,
	criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	atualizado_em DATETIME NULL,
	UNIQUE KEY uq_manifest_sha256 (sha256)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
```

---

## ✅ Pronto!

Execute: `dotnet run`

Acesse: `https://localhost:7295/Manifest`

---

## 📁 Arquivos de Referência

- `MANIFEST_COMPLETE_GUIDE.md` - Guia completo
- `Data/ApplicationDbContext.EXAMPLE.cs` - Exemplo do DbContext
- `Views/Shared/_Layout.EXAMPLE.cshtml` - Exemplo do Layout
- `Database/create_manifest_table.sql` - Script SQL

---

## 🎨 Funcionalidades

1. **Upload** - Selecione arquivo e calcule SHA256
2. **Editar** - Toggle switches para RegSvr32, Core, Ativo
3. **Excluir** - Clique na lixeira
4. **Design** - Mesmo padrão da tela de Empresas

---

**🚀 Sistema 100% funcional e pronto para uso!**
