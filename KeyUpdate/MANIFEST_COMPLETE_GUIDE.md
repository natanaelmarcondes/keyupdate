# 🎯 Sistema de Gerenciamento de Arquivos Manifest

## 📋 Resumo da Implementação

Foi criado um sistema completo para gerenciar arquivos na tabela `manifest` seguindo o mesmo padrão visual da tela de clientes/empresas.

---

## 📁 Arquivos Criados

### ✅ Backend
1. **Models/Manifest.cs** - Modelo da entidade Manifest
2. **Controllers/ManifestController.cs** - Controller com todas as ações
3. **Data/ApplicationDbContext.EXAMPLE.cs** - Exemplo do DbContext atualizado

### ✅ Frontend
4. **Views/Manifest/Index.cshtml** - View principal com grid completo
5. **Views/Shared/_Layout.EXAMPLE.cshtml** - Exemplo do Layout com menu atualizado

### ✅ Database
6. **Database/create_manifest_table.sql** - Script SQL para criar a tabela

### ✅ Estilos
7. **wwwroot/css/site.custom.css** - CSS customizado para design consistente

### ✅ Documentação
8. **README_MANIFEST_IMPLEMENTATION.md** - Instruções de integração

---

## 🚀 Funcionalidades Implementadas

### 1. **Listagem em Grid** ✅
- Grid responsivo com todos os arquivos
- Colunas: Arquivo, SHA256, RegSvr32, Core, Ativo, Ações
- Design moderno e consistente com a tela de empresas

### 2. **Upload de Arquivo** ✅
- Modal para selecionar arquivo do computador
- Cálculo automático do SHA256 do arquivo
- Extração automática do nome com extensão
- Validação de arquivo duplicado (por SHA256)

### 3. **Edição Inline** ✅
- Toggle switches para os campos:
  - ✅ RegSvr32
  - ✅ Core
  - ✅ Ativo
- Atualização automática ao clicar no switch
- Feedback visual imediato
- Salvamento instantâneo no banco de dados

### 4. **Exclusão de Arquivo** ✅
- Botão de exclusão no grid
- Confirmação antes de excluir
- Mensagem de sucesso/erro

### 5. **Design Consistente** ✅
- Mesmo padrão visual da tela de empresas
- Bootstrap 5 + Bootstrap Icons
- Cards com sombra
- Tabelas hover
- Alertas auto-fecháveis
- Modal responsivo

---

## 🔧 Passos para Integração

### Passo 1: Atualizar ApplicationDbContext.cs

Abra o arquivo `Data/ApplicationDbContext.cs` e adicione:

```csharp
public DbSet<Manifest> Manifests { get; set; } = null!;
```

E no método `OnModelCreating`:

```csharp
// Configurações da tabela Manifest
modelBuilder.Entity<Manifest>(entity =>
{
	entity.ToTable("manifest");
	entity.HasKey(e => e.Id);
	entity.HasIndex(e => e.Sha256).IsUnique().HasDatabaseName("uq_manifest_sha256");
});
```

**Ou substitua o arquivo inteiro pelo conteúdo de `Data/ApplicationDbContext.EXAMPLE.cs`**

---

### Passo 2: Atualizar _Layout.cshtml

Abra o arquivo `Views/Shared/_Layout.cshtml` e:

1. **Adicione o Bootstrap Icons no `<head>`:**
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css">
```

2. **Adicione o CSS customizado:**
```html
<link rel="stylesheet" href="~/css/site.custom.css" asp-append-version="true" />
```

3. **Adicione o item de menu (após Empresas):**
```html
<li class="nav-item">
	<a class="nav-link text-dark" asp-area="" asp-controller="Manifest" asp-action="Index">
		<i class="bi bi-file-earmark-zip me-1"></i>Arquivos
	</a>
</li>
```

**Ou use o arquivo `Views/Shared/_Layout.EXAMPLE.cshtml` como referência**

---

### Passo 3: Criar a Tabela no Banco de Dados

Execute o script SQL em `Database/create_manifest_table.sql`:

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

**Ou use Entity Framework Migrations:**
```bash
dotnet ef migrations add AddManifestTable
dotnet ef database update
```

---

### Passo 4: Compilar e Executar

```bash
dotnet build
dotnet run
```

Acesse: `https://localhost:7295/Manifest`

---

## 📸 Estrutura da Interface

### Grid de Arquivos
```
+---------------------------------------------------------------+
| 🗂️ Arquivos do Manifest                    [+ Adicionar]     |
+---------------------------------------------------------------+
| Arquivo       | SHA256      | RegSvr32 | Core | Ativo | Ações |
|---------------|-------------|----------|------|-------|-------|
| 📄 file.dll   | a1b2c3...   | [switch] | [ ]  | [x]   | 🗑️    |
| 📄 test.exe   | d4e5f6...   | [ ]      | [x]  | [x]   | 🗑️    |
+---------------------------------------------------------------+
```

### Modal de Upload
```
+------------------------------------+
| 📤 Adicionar Novo Arquivo          |
|------------------------------------|
| Selecione o arquivo:               |
| [Escolher arquivo...]              |
|                                    |
| ℹ️ O SHA256 será calculado        |
|    automaticamente                 |
|                                    |
| [Cancelar]  [Adicionar Arquivo]    |
+------------------------------------+
```

---

## 🎨 Design Features

- ✅ **Cards com sombra** - Visual moderno e limpo
- ✅ **Toggle switches** - Edição inline intuitiva
- ✅ **Hover effects** - Feedback visual nas linhas
- ✅ **Bootstrap Icons** - Ícones consistentes
- ✅ **Alertas auto-fecháveis** - Mensagens temporárias (5s)
- ✅ **Modal responsivo** - Funciona em mobile
- ✅ **Confirmação de exclusão** - Previne erros
- ✅ **Empty state** - Tela amigável quando vazio

---

## 🔐 Segurança

- ✅ **Anti-forgery tokens** - Proteção CSRF
- ✅ **Validação de arquivo** - Verifica se foi selecionado
- ✅ **SHA256 único** - Previne duplicatas
- ✅ **Server-side validation** - Validação no backend

---

## 📊 Campos da Tabela Manifest

| Campo         | Tipo         | Descrição                              |
|---------------|--------------|----------------------------------------|
| id            | BIGINT       | Chave primária auto-incremento         |
| arquivo       | VARCHAR(255) | Nome do arquivo com extensão           |
| sha256        | VARCHAR(64)  | Hash SHA256 do arquivo (único)         |
| regsvr32      | BOOLEAN      | Registrar DLL com regsvr32             |
| core          | BOOLEAN      | Indica se é arquivo core               |
| ativo         | BOOLEAN      | Status ativo/inativo                   |
| criado_em     | DATETIME     | Data de criação                        |
| atualizado_em | DATETIME     | Data da última atualização             |

---

## 🧪 Testando a Funcionalidade

### 1. Upload de Arquivo
1. Clique em "Adicionar Arquivo"
2. Selecione um arquivo do seu computador
3. Clique em "Adicionar Arquivo"
4. O arquivo aparecerá no grid com SHA256 calculado

### 2. Editar Campos
1. Clique nos toggle switches (RegSvr32, Core, Ativo)
2. O valor é salvo automaticamente
3. Veja a mensagem de sucesso

### 3. Excluir Arquivo
1. Clique no ícone de lixeira (🗑️)
2. Confirme a exclusão
3. O arquivo será removido do grid

---

## 🐛 Troubleshooting

### Erro: "Tabela manifest não existe"
**Solução:** Execute o script SQL em `Database/create_manifest_table.sql`

### Erro: "Access denied for user 'root'@'localhost'"
**Solução:** Verifique as credenciais em `appsettings.json`

### Erro: "The type or namespace name 'Manifest' could not be found"
**Solução:** Compile o projeto: `dotnet build`

### Switches não funcionam
**Solução:** Verifique se o jQuery e Bootstrap JS estão carregados

---

## 📝 Notas Técnicas

- **Framework:** ASP.NET Core MVC (.NET 10.0)
- **ORM:** Entity Framework Core
- **Database:** MySQL 8.0+
- **UI:** Bootstrap 5 + Bootstrap Icons
- **Algoritmo:** SHA256 para hash de arquivos

---

## ✨ Próximos Passos (Opcional)

- [ ] Adicionar paginação no grid
- [ ] Implementar busca/filtro
- [ ] Download do arquivo original
- [ ] Histórico de alterações
- [ ] Exportar lista para Excel/CSV
- [ ] API REST para integração

---

## 📞 Suporte

Se tiver alguma dúvida sobre a implementação:
1. Verifique o README_MANIFEST_IMPLEMENTATION.md
2. Consulte os arquivos .EXAMPLE
3. Verifique os comentários no código

---

**✅ Sistema pronto para uso!**

Acesse: `https://localhost:7295/Manifest` ou clique no menu "Arquivos"
