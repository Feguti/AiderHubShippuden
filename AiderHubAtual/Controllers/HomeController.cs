using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace AiderHubAtual.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult Privacy()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        private readonly OpenStreetMapService _openStreetMapService;

        public HomeController()
        {
            _openStreetMapService = new OpenStreetMapService();
        }

        public ActionResult Endereco(string address, string deviceLatitude, string deviceLongitude)
        {
            Coordinates coordinates = _openStreetMapService.GetCoordinates(address);

            ViewBag.Address = address;

            if (coordinates != null)
            {
                ViewBag.Latitude = coordinates.Latitude;
                ViewBag.Longitude = coordinates.Longitude;

                return RedirectToAction("Resultado", new { databaseLatitude = ViewBag.Latitude, databaseLongitude = ViewBag.Longitude,
                    deviceLatitude, deviceLongitude});
                //return View();
               // return RedirectToAction("Device", new { databaseLat = ViewBag.Latitude, databaseLone = ViewBag.Longitude });
            }
            else
            {
                // não achou / deu erro
                ViewBag.ErrorMessage = "Address not found.";
            }
            return View();
        }

        [HttpPost]
        public ActionResult CheckIn(string address, string deviceLatitude, string deviceLongitude)
        {
            return RedirectToAction("Endereco", new { address, deviceLatitude, deviceLongitude });
        }

        [HttpGet]
        [HttpPost]
        public ActionResult Resultado(string databaseLatitude, string databaseLongitude, string deviceLatitude, string deviceLongitude)
        {

            double parsedDeviceLatitude = double.Parse(deviceLatitude, CultureInfo.InvariantCulture);
            double parsedDeviceLongitude = double.Parse(deviceLongitude, CultureInfo.InvariantCulture);
            //double deviceLatitude = -23.465585;
            //double deviceLongitude = -46.573850;

            if(string.IsNullOrEmpty(databaseLatitude) || string.IsNullOrEmpty(databaseLongitude))
            {
                return View("Eventos/Index");
            }

            double parsedDataBaselatitude = double.Parse(databaseLatitude, CultureInfo.InvariantCulture);
            double parsedDataBaselongitude = double.Parse(databaseLongitude, CultureInfo.InvariantCulture);

            double distanceInMeters = CalculateDistance(parsedDeviceLatitude, parsedDeviceLongitude, parsedDataBaselatitude, parsedDataBaselongitude);

                if (distanceInMeters <= 4000)
                {
                    ViewBag.resultado = "DENTRO DO RAIO, CHECK-IN REALIZADO COM SUCESSO!";
                    ViewBag.coordenadas = $"{parsedDeviceLatitude}, {parsedDeviceLongitude}";
                    ViewBag.distancia = distanceInMeters;

                    return RedirectToAction("Validar", new { result = ViewBag.resultado, coordinate = ViewBag.coordenadas, distance = ViewBag.distancia });
                }
                else
                {
                    ViewBag.resultado = "FORA DO RAIO, CHECK-IN INVÁLIDO!";
                    ViewBag.coordenadas = $"{parsedDeviceLatitude}, {parsedDeviceLongitude}";
                    ViewBag.distancia = distanceInMeters;

                    return RedirectToAction("Validar", new { result = ViewBag.resultado, coordinate = ViewBag.coordenadas, distance = ViewBag.distancia });
                }
        }

        public ActionResult Validar(string result, string coordinate, double distance)
        {
            ViewBag.Result = result;
            ViewBag.Coordinate = coordinate;
            ViewBag.Distance = distance;
            return View();
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadius = 6371000; // in meters

            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = EarthRadius * c;

            return distance;
        }

        /*[HttpPost]
        public ActionResult ExecutaMacro()
        {
            string nomeArquivo = "MacroCertificado.xlsm";
            string diretorioAtual = AppDomain.CurrentDomain.BaseDirectory;
            string caminho = Path.Combine(diretorioAtual, "Relatorio", nomeArquivo);

            //string caminho = "C:\\Users\\Feguti\\source\\repos\\AHub\\AiderHubAtual\\Relatorio\\MacroCertificado.xlsm";

            Application xlApp = new Application();

            if (xlApp == null)
            {
                ViewBag.Mensagem = "Erro ao executar a macro: aplicativo Excel não encontrado.";
                return View("Relatorio");
            }

            Workbook xlWorkbook = xlApp.Workbooks.Open(caminho, ReadOnly: false);

            try
            {
                xlApp.Visible = false;
                xlApp.Run("GerarCertificado");
            }
            catch (System.Exception)
            {
                ViewBag.Mensagem = "Erro ao executar a macro.";
                return View("Relatorio");
            }

            xlWorkbook.Close(false);
            xlApp.Application.Quit();
            xlApp.Quit();


            ViewBag.Mensagem = "Arquivo gerado com sucesso!";
            return View("Relatorio");
        }

        public ActionResult Relatorio()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
