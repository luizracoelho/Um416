﻿using System;
using System.Collections.Generic;
using Um416.Base;

namespace Um416
{
    public class Loteamento : BaseClass
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public Imagem Mapa { get; set; }
        public long? MapaId { get; set; }
        public IList<Lote> Lotes { get; set; }
        public UsuarioEmpresa Empresa { get; set; }
        public long EmpresaId { get; set; }
        public string Url { get; set; }
        public string NomeHashtag { get; set; }
        public string Site { get; set; }
        public int IndicadorMultinivel { get; set; }
        public int QuantParcelas { get; set; }
    }
}
