CREATE DATABASE startup;

CREATE TABLE Cliente (
  id_cliente INT PRIMARY KEY IDENTITY,
  nome VARCHAR(100) NOT NULL,
  cpf_cnpj VARCHAR(14) UNIQUE,
  telefone VARCHAR(20)NOT NULL,
  endereco VARCHAR(150) UNIQUE,
  tipo_cliente CHAR(2)
);

SELECT * FROM Cliente;

CREATE TABLE Entregador (
  id_entregador INT PRIMARY KEY IDENTITY,
  nome VARCHAR(100) NOT NULL,
  cpf VARCHAR(11) UNIQUE,
  veiculo VARCHAR(50) NOT NULL,
  placa VARCHAR(10)
);

SELECT * FROM Entregador;

CREATE TABLE Pedido (
  id_pedido INT PRIMARY KEY IDENTITY,
  data_pedido DATE,
  status_atual VARCHAR(20),
  valor_total DECIMAL(10,2),
  id_cliente INT,
  id_entregador INT,
  FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
  FOREIGN KEY (id_entregador) REFERENCES Entregador(id_entregador)
);

SELECT * FROM Pedido;

CREATE TABLE Produto (
  id_produto INT PRIMARY KEY IDENTITY,
  nome VARCHAR(100) NOT NULL,
  descricao TEXT NOT NULL,
  categoria VARCHAR(50) NOT NULL,
  preco_atual FLOAT NOT NULL
);

SELECT * FROM Produto;

CREATE TABLE Itens_Pedido (
  id_pedido INT,
  id_produto INT,
  quantidade INT,
  preco_unitario DECIMAL(10,2),
  PRIMARY KEY (id_pedido, id_produto),
  FOREIGN KEY (id_pedido) REFERENCES Pedido(id_pedido),
  FOREIGN KEY (id_produto) REFERENCES Produto(id_produto)
);

SELECT * FROM Itens_Pedido;

CREATE TABLE Rastreamento (
  id_rastreamento INT PRIMARY KEY IDENTITY,
  data_hora DATETIME,
  status VARCHAR(20),
  localizacao VARCHAR(100),
  id_pedido INT,
  FOREIGN KEY (id_pedido) REFERENCES Pedido(id_pedido)
);

SELECT * FROM Rastreamento;
