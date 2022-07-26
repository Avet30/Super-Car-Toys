
using AuxiPress.DAL;
using AuxiPress.DAL.Repository.IRepository;
using AuxiPress.Models;
using AuxiPress.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuxiPress_Project.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET

        public IActionResult Upsert(int? id) //Une seule méthode pour Update & Insert
        {
            ProductViewModel productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

                CarTypeList = _unitOfWork.CarType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };

            if(id != null && id > 0)
            {
                //Update product
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                

            }
            return View(productVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] 
                                   
        public IActionResult Upsert(ProductViewModel obj, IFormFile? file) //J'ai rajouté IFormeFile qui me permet de recevoir en paramètre un upload de fichier
        {

            if (ModelState.IsValid) 
            {
                string wwwRoothPath = _hostEnvironment.WebRootPath; //Je récupère la route du wwwroot
                if(file != null) //je vérifie si il est nul
                {
                    string fileName = Guid.NewGuid().ToString(); //Je génère un nv nom de fichier afin d'éviter des soucis de fichiers portant le même nom!
                    var uploads = Path.Combine(wwwRoothPath, @"lib\");
                    var extension = Path.GetExtension(file.FileName); //je garde la même extension!

                    //Je m'assure d'effacer l'ancienne image si elle existe(dans le cas d'un update!)
                    if(obj.Product.ImageURL != null)
                    {
                        var oldImagePath = Path.Combine(wwwRoothPath, obj.Product.ImageURL.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageURL = @"lib\" + fileName + extension; //Je le rajoute dans DB
                }

                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }

                _unitOfWork.Save(); 
                TempData["Success"] = "Product Created Succesfully";
                return RedirectToAction("Index"); //Je redirige directement sur ma mon Index qui affiche mon tableau
            }
            return View(obj);
        }



        //API EndPoint
        //GET
        #region API Calls 
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CarType");
            return Json(new {data = productList});
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion

    }


}

