using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AiderHubAtual.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AiderHubAtual.Controllers
{
    public class EventosController : Controller
    {
        private readonly Context _context;

        public EventosController(Context context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }


        //public async Task<IActionResult> IndexOng()
        //{
        //    int idUser = HttpContext.Session.GetInt32("IdUser") ?? 0;

        //    // Filtrar as inscrições pelo valor de idVoluntario
        //    var eventos = await _context.Eventos
        //        .Where(e => e./*idOng*/ == idUser)
        //        .ToListAsync();

        //    return View(eventos);
        //}
        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id_Evento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Evento,data_Hora,Endereco,Carga_Horario,Descricao,Responsavel")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Evento,data_Hora,Endereco,Carga_Horario,Descricao,Responsavel")] Evento evento)
        {
            if (id != evento.Id_Evento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id_Evento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id_Evento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id_Evento == id);
        }

        public async Task<IActionResult> Inscrever(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id_Evento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return RedirectToAction("InscreverConfirmed", new { id });
        }

        public async Task<IActionResult> InscreverConfirmed(int id)
        {
            // Use os valores recebidos para criar a inscrição
            //var idUser = (int)ViewData["IdUser"];
            int idUser = HttpContext.Session.GetInt32("IdUser") ?? 0;

            var existingInscricao = await _context.Inscricoes
            .FirstOrDefaultAsync(i => i.idEvento == id && i.idVoluntario == idUser);

            if (existingInscricao != null)
            {
                // Já existe uma inscrição com os mesmos valores, faça o tratamento necessário
                ViewBag.Mensagem = "Você já está inscrito.";
                return View("Inscricao"); // Redireciona para a página desejada
            }

            Inscricao inscricao = new Inscricao
            {
                idEvento = id,
                idVoluntario = idUser,
                Status = true,
                Tipo = "V",
                Confirmacao = false,
                DataInscricao = DateTime.Today
            };
            var inscricaoController = new InscricoesController(_context);
            await inscricaoController.Create(inscricao);
            // Faça o processamento necessário com a inscrição

            ViewBag.Mensagem = "Inscrição Confirmada";
          
            return View("Inscricao"); 
        }
        //public void CriarTabelaInscricoes(int idEvento)
        //{
        //    // Caminho do arquivo .xlsm
        //    string nomeArquivo = "MacroCertificado.xlsm";
        //    string diretorioAtual = AppDomain.CurrentDomain.BaseDirectory;
        //    string caminho = Path.Combine(diretorioAtual, "Relatorio", nomeArquivo);

        //    // Criar uma instância do aplicativo Excel
        //    var xlApp = new Excel.Application();

        //    // Abrir o arquivo .xlsm
        //    var xlWorkbook = xlApp.Workbooks.Open(caminho);

        //    // Selecionar a planilha onde a tabela será criada (por exemplo, planilha "Dados")
        //    var xlWorksheet = (Excel.Worksheet)xlWorkbook.Sheets["Dados"];

        //    // Obter os dados da tabela de inscrições para o evento específico
        //    List<Inscricao> inscricoes = ObterInscricoesPorEvento(idEvento);

        //    // Definir a célula inicial para a tabela
        //    int startRow = 2; // Começando na linha 2 (exemplo)

        //    // Preencher os dados da tabela
        //    for (int i = 0; i < inscricoes.Count; i++)
        //    {
        //        var inscricao = inscricoes[i];

        //        // Preencher as células com os dados da inscrição
        //        xlWorksheet.Cells[startRow + i, 1] = inscricao.IdEvento;
        //        xlWorksheet.Cells[startRow + i, 2] = inscricao.IdVoluntario;
        //    }

        //    // Salvar as alterações no arquivo .xlsm
        //    xlWorkbook.Save();

        //    // Fechar o arquivo e liberar os recursos
        //    xlWorkbook.Close();
        //    xlApp.Quit();
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
        //}

        //public List<InscricaoData> ObterInscricoesPorEvento(int idEvento)
        //{
        //    List<InscricaoData> inscricoes = new List<InscricaoData>();

        //    // Recuperar os dados do evento com base no IdEvento
        //    var evento = _context.Eventos.FirstOrDefault(e => e.Id_Evento == idEvento);
        //    if (evento != null)
        //    {
        //        // Recuperar os dados das inscrições para o evento
        //        var inscricoesEvento = _context.Inscricoes.Where(i => i.idEvento == idEvento).ToList();

        //        foreach (var inscricaoEvento in inscricoesEvento)
        //        {
        //            // Recuperar o nome do voluntário com base no IdVoluntario
        //            var voluntario = _context.Voluntarios.FirstOrDefault(v => v.Id == inscricaoEvento.idVoluntario);
        //            if (voluntario != null)
        //            {
        //                // Criar um objeto Inscricao com os dados recuperados
        //                var inscricao = new InscricaoData
        //                {
        //                    idEvento = idEvento,
        //                    idVoluntario = inscricaoEvento.idVoluntario,
        //                    NomeVoluntario = voluntario.Nome,
        //                    CargaHoraria = evento.Carga_Horario,
        //                    DataEvento = evento.data_Hora,
        //                    //Ong = evento.idOng falta adicionar no bdd um id_ong
        //                };

        //                // Adicionar a inscrição à lista
        //                inscricoes.Add(inscricao);
        //            }
        //        }
        //    }

        //    return inscricoes;
        //}


    }
}
