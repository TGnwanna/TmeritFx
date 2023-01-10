using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TmeritFX.DataBase_Connection;
using TmeritFX.Enum;
using TmeritFX.IHelper;
using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IUserHelper _userHelper;
        private readonly IAdminHelper _adminHelper;

        public AdminController(AppDbContext db, IUserHelper userHelper, IAdminHelper adminHelper)
        {
            _db = db;
            _userHelper = userHelper;
            _adminHelper = adminHelper;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Investment()
        {
            var investments = _adminHelper.GetPendingInvestmentList();
            return View(investments);
        }
        public IActionResult Clients()
        {
            var clients = _userHelper.GetAllClientsList();
            return View(clients);
        }

        // POST ACTION FOR CLIENT ACTIVATION AND DEACTIVATION
        [HttpPost]
        public JsonResult ClientTurnOnAndOff(string collectedClientData)
        {
            try
            {
                if (collectedClientData != null)
                {
                    var clientAccount = JsonConvert.DeserializeObject<UserViewModel>(collectedClientData);
                    if (clientAccount.ActionType == GeneralAction.ACTIVATE)
                    {
                        if (clientAccount.Id != null)
                        {
                            var clientToActivate = _adminHelper.ActivateClientAccount(clientAccount);
                            if (clientToActivate != null)
                            {
                                return Json(new { isError = false, msg = "Activated Successfully" });
                            }
                        }

                    }
                    else if (clientAccount.ActionType == GeneralAction.DEACTIVATE)
                    {
                        if (clientAccount.Id != null)
                        {
                            var clientToDectivate = _adminHelper.DeactivateClientAccount(clientAccount);
                            if (clientToDectivate != null)
                            {
                                return Json(new { isError = false, msg = "Deactivated Successfully" });
                            }
                        }
                    }
                }
                return Json(new { isError = true, msg = "Failed" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
