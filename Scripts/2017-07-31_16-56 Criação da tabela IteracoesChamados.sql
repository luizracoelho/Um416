USE Um416;
CREATE TABLE IteracoesChamados
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Conteudo varchar(max) NOT NULL,
	DataHora datetime NOT NULL,
	AdminId bigint CONSTRAINT FK_UsuariosAdmins_IteracoesChamados FOREIGN KEY REFERENCES UsuariosAdmins(Id),
	ChamadoId bigint NOT NULL CONSTRAINT FK_Chamados_IteracoesChamados FOREIGN KEY REFERENCES Chamados(Id) ON DELETE CASCADE
);