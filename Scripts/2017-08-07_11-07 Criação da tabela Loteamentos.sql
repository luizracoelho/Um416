USE Um416;
CREATE TABLE Loteamentos
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Nome varchar(100) NOT NULL,
	Descricao varchar(max) NOT NULL,
	DataCadastro datetime NOT NULL,
	Logradouro varchar(100),
	Numero varchar(10),
	Bairro varchar(100),
	Cidade varchar(100),
	Uf varchar(2),
	Cep varchar(10),
	MapaId bigint CONSTRAINT FK_Loteamentos_Imagens FOREIGN KEY REFERENCES Imagens(Id),
	EmpresaId bigint CONSTRAINT FK_Loteamentos_Emrpesas FOREIGN KEY REFERENCES Empresas(Id) NOT NULL
);