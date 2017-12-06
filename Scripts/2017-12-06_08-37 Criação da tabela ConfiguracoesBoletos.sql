USE Um416;
CREATE TABLE ConfiguracoesBoletos
(
	Id bigint NOT NULL IDENTITY PRIMARY KEY,
	CodigoEmpresa varchar(26) NOT NULL,
	Chave varchar(16) NOT NULL,
	UrlRetorna varchar(60) NOT NULL,
	Observacao1 varchar(60) NOT NULL,
	Observacao2 varchar(60)  NOT NULL,
	Observacao3 varchar(60) NOT NULL
);