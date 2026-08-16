-- ============================================================
-- BANCO
-- ============================================================

DROP DATABASE IF EXISTS RESTAURANTE;

CREATE DATABASE RESTAURANTE
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE RESTAURANTE;


-- ============================================================
-- USUÁRIOS
-- ============================================================

CREATE TABLE USUARIOS (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    NOME VARCHAR(100) NOT NULL,
    USERNAME VARCHAR(50) NOT NULL,
    PASSWORD VARCHAR(255) NOT NULL,
    CRIADO TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EDITADO TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT UK_USUARIO_USERNAME
        UNIQUE (USERNAME)
);

INSERT INTO USUARIOS
    (NOME, USERNAME, PASSWORD)
VALUES
    ('ADMINISTRADOR', 'admin', 'admin'),
    ('CAIXA', 'caixa', '123456'),
    ('GERENTE', 'gerente', '123456');


-- ============================================================
-- FORNECEDORES
-- ============================================================

CREATE TABLE FORNECEDORES (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    CPF VARCHAR(14),
    CNPJ VARCHAR(18),
    NOME VARCHAR(100) NOT NULL,
    UF VARCHAR(2) NOT NULL,
    CIDADE VARCHAR(100) NOT NULL,
    BAIRRO VARCHAR(80) NOT NULL,
    RUA VARCHAR(100) NOT NULL,
    NUMERO VARCHAR(10) NOT NULL,
    TELEFONE VARCHAR(20) NOT NULL,

    CONSTRAINT UK_FORNECEDOR_CPF
        UNIQUE (CPF),

    CONSTRAINT UK_FORNECEDOR_CNPJ
        UNIQUE (CNPJ),

    CONSTRAINT UK_FORNECEDOR_ENDERECO
        UNIQUE (UF, CIDADE, BAIRRO, RUA, NUMERO),

    CONSTRAINT UK_FORNECEDOR_TELEFONE
        UNIQUE (TELEFONE)
);


INSERT INTO FORNECEDORES
    (CPF, CNPJ, NOME, UF, CIDADE, BAIRRO, RUA, NUMERO, TELEFONE)
VALUES
    (
        NULL,
        '12345678000101',
        'Distribuidora Nordeste LTDA',
        'PE',
        'Vitoria de Santo Antao',
        'Centro',
        'Rua Principal',
        '100',
        '81999990001'
    ),
    (
        NULL,
        '98765432000102',
        'Bebidas Nordeste LTDA',
        'PE',
        'Recife',
        'Boa Viagem',
        'Avenida Boa Viagem',
        '500',
        '81999990002'
    ),
    (
        '71129785483',
        NULL,
        'Rhuan Victor da Silva Oliveira',
        'PE',
        'Vitoria de Santo Antao',
        'Flores',
        'Rua 4',
        '28',
        '81987031072'
    );


-- ============================================================
-- CATEGORIAS
-- ============================================================

CREATE TABLE CATEGORIAS (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    NOME VARCHAR(100) NOT NULL,

    CONSTRAINT UK_CATEGORIA_NOME
        UNIQUE (NOME)
);


INSERT INTO CATEGORIAS (NOME)
VALUES
    ('COMIDA'),
    ('BEBIDA'),
    ('SOBREMESA'),
    ('PORCAO'),
    ('MOLHO');


-- ============================================================
-- ESTOQUE
-- ============================================================

CREATE TABLE ESTOQUE (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    IDFORNECEDOR INT NOT NULL,
    PRODUTO VARCHAR(100) NOT NULL,
    DESCRICAO VARCHAR(160),
    VALOR DECIMAL(10,2) NOT NULL,
    PESO DECIMAL(10,3),
    QTD_MIN INT NOT NULL,
    QUANTIDADE INT NOT NULL DEFAULT 0,
    IDCATEGORIA INT NOT NULL,

    CONSTRAINT FK_ESTOQUE_FORNECEDOR
        FOREIGN KEY (IDFORNECEDOR)
        REFERENCES FORNECEDORES(ID),

    CONSTRAINT FK_ESTOQUE_CATEGORIA
        FOREIGN KEY (IDCATEGORIA)
        REFERENCES CATEGORIAS(ID)
);


INSERT INTO ESTOQUE
    (IDFORNECEDOR, PRODUTO, DESCRICAO, VALOR, PESO, QTD_MIN, QUANTIDADE, IDCATEGORIA)
VALUES
    (1, 'OLEO SOYA 1L',
        'OLEO DE COZINHA SOYA 1 LITRO',
        10.51, 1.000, 10, 50, 1),

    (1, 'BATATA CONGELADA 10KG',
        'BATATA CONGELADA PARA FRITURA',
        95.00, 10.000, 5, 20, 4),

    (1, 'CARNE BOVINA 5KG',
        'CARNE BOVINA PARA HAMBURGUER',
        120.00, 5.000, 5, 15, 1),

    (2, 'COCA COLA LATA',
        'REFRIGERANTE COCA COLA 350ML',
        3.50, 0.350, 20, 100, 2),

    (2, 'GUARANA LATA',
        'REFRIGERANTE GUARANA 350ML',
        3.50, 0.350, 20, 100, 2);


-- ============================================================
-- MESAS
-- ============================================================

CREATE TABLE MESAS (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    NOME VARCHAR(100) NOT NULL,

    CONSTRAINT UK_MESA_NOME
        UNIQUE (NOME)
);


INSERT INTO MESAS (NOME)
VALUES
    ('MESA 1'),
    ('MESA 2'),
    ('MESA 3'),
    ('MESA 4'),
    ('MESA 5'),
    ('MESA 6'),
    ('MESA 7'),
    ('MESA 8'),
    ('MESA 9'),
    ('MESA 10');


-- ============================================================
-- PRODUTOS
-- ============================================================

CREATE TABLE PRODUTOS (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    NOME VARCHAR(100) NOT NULL,
    DESCRICAO VARCHAR(160),
    VALOR DECIMAL(10,2) NOT NULL,
    IDCATEGORIA INT NOT NULL,

    CONSTRAINT UK_PRODUTO_NOME
        UNIQUE (NOME),

    CONSTRAINT FK_PRODUTO_CATEGORIA
        FOREIGN KEY (IDCATEGORIA)
        REFERENCES CATEGORIAS(ID)
);


INSERT INTO PRODUTOS
    (NOME, DESCRICAO, VALOR, IDCATEGORIA)
VALUES
    ('X-BURGER',
        'HAMBURGUER ARTESANAL COM QUEIJO',
        25.00,
        1),

    ('X-SALADA',
        'HAMBURGUER COM QUEIJO, ALFACE E TOMATE',
        28.00,
        1),

    ('X-BACON',
        'HAMBURGUER COM QUEIJO E BACON',
        32.00,
        1),

    ('BATATA FRITA 300G',
        'PORCAO DE BATATA FRITA 300 GRAMAS',
        18.00,
        4),

    ('BATATA FRITA 500G',
        'PORCAO DE BATATA FRITA 500 GRAMAS',
        25.00,
        4),

    ('COCA COLA LATA',
        'REFRIGERANTE COCA COLA 350ML',
        7.00,
        2),

    ('GUARANA LATA',
        'REFRIGERANTE GUARANA 350ML',
        6.00,
        2),

    ('SUCO DE LARANJA',
        'SUCO NATURAL DE LARANJA',
        8.00,
        2),

    ('PUDIM',
        'PUDIM DE LEITE CONDENSADO',
        10.00,
        3),

    ('SORVETE',
        'SORVETE DE CREME',
        8.00,
        3),

    ('MOLHO BARBECUE',
        'MOLHO BARBECUE ESPECIAL',
        3.00,
        5),

    ('MOLHO CHEDDAR',
        'MOLHO DE CHEDDAR',
        4.00,
        5);


-- ============================================================
-- PRODUTOS x ACOMPANHAMENTOS
-- RELAÇÃO N:N
-- ============================================================

CREATE TABLE PRODUTO_ACOMPANHAMENTOS (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    IDPRODUTO INT NOT NULL,
    IDACOMPANHAMENTO INT NOT NULL,

    CONSTRAINT FK_PA_PRODUTO
        FOREIGN KEY (IDPRODUTO)
        REFERENCES PRODUTOS(ID),

    CONSTRAINT FK_PA_ACOMPANHAMENTO
        FOREIGN KEY (IDACOMPANHAMENTO)
        REFERENCES PRODUTOS(ID),

    CONSTRAINT UK_PRODUTO_ACOMPANHAMENTO
        UNIQUE (IDPRODUTO, IDACOMPANHAMENTO)
);


-- X-BURGER
-- Pode receber batata, coca, guarana e molhos

INSERT INTO PRODUTO_ACOMPANHAMENTOS
    (IDPRODUTO, IDACOMPANHAMENTO)
VALUES
    (1, 4),
    (1, 6),
    (1, 7),
    (1, 11),
    (1, 12);


-- X-SALADA
INSERT INTO PRODUTO_ACOMPANHAMENTOS
    (IDPRODUTO, IDACOMPANHAMENTO)
VALUES
    (2, 4),
    (2, 6),
    (2, 8),
    (2, 11),
    (2, 12);


-- X-BACON
INSERT INTO PRODUTO_ACOMPANHAMENTOS
    (IDPRODUTO, IDACOMPANHAMENTO)
VALUES
    (3, 4),
    (3, 5),
    (3, 6),
    (3, 11),
    (3, 12);


-- ============================================================
-- CONTAS
-- ============================================================

CREATE TABLE CONTAS (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    IDMESA INT NOT NULL,
    STATUS VARCHAR(20) NOT NULL DEFAULT 'ABERTA',
    ABERTA TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FECHADA TIMESTAMP NULL,

    CONSTRAINT FK_CONTA_MESA
        FOREIGN KEY (IDMESA)
        REFERENCES MESAS(ID)
);


-- ============================================================
-- ITENS DA CONTA
-- ============================================================

CREATE TABLE ITENS_CONTA (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    IDCONTA INT NOT NULL,
    IDPRODUTO INT NOT NULL,
    QUANTIDADE INT NOT NULL,
    VALOR_UNITARIO DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_ITEM_CONTA
        FOREIGN KEY (IDCONTA)
        REFERENCES CONTAS(ID),

    CONSTRAINT FK_ITEM_PRODUTO
        FOREIGN KEY (IDPRODUTO)
        REFERENCES PRODUTOS(ID)
);


-- ============================================================
-- PAGAMENTOS
-- ============================================================

CREATE TABLE PAGAMENTOS (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    IDCONTA INT NOT NULL,
    VALOR DECIMAL(10,2) NOT NULL,
    FORMA_PAGAMENTO VARCHAR(30) NOT NULL,
    DATA TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT FK_PAGAMENTO_CONTA
        FOREIGN KEY (IDCONTA)
        REFERENCES CONTAS(ID)
);


-- ============================================================
-- DADOS DE TESTE - CONTAS
-- ============================================================

-- Conta aberta na MESA 2
INSERT INTO CONTAS
    (IDMESA, STATUS)
VALUES
    (2, 'ABERTA');


-- Conta aberta na MESA 5
INSERT INTO CONTAS
    (IDMESA, STATUS)
VALUES
    (5, 'ABERTA');


-- Conta fechada da MESA 1
INSERT INTO CONTAS
    (IDMESA, STATUS, ABERTA, FECHADA)
VALUES
    (
        1,
        'FECHADA',
        DATE_SUB(NOW(), INTERVAL 3 HOUR),
        DATE_SUB(NOW(), INTERVAL 1 HOUR)
    );


-- ============================================================
-- ITENS DAS CONTAS
-- ============================================================

-- CONTA 1 - MESA 2
-- 2 X-BURGER
-- 1 BATATA
-- 2 COCA COLA

INSERT INTO ITENS_CONTA
    (IDCONTA, IDPRODUTO, QUANTIDADE, VALOR_UNITARIO)
VALUES
    (1, 1, 2, 25.00),
    (1, 4, 1, 18.00),
    (1, 6, 2, 7.00);


-- CONTA 2 - MESA 5
-- 1 X-BACON
-- 1 BATATA 500G
-- 1 SUCO

INSERT INTO ITENS_CONTA
    (IDCONTA, IDPRODUTO, QUANTIDADE, VALOR_UNITARIO)
VALUES
    (2, 3, 1, 32.00),
    (2, 5, 1, 25.00),
    (2, 8, 1, 8.00);


-- CONTA 3 - MESA 1
-- CONTA JÁ FECHADA

INSERT INTO ITENS_CONTA
    (IDCONTA, IDPRODUTO, QUANTIDADE, VALOR_UNITARIO)
VALUES
    (3, 2, 2, 28.00),
    (3, 4, 1, 18.00),
    (3, 6, 2, 7.00);


-- ============================================================
-- PAGAMENTO DA CONTA FECHADA
-- ============================================================

INSERT INTO PAGAMENTOS
    (IDCONTA, VALOR, FORMA_PAGAMENTO)
VALUES
    (3, 88.00, 'PIX');


-- ============================================================
-- CONSULTAS DE TESTE
-- ============================================================


-- USUARIOS

SELECT *
FROM USUARIOS;


-- FORNECEDORES

SELECT *
FROM FORNECEDORES;


-- CATEGORIAS

SELECT *
FROM CATEGORIAS;


-- ESTOQUE COMPLETO

SELECT
    E.ID,
    E.PRODUTO,
    E.DESCRICAO,
    E.VALOR,
    E.PESO,
    E.QTD_MIN,
    E.QUANTIDADE,
    F.NOME AS FORNECEDOR,
    C.NOME AS CATEGORIA
FROM ESTOQUE E
INNER JOIN FORNECEDORES F
    ON E.IDFORNECEDOR = F.ID
INNER JOIN CATEGORIAS C
    ON E.IDCATEGORIA = C.ID;


-- PRODUTOS

SELECT
    P.ID,
    P.NOME,
    P.DESCRICAO,
    P.VALOR,
    C.NOME AS CATEGORIA
FROM PRODUTOS P
INNER JOIN CATEGORIAS C
    ON P.IDCATEGORIA = C.ID;


-- ACOMPANHAMENTOS DOS PRODUTOS

SELECT
    P.NOME AS PRODUTO,
    A.NOME AS ACOMPANHAMENTO,
    A.VALOR AS VALOR_ACOMPANHAMENTO
FROM PRODUTO_ACOMPANHAMENTOS PA
INNER JOIN PRODUTOS P
    ON PA.IDPRODUTO = P.ID
INNER JOIN PRODUTOS A
    ON PA.IDACOMPANHAMENTO = A.ID
ORDER BY P.NOME, A.NOME;


-- CONTAS

SELECT
    C.ID,
    M.NOME AS MESA,
    C.STATUS,
    C.ABERTA,
    C.FECHADA
FROM CONTAS C
INNER JOIN MESAS M
    ON C.IDMESA = M.ID
ORDER BY C.ID;


-- ITENS DAS CONTAS

SELECT
    C.ID AS CONTA,
    M.NOME AS MESA,
    P.NOME AS PRODUTO,
    IC.QUANTIDADE,
    IC.VALOR_UNITARIO,
    IC.QUANTIDADE * IC.VALOR_UNITARIO AS TOTAL
FROM ITENS_CONTA IC
INNER JOIN CONTAS C
    ON IC.IDCONTA = C.ID
INNER JOIN MESAS M
    ON C.IDMESA = M.ID
INNER JOIN PRODUTOS P
    ON IC.IDPRODUTO = P.ID
ORDER BY C.ID;


-- ============================================================
-- TOTAL DE CADA CONTA
-- ============================================================

SELECT
    C.ID AS CONTA,
    M.NOME AS MESA,
    C.STATUS,
    COALESCE(
        SUM(IC.QUANTIDADE * IC.VALOR_UNITARIO),
        0
    ) AS TOTAL
FROM CONTAS C
INNER JOIN MESAS M
    ON C.IDMESA = M.ID
LEFT JOIN ITENS_CONTA IC
    ON C.ID = IC.IDCONTA
GROUP BY
    C.ID,
    M.NOME,
    C.STATUS
ORDER BY C.ID;


-- ============================================================
-- PAGAMENTOS
-- ============================================================

SELECT
    PG.ID,
    PG.IDCONTA AS CONTA,
    M.NOME AS MESA,
    PG.VALOR,
    PG.FORMA_PAGAMENTO,
    PG.DATA
FROM PAGAMENTOS PG
INNER JOIN CONTAS C
    ON PG.IDCONTA = C.ID
INNER JOIN MESAS M
    ON C.IDMESA = M.ID;


-- ============================================================
-- VISÃO COMPLETA DA CONTA
-- ============================================================

SELECT
    C.ID AS CONTA,
    M.NOME AS MESA,
    C.STATUS,
    P.NOME AS PRODUTO,
    IC.QUANTIDADE,
    IC.VALOR_UNITARIO,
    IC.QUANTIDADE * IC.VALOR_UNITARIO AS TOTAL_ITEM
FROM CONTAS C
INNER JOIN MESAS M
    ON C.IDMESA = M.ID
INNER JOIN ITENS_CONTA IC
    ON C.ID = IC.IDCONTA
INNER JOIN PRODUTOS P
    ON IC.IDPRODUTO = P.ID
ORDER BY
    C.ID,
    IC.ID;