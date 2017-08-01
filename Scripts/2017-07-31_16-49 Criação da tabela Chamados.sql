USE Um416;
CREATE TABLE Chamados
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Titulo varchar(max) NOT NULL,
	DataHoraCriacao datetime NOT NULL,
	DataHoraEncerramento datetime,
	TipoEncerramento smallint,
	Encerrada tinyint NOT NULL default 0,
	AdminEncerramentoId bigint CONSTRAINT FK_UsuariosAdmins_Chamados FOREIGN KEY REFERENCES UsuariosAdmins(Id),
	ClienteId bigint NOT NULL CONSTRAINT FK_UsuariosClientes_Chamados FOREIGN KEY REFERENCES UsuariosClientes(Id)
);