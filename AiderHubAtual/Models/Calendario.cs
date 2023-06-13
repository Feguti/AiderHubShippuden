using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AiderHubAtual.Models
{
    [Table("calendario")]
    public class Calendario
    {  
        [Key]
        [Column("id_calendario")]
        public int Id { get; set; }
        [Column("nome_evento")]
        public string Nome { get; set; }
        [Column("data_hora")]
        public DateTime DataHora { get; set; }
        [Column("carga_horaria")]
        public TimeSpan CargaHoraria { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("cep")]
        public string Cep { get; set; }
        [Column("endereco")]
        public string Endereco { get; set; }
        [Column("logradouro")]
        public int Logradouro { get; set; }
        [Column("complemento")]
        public string Complemento { get; set; }
        [Column("bairro")]
        public string Bairro { get; set; }
        [Column("cidade")]
        public string Cidade { get; set; }
        [Column("uf")]
        public char Uf { get; set; }
        [Column("eventos_marcados")]
        public TimeSpan EventosMarcados { get; set; }

        public Calendario(){ }

        public Calendario(int Id, string Nome, DateTime DataHora, TimeSpan CargaHoraria, string Descricao, string Cep, string Endereco, int Logradouro, string Complemento, string Bairro, string Cidade, char Uf, TimeSpan EventosMarcados)
        {

        }

    }
}