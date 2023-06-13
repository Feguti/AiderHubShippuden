using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AiderHubAtual.Models
{
    [Table("ong")]
    public class Ong
    {
        [Key]
        [Column("id_ong")]
        public int Id { get; set; }
        [Column("razao_social")]
        public string RazaoSocial { get; set; }
        [Column("nome_fantasia")]
        public string NomeFantasia { get; set; }
        [Column("cnpj")]
        public string Cnpj { get; set; }
        [Column("assinatura_digital")]        
        public byte AssinaturaDigital { get; set; }
        [Column("endereco")]        
        public string Endereco { get; set; }
        [Column("telefone")]
        public string Telefone { get; set; }
        [Column("foto_logo")]
        public byte Foto { get; set; }
        [Column("historico_eventos")]
        public string Historico { get; set; }   
        [Column("proximos_eventos")]
        public string ProximosEventos { get; set; }
        [Column("tipo")]
        public char Tipo { get; set; }
        [Column("usuario")]
        public int Usuario { get; set; }


        public Ong() { }

        public Ong(int id_ong, string RazaoSocial, string NomeFantasia, string Cnpj, byte AssinaturaDigital, string Endereco, string Telefone, byte Foto, string Historico, string ProximosEventos, char Tipo, int Usuario)
        {

        }
    }
}