USE Um416;
CREATE TABLE Lotes
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Codigo varchar(100) NOT NULL,
	Descricao varchar(max) NOT NULL,
	Area decimal(18, 2) NOT NULL,
	Valor decimal(18, 2) NOT NULL,
	TipoLote smallint NOT NULL,
	Logradouro varchar(100),
	Numero varchar(10),
	Complemento varchar(50),
	Bairro varchar(100),
	Cidade varchar(100),
	Uf varchar(2),
	Cep varchar(10),
	LoteamentoId bigint CONSTRAINT FK_Lotemanentos_Lotes FOREIGN KEY REFERENCES Loteamentos(Id) ON DELETE CASCADE
);