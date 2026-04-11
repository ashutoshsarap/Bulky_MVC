using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Company)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }   
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll(null,"").ToList();
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = 

            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            
            if(id==null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id,"");
                return View(companyObj);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {

            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "Name and Display Order cannot be same");
            //}
            //if (obj.Name!=null && obj.Name.ToLower()=="test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);

                }
                else
                { 
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            { 
                return View(CompanyObj);
            }
        }

        //public IActionResult Upsert(int? id)
        //{
        //    CompanyVM CompanyVM = new()
        //    {
        //        CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString()
        //        })
        //    };
        //    if(id==null || id==0)
        //    {
        //        return View(CompanyVM);
        //    }
        //    else
        //    {
        //        CompanyVM.Company = _unitOfWork.Company.Get(u => u.Id==u.Id);
        //        return View(CompanyVM);
        //    }
        //}

        //[HttpPost]
        //public IActionResult Upsert(CompanyVM CompanyVM, IFormFile? file)
        //{

        //    //if (obj.Name == obj.DisplayOrder.ToString())
        //    //{
        //    //    ModelState.AddModelError("Name", "Name and Display Order cannot be same");
        //    //}
        //    //if (obj.Name!=null && obj.Name.ToLower()=="test")
        //    //{ 
        //    //    ModelState.AddModelError("", "Test is an invalid value");
        //    //}

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Update(CompanyVM.Company);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Company updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id,"");
        //    //Company? CompanyFromDb1 = CompanyRepo.Categories.FirstOrDefault(u => u.Id == id);
        //    //Company? CompanyFromDb2 = CompanyRepo.Categories.Where(u => u.Id == id).FirstOrDefault();
        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromDb);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{

        //    Company? obj = _unitOfWork.Company.Get(u => u.Id == id,"");
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Company deleted successfully";
        //    return RedirectToAction("Index");

        //}

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var objCompanyList = _unitOfWork.Company.GetAll(null,"").ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id==id, "");

            if(CompanyToBeDeleted==null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion
    }
}
