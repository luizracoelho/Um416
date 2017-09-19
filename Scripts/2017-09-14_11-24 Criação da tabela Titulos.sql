USE Um416;
CREATE TABLE Titulos
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Numero int NOT NULL,
	Valor decimal(18, 2) NOT NULL,
	DataVencimento date NOT NULL,
	Pago bit NOT NULL,
	DataPgto datetime NULL,
	ValorPgto decimal(18, 2) NULL,
	VendaId bigint NOT NULL CONSTRAINT FK_Vendas_Titulos FOREIGN KEY REFERENCES Vendas(Id) ON DELETE CASCADE
);