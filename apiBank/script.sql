-- Criação da tabela ContaCorrente
CREATE TABLE ContaCorrente (
    Id CHAR(36) PRIMARY KEY,
    Conta VARCHAR(50) NOT NULL,
    Saldo DECIMAL(18, 2) NOT NULL
);

-- Inserção de um registro de exemplo
INSERT INTO ContaCorrente (Id, Conta, Saldo)
VALUES ('2a6713c1-8e71-4a0f-bc13-63d199bc6b5a', '123456789', 1000.00);
