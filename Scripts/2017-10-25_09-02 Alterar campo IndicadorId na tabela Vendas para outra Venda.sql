USE Um416;
Alter Table Vendas
	Drop Constraint FK_Indicador_Vendas;
Alter Table Vendas
	Drop Column IndicadorId;	
Alter Table Vendas	
	Add Column IndicadorId bigint NULL CONSTRAINT FK_Indicador_Vendas FOREIGN KEY REFERENCES Vendas(Id);