# 📝 Code Snippets - Sistema Manifest

Snippets úteis para copiar e colar durante a implementação.

---

## 🔧 ApplicationDbContext.cs

### DbSet
```csharp
public DbSet<Manifest> Manifests { get; set; } = null!;
```

### OnModelCreating - Configuração Completa
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
	base.OnModelCreating(modelBuilder);

	// Configurações da tabela Empresa
	modelBuilder.Entity<Empresa>(entity =>
	{
		entity.ToTable("empresas");
		entity.HasKey(e => e.Id);
		entity.HasIndex(e => e.EmpCodigo).IsUnique().HasDatabaseName("uq_emp_codigo");
		entity.HasIndex(e => e.EmpNome).IsUnique().HasDatabaseName("uq_emp_nome");
	});

	// Configurações da tabela Manifest
	modelBuilder.Entity<Manifest>(entity =>
	{
		entity.ToTable("manifest");
		entity.HasKey(e => e.Id);
		entity.HasIndex(e => e.Sha256).IsUnique().HasDatabaseName("uq_manifest_sha256");
	});
}
```

### Using Statements
```csharp
using Microsoft.EntityFrameworkCore;
using KeyUpdate.Models;
```

---

## 🎨 _Layout.cshtml

### Head - CDN Links
```html
<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<title>@ViewData["Title"] - KeyUpdate</title>
	<link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
	<link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
	<link rel="stylesheet" href="~/css/site.custom.css" asp-append-version="true" />
	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css">
</head>
```

### Menu Completo
```html
<ul class="navbar-nav flex-grow-1">
	<li class="nav-item">
		<a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">
			<i class="bi bi-house-fill me-1"></i>Home
		</a>
	</li>
	<li class="nav-item">
		<a class="nav-link text-dark" asp-area="" asp-controller="Empresas" asp-action="Index">
			<i class="bi bi-building me-1"></i>Empresas
		</a>
	</li>
	<li class="nav-item">
		<a class="nav-link text-dark" asp-area="" asp-controller="Manifest" asp-action="Index">
			<i class="bi bi-file-earmark-zip me-1"></i>Arquivos
		</a>
	</li>
	<li class="nav-item">
		<a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">
			<i class="bi bi-shield-lock me-1"></i>Privacidade
		</a>
	</li>
</ul>
```

### Scripts no Body
```html
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
<script src="~/js/site.js" asp-append-version="true"></script>
@await RenderSectionAsync("Scripts", required: false)
```

---

## 🗄️ SQL Snippets

### Criar Tabela
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

### Índices Adicionais (Opcional)
```sql
CREATE INDEX idx_manifest_arquivo ON manifest(arquivo);
CREATE INDEX idx_manifest_ativo ON manifest(ativo);
CREATE INDEX idx_manifest_core ON manifest(core);
```

### Consultas Úteis
```sql
-- Listar todos os arquivos ativos
SELECT * FROM manifest WHERE ativo = TRUE ORDER BY arquivo;

-- Contar arquivos por tipo
SELECT 
	CASE 
		WHEN regsvr32 = TRUE THEN 'DLL (RegSvr32)'
		WHEN core = TRUE THEN 'Core'
		ELSE 'Outros'
	END AS tipo,
	COUNT(*) AS quantidade
FROM manifest
GROUP BY tipo;

-- Buscar por nome de arquivo
SELECT * FROM manifest WHERE arquivo LIKE '%nome%';

-- Verificar duplicatas (não deve retornar nada)
SELECT sha256, COUNT(*) 
FROM manifest 
GROUP BY sha256 
HAVING COUNT(*) > 1;
```

---

## 🎯 Program.cs

### Configuração do DbContext
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(connectionString, 
		ServerVersion.AutoDetect(connectionString)));
```

### Connection String (appsettings.json)
```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=localhost;Port=3306;Database=keysystems;User=root;Password=SuaSenha;"
  }
}
```

---

## 🧪 Comandos de Terminal

### Compilar
```bash
dotnet build
```

### Executar
```bash
dotnet run
```

### Entity Framework Migrations (Opcional)
```bash
# Criar migration
dotnet ef migrations add AddManifestTable

# Aplicar migration
dotnet ef database update

# Reverter migration
dotnet ef database update PreviousMigrationName

# Remover última migration
dotnet ef migrations remove
```

### Instalar Pacotes NuGet (se necessário)
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 📊 JavaScript Útil

### Toggle Switch Handler
```javascript
document.querySelectorAll('.toggle-field').forEach(function(checkbox) {
	checkbox.addEventListener('change', function() {
		const id = this.dataset.id;
		const field = this.dataset.field;
		const value = this.checked;

		this.disabled = true;

		fetch('/Manifest/UpdateField', {
			method: 'POST',
			headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
			body: `id=${id}&field=${field}&value=${value}`
		})
		.then(response => response.json())
		.then(data => {
			if (!data.success) {
				alert('Erro: ' + data.message);
				this.checked = !value;
			}
			this.disabled = false;
		})
		.catch(error => {
			alert('Erro: ' + error);
			this.checked = !value;
			this.disabled = false;
		});
	});
});
```

### Confirmação de Exclusão
```javascript
function confirmDelete(id, arquivo) {
	if (confirm(`Tem certeza que deseja excluir "${arquivo}"?`)) {
		document.getElementById('deleteId').value = id;
		document.getElementById('deleteForm').submit();
	}
}
```

### Auto-fechar Alertas
```javascript
setTimeout(function() {
	const alerts = document.querySelectorAll('.alert');
	alerts.forEach(function(alert) {
		const bsAlert = new bootstrap.Alert(alert);
		bsAlert.close();
	});
}, 5000);
```

---

## 🎨 CSS Útil

### Card Hover Effect
```css
.card {
	transition: all 0.3s ease;
}

.card:hover {
	transform: translateY(-5px);
	box-shadow: 0 8px 16px rgba(0,0,0,0.15);
}
```

### Table Row Hover
```css
.table tbody tr {
	transition: all 0.2s ease;
}

.table tbody tr:hover {
	background-color: #f8f9fa;
	transform: translateX(2px);
}
```

### Switch Customizado
```css
.form-check-input {
	cursor: pointer;
	width: 2.5rem;
	height: 1.25rem;
}

.form-check-input:checked {
	background-color: #198754;
	border-color: #198754;
}
```

---

## 🔍 Debugging Snippets

### Verificar Connection String
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection String: {connectionString}");
```

### Log de Erro no Controller
```csharp
catch (Exception ex)
{
	Console.WriteLine($"Erro: {ex.Message}");
	Console.WriteLine($"Stack Trace: {ex.StackTrace}");
	TempData["ErrorMessage"] = $"Erro: {ex.Message}";
}
```

### Testar SHA256
```csharp
using System.Security.Cryptography;

string CalculateSHA256(Stream stream)
{
	using (var sha256 = SHA256.Create())
	{
		var hashBytes = sha256.ComputeHash(stream);
		return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
	}
}
```

---

## 📦 Model Completo

### Manifest.cs
```csharp
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
		[StringLength(255)]
		[Column("arquivo")]
		public string Arquivo { get; set; } = string.Empty;

		[Required]
		[StringLength(64)]
		[Column("sha256")]
		public string Sha256 { get; set; } = string.Empty;

		[Column("regsvr32")]
		public bool Regsvr32 { get; set; }

		[Column("core")]
		public bool Core { get; set; }

		[Column("ativo")]
		public bool Ativo { get; set; } = true;

		[Column("criado_em")]
		public DateTime CriadoEm { get; set; } = DateTime.Now;

		[Column("atualizado_em")]
		public DateTime? AtualizadoEm { get; set; }
	}
}
```

---

## 🚀 Ready to Copy-Paste!

Todos os snippets acima estão prontos para copiar e colar diretamente no seu projeto.

**Lembre-se:** Sempre faça backup antes de modificar arquivos existentes!
