USE Um416;
CREATE TABLE Empresas
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Nome varchar(MAX) NOT NULL, 
	DataCadastro varchar(MAX) NOT NULL,
	Rg varchar(14),
	Cpf varchar(14) NOT NULL,
	TipoPessoa tinyint NOT NULL,
	Ativo tinyint NOT NULL,
	Observacoes varchar(MAX),
	Site varchar(MAX),
	TelFixo varchar(15),
	TelMovel varchar(15),
	Email varchar(MAX),
	Logradouro varchar(MAX),
	Numero varchar(MAX),
	Complemento varchar(MAX),
	Bairro varchar(MAX),
	Cidade varchar (MAX),
	Uf varchar(2),
	Cep varchar(9),
	Login varchar(30) NOT NULL,
	Senha varchar(100) NOT NULL
);