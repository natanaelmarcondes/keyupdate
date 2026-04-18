# Instruções de Atualização para Implementar o Manifest

## 1. Atualizar ApplicationDbContext.cs

Adicione a seguinte linha na classe `ApplicationDbContext`:

```csharp
public DbSet<Manifest> Manifests { get; set; } = null!;
```

E no método `OnModelCreating`, adicione:

```csharp
// Configurações da tabela Manifest
modelBuilder.Entity<Manifest>(entity =>
{
	entity.ToTable("manifest");
	entity.HasKey(e => e.Id);
	entity.HasIndex(e => e.Sha256).IsUnique().HasDatabaseName("uq_manifest_sha256");
});
```

## 2. Atualizar _Layout.cshtml

No arquivo `Views/Shared/_Layout.cshtml`, adicione este item no menu de navegação (após o item de Empresas):

```html
<li class="nav-item">
	<a class="nav-link text-dark" asp-area="" asp-controller="Manifest" asp-action="Index">
		<i class="bi bi-file-earmark-zip me-1"></i>Arquivos
	</a>
</li>
```

## 3. Adicionar Bootstrap Icons

Se ainda não tiver, adicione no `<head>` do _Layout.cshtml:

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css">
```

## 4. Executar Migrations (se estiver usando)

Se estiver usando Entity Framework Migrations:

```bash
dotnet ef migrations add AddManifestTable
dotnet ef database update
```

## 5. SQL para criar a tabela manualmente (se necessário)

Se não estiver usando migrations, execute este SQL no MySQL:

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

## Arquivos Criados

✅ **Models/Manifest.cs** - Modelo da entidade
✅ **Controllers/ManifestController.cs** - Controller com as ações
✅ **Views/Manifest/Index.cshtml** - View principal com grid

## Funcionalidades Implementadas

1. ✅ Listagem de todos os arquivos em grid
2. ✅ Upload de arquivo com cálculo automático de SHA256
3. ✅ Edição inline dos campos: RegSvr32, Core e Ativo (toggle switches)
4. ✅ Exclusão de arquivo com confirmação
5. ✅ Design consistente com a tela de Empresas
6. ✅ Validação de arquivo duplicado por SHA256
7. ✅ Mensagens de sucesso/erro
8. ✅ Modal para upload de arquivo
9. ✅ Interface responsiva e moderna

## Como Usar

1. Acesse `/Manifest` ou clique no menu "Arquivos"
2. Clique em "Adicionar Arquivo" para fazer upload
3. Use os switches para alternar RegSvr32, Core e Ativo
4. Clique no ícone de lixeira para excluir um arquivo
