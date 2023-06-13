using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AiderHubAtual.Models
{
    [Table("relatorio")]
    public class Relatorio
    {
        [Key]
        [Column("id_relatorio")]
        public int Id { get; set; }
        [Column("foto_logo")]
        public byte Foto { get; set; }
        [Column("id_voluntario")]
        public int idVoluntario { get; set; }
        [Column("id_evento")]
        public int idEvento { get; set; }
        [Column("relatorio_gerado")]
        public bool RelatorioGerado { get; set; }
        [Column("data_geracao")]
        public TimeSpan DataGeracao { get; set; }

        public Relatorio() { }

        public Relatorio(int Id, byte Foto, int idVoluntario, int idEvento, bool RelatorioGerado, TimeSpan DataGeracao)
        {

        }
    }
}