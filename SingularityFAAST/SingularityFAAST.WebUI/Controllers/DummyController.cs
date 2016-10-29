using System.Web.Mvc;
using SingularityFAAST.Services.Services;

namespace SingularityFAAST.WebUI.Controllers
{
    public class DummyController : Controller
    {
        // GET: Dummy
        public ActionResult Index()
        {
            var services = new DummyServices();

            var model = services.GetAllDummies();

            return View(model);
        }
    }
}