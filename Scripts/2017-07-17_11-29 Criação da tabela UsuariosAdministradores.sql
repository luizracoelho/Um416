USE Um416;
CREATE TABLE UsuariosAdmins
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	Nome varchar(100) NOT NULL,
	Cpf varchar(19) NOT NULL,
	Email varchar(100) NOT NULL,
	TelFixo varchar(15),
	TelMovel varchar(15),
	Login varchar(30) NOT NULL,
	Senha varchar(100) NOT NULL
);