
using AuxiPress.DAL;
using AuxiPress.DAL.Repository.IRepository;
using AuxiPress.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuxiPress_Project.Controllers
{
    [Area("Admin")]
    public class CarTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; 
        public CarTypeController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork; 
        }

        public IActionResult Index()
        {
            IEnumerable<CarType> objCarTypeList = _unitOfWork.CarType.GetAll(); 
            return View(objCarTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View(); 
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(CarType obj) 
        {
            if(ModelState.IsValid) 
            {
                _unitOfWork.CarType.Add(obj); 
                _unitOfWork.Save(); 
                TempData["Success"] = "CarType Created Succesfully"; 
                return RedirectToAction("Index"); 
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            
            var carTypeFromDbFirst = _unitOfWork.CarType.GetFirstOrDefault(u=>u.Id==id); 

            if(carTypeFromDbFirst == null) 
            {
                return NotFound();
            }
            return View(carTypeFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] 
                                   
        public IActionResult Edit(CarType obj) 
        {

            if (ModelState.IsValid) 
            {
                _unitOfWork.CarType.Update(obj); 
                _unitOfWork.Save(); 
                TempData["Success"] = "CarType Updated Succesfully";
                return RedirectToAction("Index"); //Je redirige directement sur ma mon Index qui affiche mon tableau
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
           
            var carTypeFromDbFirst = _unitOfWork.CarType.GetFirstOrDefault(u => u.Id == id);

            if (carTypeFromDbFirst == null) 
            {
                return NotFound();
            }
            return View(carTypeFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] 
                                   
        public IActionResult DeletePost(int? id) 
        {
            var obj = _unitOfWork.CarType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                NotFound();
            }

            _unitOfWork.CarType.Remove(obj); 
            _unitOfWork.Save(); 
            TempData["Success"] = "CarType Deleted Succesfully";
            return RedirectToAction("Index"); 
            
        }
    }
}

