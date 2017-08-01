USE Um416;
CREATE TABLE LeiturasNotificacoes
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	ClienteId bigint NOT NULL FOREIGN KEY REFERENCES UsuariosClientes(Id),
	NotificacaoId bigint NOT NULL CONSTRAINT FK_Notificacoes_LeiturasNotificacoes FOREIGN KEY REFERENCES Notificacoes(Id) ON DELETE CASCADE,
	DataHoraLeitura datetime,
	Lida smallint NOT NULL
);