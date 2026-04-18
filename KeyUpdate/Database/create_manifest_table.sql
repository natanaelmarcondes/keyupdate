-- Script SQL para criar a tabela manifest no MySQL

CREATE TABLE IF NOT EXISTS manifest (
	id BIGINT AUTO_INCREMENT PRIMARY KEY,
	arquivo VARCHAR(255) NOT NULL COMMENT 'Nome do arquivo com extensão',
	sha256 VARCHAR(64) NOT NULL COMMENT 'Hash SHA256 do arquivo',
	regsvr32 BOOLEAN NOT NULL DEFAULT FALSE COMMENT 'Flag para registrar DLL com regsvr32',
	core BOOLEAN NOT NULL DEFAULT FALSE COMMENT 'Flag para indicar se é arquivo core',
	ativo BOOLEAN NOT NULL DEFAULT TRUE COMMENT 'Flag para indicar se está ativo',
	criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Data de criação do registro',
	atualizado_em DATETIME NULL COMMENT 'Data da última atualização',
	UNIQUE KEY uq_manifest_sha256 (sha256)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Tabela de arquivos do manifest';

-- Índices adicionais (opcional)
CREATE INDEX idx_manifest_arquivo ON manifest(arquivo);
CREATE INDEX idx_manifest_ativo ON manifest(ativo);

-- Exemplo de dados para teste (opcional)
-- INSERT INTO manifest (arquivo, sha256, regsvr32, core, ativo) VALUES
-- ('exemplo.dll', 'a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6a7b8c9d0e1f2', true, false, true),
-- ('teste.exe', 'b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6a7b8c9d0e1f2g3', false, true, true);
