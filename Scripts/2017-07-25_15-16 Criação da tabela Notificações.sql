USE Um416;
CREATE TABLE Notificacoes
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Titulo varchar(100) NOT NULL,
	Mensagem varchar(max) NOT NULL,
	DataHora datetime NOT NULL,
);