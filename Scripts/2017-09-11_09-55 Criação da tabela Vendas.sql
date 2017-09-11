USE Um416;
CREATE TABLE Vendas
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Numero bigint NOT NULL,
	DataHora datetime NOT NULL,
	Valor decimal(18,2) NOT NULL,
	QuantParcelas int NOT NULL,
	DiaVencimento int NOT NULL,
	ClienteId bigint NOT NULL CONSTRAINT FK_Clientes_Vendas FOREIGN KEY REFERENCES UsuariosClientes(Id),
	LoteId bigint NOT NULL CONSTRAINT FK_Lotes_Vendas FOREIGN KEY REFERENCES Lotes(Id)
);