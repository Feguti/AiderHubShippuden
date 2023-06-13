using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AiderHubAtual.Models
{
    [Table("voluntario")]
    public class Voluntario
    {
        [Key]
        [Column("id_voluntario")]
        public int Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }
        [Column("cpf")]
        public string Cpf { get; set; }
        [Column("endereco")]
        public string Endereco { get; set; }
        [Column("formacao")]
        public string Formacao { get; set; }
        [Column("sobre")]
        public string Sobre { get; set; }
        [Column("interesses")]
        public string Interesses { get; set; }
        //[Column("historico_eventos")]
        //public string Historico { get; set; }   
        //[Column("proximos_eventos")]
        //public string ProximosEventos { get; set; }
        [Column("telefone")]
        public string Telefone { get; set; }
        //[Column("foto_logo")]
        //public byte Foto { get; set; }
        [Column("tipo")]
        public char Tipo { get; set; }
        //[Column("usuario")]
        //public int Usuario { get; set; }

        public Voluntario() { }

        public Voluntario(int id_voluntario, string nome, DateTime data_nascimento, string cpf, string endereco, string formacao, string sobre, string interesses, /*string Historico, string ProximosEventos*/ string telefone, /*byte Foto,*/ char Tipo/* int Usuario*/)
        {

        }

    }
}
