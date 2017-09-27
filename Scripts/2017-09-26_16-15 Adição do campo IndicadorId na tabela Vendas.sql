USE Um416;
Alter Table Vendas
Add IndicadorId bigint NULL CONSTRAINT FK_Indicador_Vendas FOREIGN KEY REFERENCES UsuariosClientes(Id);