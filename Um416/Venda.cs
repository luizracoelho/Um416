﻿using System;
using System.Collections.Generic;
using Um416.Base;

namespace Um416
{
    public class Venda : BaseClass
    {
        public long Numero { get; set; }
        public DateTime DataHora { get; set; }
        public decimal Valor { get; set; }
        public int QuantParcelas { get; set; }
        public int DiaVencimento { get; set; }
        public long ClienteId { get; set; }
        public UsuarioCliente Cliente { get; set; }
        public long LoteId { get; set; }
        public Lote Lote { get; set; }
        public decimal ValorParcela { get; set; }
        public int Pagas { get; set; }
        public long? IndicadorId { get; set; }
        public Lote Indicador { get; set; }
        public virtual IList<Venda> VendasIndicadas { get; set; }
        //Campos que informar se a venda está válida para a indicação e descontos, caso não, preenche-se uma mensagem para o usuário
        public bool Valida { get; set; }
        public string Mensagem { get; set; }
        public int Vencidas { get; set; }
    }
}
