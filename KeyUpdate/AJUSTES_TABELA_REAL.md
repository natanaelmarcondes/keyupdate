# 🎯 AJUSTES REALIZADOS - Estrutura Real da Tabela Manifest

## ✅ Arquivos Atualizados

Todos os arquivos foram ajustados para corresponder à estrutura real da sua tabela `manifest`:

### 1. **Models/Manifest.cs** ✅ Atualizado
- ✅ Campo `manifest_id` (BIGINT) adicionado
- ✅ Campo `path` (VARCHAR 255) em vez de `arquivo`
- ✅ Campo `size` (BIGINT) adicionado
- ✅ Campos `criado_em` e `atualizado_em` removidos (não existem na tabela)
- ✅ Propriedades computadas adicionadas:
  - `NomeArquivo` - Extrai apenas o nome do arquivo do path
  - `TamanhoFormatado` - Formata o tamanho (B, KB, MB, GB)

### 2. **Controllers/ManifestController.cs** ✅ Atualizado
- ✅ Upload agora requer `manifestId` (campo obrigatório)
- ✅ Captura o `size` do arquivo automaticamente
- ✅ Salva o nome do arquivo no campo `path`
- ✅ Validação de duplicata agora considera `sha256` + `manifest_id`
- ✅ Campos `criado_em` e `atualizado_em` removidos

### 3. **Views/Manifest/Index.cshtml** ✅ Atualizado
- ✅ Nova coluna "Manifest ID" com badge azul
- ✅ Coluna "Tamanho" formatada (B, KB, MB, GB)
- ✅ Campo "Manifest ID" adicionado ao modal de upload
- ✅ Exibe o nome do arquivo extraído do path
- ✅ Se houver caminho completo, exibe em texto menor

### 4. **Data/ApplicationDbContext.EXAMPLE.cs** ✅ Atualizado
- ✅ Índices `idx_manifest_id` e `idx_ativo` configurados
- ✅ Conversão de `TINYINT(1)` para `bool` nos campos booleanos
- ✅ Remoção do índice único em `sha256` (permite duplicatas entre manifests diferentes)

---

## 📊 Estrutura da Tabela (Real)

```sql
CREATE TABLE manifest (
	id BIGINT AUTO_INCREMENT PRIMARY KEY,
	manifest_id BIGINT NOT NULL,
	path VARCHAR(255) NOT NULL,
	sha256 VARCHAR(64) NOT NULL,
	size BIGINT NOT NULL,
	regsvr32 TINYINT(1) NOT NULL DEFAULT 0,
	core TINYINT(1) NOT NULL DEFAULT 0,
	ativo TINYINT(1) NOT NULL DEFAULT 1,
	INDEX idx_manifest_id (manifest_id),
	INDEX idx_ativo (ativo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

---

## 🎨 Interface Atualizada

### Grid de Arquivos
```
+--------------------------------------------------------------------------------------------+
| Manifest ID | Arquivo         | SHA256      | Tamanho  | RegSvr32 | Core | Ativo | Ações |
|-------------|-----------------|-------------|----------|----------|------|-------|-------|
|     [1]     | 📄 file.dll     | a1b2c3...   | 2.5 MB   | [switch] | [ ]  | [x]   | 🗑️    |
|     [1]     | 📄 test.exe     | d4e5f6...   | 1.2 MB   | [ ]      | [x]  | [x]   | 🗑️    |
|     [2]     | 📄 app.dll      | 9f8e7d...   | 512 KB   | [x]      | [ ]  | [x]   | 🗑️    |
+--------------------------------------------------------------------------------------------+
```

### Modal de Upload
```
+--------------------------------------------+
| 📤 Adicionar Novo Arquivo                  |
|--------------------------------------------|
| Manifest ID: *                             |
| [  ] <- Digite o ID do manifest            |
|                                            |
| Selecione o arquivo: *                     |
| [Escolher arquivo...]                      |
|                                            |
| ℹ️ O nome, SHA256 e tamanho serão         |
|    calculados automaticamente              |
|                                            |
| [Cancelar]  [Adicionar Arquivo]            |
+--------------------------------------------+
```

---

## 🔧 Passos de Integração (Atualizados)

### Passo 1: Atualizar ApplicationDbContext.cs

**Adicione:**
```csharp
public DbSet<Manifest> Manifests { get; set; } = null!;
```

**No método `OnModelCreating`:**
```csharp
// Configurações da tabela Manifest
modelBuilder.Entity<Manifest>(entity =>
{
	entity.ToTable("manifest");
	entity.HasKey(e => e.Id);
	entity.HasIndex(e => e.ManifestId).HasDatabaseName("idx_manifest_id");
	entity.HasIndex(e => e.Ativo).HasDatabaseName("idx_ativo");

	// Configurar conversão de TINYINT para bool
	entity.Property(e => e.Regsvr32).HasConversion<sbyte>();
	entity.Property(e => e.Core).HasConversion<sbyte>();
	entity.Property(e => e.Ativo).HasConversion<sbyte>();
});
```

### Passo 2: Atualizar _Layout.cshtml

No `<head>`:
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css">
<link rel="stylesheet" href="~/css/site.custom.css" asp-append-version="true" />
```

No menu:
```html
<li class="nav-item">
	<a class="nav-link text-dark" asp-area="" asp-controller="Manifest" asp-action="Index">
		<i class="bi bi-file-earmark-zip me-1"></i>Arquivos
	</a>
</li>
```

### Passo 3: Compilar e Executar

```bash
dotnet build
dotnet run
```

**Pronto!** A tabela já existe, não precisa criar.

---

## 🎯 Funcionalidades

### ✅ Upload de Arquivo
1. Clique em "Adicionar Arquivo"
2. **Digite o Manifest ID** (novo campo obrigatório)
3. Selecione o arquivo do computador
4. O sistema automaticamente:
   - Calcula o SHA256
   - Captura o tamanho em bytes
   - Salva o nome no campo `path`

### ✅ Grid com Informações
- **Manifest ID**: Badge colorido para fácil identificação
- **Arquivo**: Nome extraído do path
- **SHA256**: Hash completo do arquivo
- **Tamanho**: Formatado automaticamente (B, KB, MB, GB)
- **RegSvr32, Core, Ativo**: Toggle switches editáveis

### ✅ Edição Inline
- Clique nos switches para alterar valores
- Salvamento automático no banco
- Feedback visual instantâneo

### ✅ Exclusão
- Clique no ícone de lixeira
- Confirme a exclusão
- Arquivo removido do banco

---

## 🔍 Diferenças da Estrutura Original

| Campo Antigo      | Campo Novo     | Descrição                          |
|-------------------|----------------|------------------------------------|
| arquivo           | path           | Nome/caminho do arquivo            |
| -                 | manifest_id    | ID do manifest (NOVO)              |
| -                 | size           | Tamanho do arquivo (NOVO)          |
| criado_em         | (removido)     | Não existe na tabela real          |
| atualizado_em     | (removido)     | Não existe na tabela real          |

---

## 💡 Validações Implementadas

1. **Manifest ID obrigatório** - Não permite upload sem informar
2. **Arquivo obrigatório** - Valida se arquivo foi selecionado
3. **Duplicata por Manifest** - Mesma combinação SHA256 + manifest_id
4. **Tamanho automático** - Capturado do arquivo real
5. **SHA256 automático** - Calculado durante upload

---

## 🎨 Propriedades Computadas

### NomeArquivo
Extrai apenas o nome do arquivo do path:
- `C:\Temp\arquivo.dll` → `arquivo.dll`
- `arquivo.dll` → `arquivo.dll`

### TamanhoFormatado
Formata o tamanho de forma amigável:
- `1024` → `1 KB`
- `1048576` → `1 MB`
- `1073741824` → `1 GB`

---

## 🚀 Testando

### Teste 1: Upload
1. Acesse `/Manifest`
2. Clique em "Adicionar Arquivo"
3. Digite `1` no Manifest ID
4. Selecione um arquivo qualquer
5. Clique em "Adicionar Arquivo"
6. ✅ Arquivo deve aparecer no grid

### Teste 2: Edição
1. Clique no switch "RegSvr32" de algum arquivo
2. ✅ Deve mudar instantaneamente
3. Verifique no banco: `SELECT * FROM manifest WHERE id = X`
4. ✅ Valor deve estar atualizado

### Teste 3: Exclusão
1. Clique no ícone de lixeira
2. Confirme a exclusão
3. ✅ Arquivo removido do grid e do banco

### Teste 4: Manifest ID Diferentes
1. Adicione arquivo com Manifest ID = 1
2. Adicione MESMO arquivo com Manifest ID = 2
3. ✅ Deve permitir (SHA256 igual, mas manifest_id diferente)

---

## 📝 Mapeamento de Campos

### Model → Banco de Dados

```csharp
[Column("id")]           → id BIGINT
[Column("manifest_id")]  → manifest_id BIGINT
[Column("path")]         → path VARCHAR(255)
[Column("sha256")]       → sha256 VARCHAR(64)
[Column("size")]         → size BIGINT
[Column("regsvr32")]     → regsvr32 TINYINT(1)
[Column("core")]         → core TINYINT(1)
[Column("ativo")]        → ativo TINYINT(1)
```

### TINYINT(1) → bool

O Entity Framework automaticamente converte:
- `0` → `false`
- `1` → `true`

---

## ✅ Pronto para Usar!

Todos os ajustes foram feitos. O sistema está 100% compatível com sua tabela `manifest` existente.

**Próximos passos:**
1. Copie o conteúdo de `Data/ApplicationDbContext.EXAMPLE.cs`
2. Cole no seu `Data/ApplicationDbContext.cs` existente
3. Adicione o menu no `_Layout.cshtml`
4. Compile e execute!

---

**🎉 Sistema ajustado e pronto para produção!**
