USE Um416;
CREATE TABLE UsuariosVendedores
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Nome varchar(100) NOT NULL,
	Cpf varchar(19) NOT NULL,
	Email varchar(100) NOT NULL,
	TelFixo varchar(15),
	TelMovel varchar(15),
	Login varchar(30) NOT NULL,
	Senha varchar(100) NOT NULL,
	DataCadastro datetime NOT NULL,
	DataNascimento datetime,
	Sexo smallint,
	Rg varchar(30),
	Logradouro varchar(100),
	Numero varchar(10),
	Complemento varchar(50),
	Bairro varchar(100),
	Cidade varchar(100),
	Uf varchar(2),
	Cep varchar(10),
	PercentualComissao decimal(16,4) NOT NULL
);