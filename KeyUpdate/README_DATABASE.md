# Instruções para Configuração do Banco de Dados

## Pré-requisitos
- MariaDB instalado e em execução na porta 3606
- Banco de dados **keysystems** já criado
- Tabelas já existentes (empresas, logs, manifest, keysiscfg)

## Configuração da Connection String

A connection string já está configurada no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3606;Database=keysystems;User=root;Password=SUA_SENHA;"
}
```

**IMPORTANTE:** Atualize o campo `Password` com a senha do seu servidor MariaDB.

## Estrutura da Tabela Empresas

O sistema utiliza a tabela `empresas` existente:

```sql
CREATE TABLE empresas (
	id BIGINT AUTO_INCREMENT PRIMARY KEY,
	emp_codigo BIGINT NOT NULL,
	emp_nome VARCHAR(120) NOT NULL,
	exe_mode ENUM('LOCAL','SERVIDOR') NOT NULL DEFAULT 'SERVIDOR',
	ativo TINYINT(1) NOT NULL DEFAULT 1,
	atualizado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
		ON UPDATE CURRENT_TIMESTAMP,
	UNIQUE KEY uq_emp_codigo (emp_codigo),
	UNIQUE KEY uq_emp_nome (emp_nome),
	INDEX idx_ativo (ativo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

## Mapeamento do Modelo

O modelo `Empresa` está mapeado para a tabela `empresas`:

- **Id** → `id` (BIGINT, PRIMARY KEY)
- **EmpCodigo** → `emp_codigo` (BIGINT, UNIQUE)
- **EmpNome** → `emp_nome` (VARCHAR(120), UNIQUE)
- **ExeMode** → `exe_mode` (ENUM: 'LOCAL', 'SERVIDOR')
- **Ativo** → `ativo` (TINYINT)
- **AtualizadoEm** → `atualizado_em` (DATETIME, auto-update)

## Como Executar o Projeto

1. **Configure a senha do banco de dados** no `appsettings.json`

2. **Execute o projeto:**
   ```bash
   dotnet run
   ```

3. **Acesse a aplicação:**
   - Home: http://localhost:5000
   - Empresas: http://localhost:5000/Empresas

## Funcionalidades Implementadas

✅ Listagem de empresas com grid responsivo  
✅ Cadastro de novas empresas  
✅ Edição de empresas existentes  
✅ Exclusão de empresas (com confirmação)  
✅ Validação de campos únicos (emp_codigo e emp_nome)  
✅ Filtros por status (Ativo/Inativo)  
✅ Display de modo de execução (Local/Servidor)  
✅ Timestamp automático de atualização  

## Observações

- O campo `emp_codigo` deve ser único
- O campo `emp_nome` deve ser único
- O campo `atualizado_em` é atualizado automaticamente pelo banco de dados
- As outras tabelas (logs, manifest, keysiscfg) podem ser implementadas futuramente

