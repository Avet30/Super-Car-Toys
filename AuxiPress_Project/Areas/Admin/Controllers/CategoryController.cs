
using AuxiPress.DAL;
using AuxiPress.DAL.Repository.IRepository;
using AuxiPress.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuxiPress_Project.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //Grâce a l'injection de dépendance, je ne dois pas créer un objet de la classe AppDbContext et l'initialiser!

        public CategoryController(IUnitOfWork unitOfWork) //Je demande au constructeur d'utiliser mon UnitOfWork pour accéder a la database
        {
            _unitOfWork = unitOfWork; //J'ai maintenant mon UnitOfWork avec l'injection de dépendance
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll(); //j'accède a ma Db via _db je récupère mes Categories que je place dans ma IEnumerable objCategoryList!!
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View(); //Je laisse mon Create Vide car et je vais créer mon modèle directement dans ma vue!
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Un token qui permet de bloquer des attaques sur
                                   //les formulaires en injectant une clé qui sera valider ici
        public IActionResult Create(Category obj) //Il recevra en paramètre un objet de type Category
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order should be different from the Name"); //Erreur Customisé
            }
            if(ModelState.IsValid) //Validation coté serveur avec l'outil intégré ModelState.IsValid (Notre name est Obligatoire !)
            {
                _unitOfWork.Category.Add(obj); //J'utilise ma méthode Add de IUnitOfWork
                _unitOfWork.Save(); //Cela va sauvegarder les données dans ma base de donnée SQL
                TempData["Success"] = "Category Created Succesfully"; /*Je crée un petit Message style Alerte avec un TempData!*/
                return RedirectToAction("Index"); //Je redirige directement sur ma mon Index qui affiche mon tableau
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
            //var categoryFromDb = _db.Categories.Find(id); 
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id); //En parametre de ma méthode GetFirstOrDefault j'ai spécifié qu'il accpete les fonctions fléchés

            if(categoryFromDbFirst == null) //Je check qu'il soit pas nul
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Un token qui permet de bloquer des attaques sur
                                   //les formulaires en injectant une clé qui sera validee ici
        public IActionResult Edit(Category obj) //Il recevra en paramètre un objet de type Category
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order should be different from the Name"); //Erreur Customisé
            }
            if (ModelState.IsValid) //Validation coté serveur avec l'outil intégré ModelState.IsValid (Notre name est Obligatoire !)
            {
                _unitOfWork.Category.Update(obj); //Grâce au Update déjà mis en place via Entity Framework je met à jour ma DB!!
                _unitOfWork.Save(); //Cela va sauvegarder les données dans ma base de donnée SQL
                TempData["Success"] = "Category Updated Succesfully";
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
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null) //Je check qu'il soit pas nul
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Un token qui permet de bloquer des attaques sur
                                   //les formulaires en injectant une clé qui sera valider ici
        public IActionResult DeletePost(int? id) //Il recevra en paramètre un objet de type Category
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                NotFound();
            }

            _unitOfWork.Category.Remove(obj); //Grâce au Update déjà mis en place via Entity Framework je met à jour ma DB!!
            _unitOfWork.Save(); //Cela va sauvegarder les données dans ma base de donnée SQL
            TempData["Success"] = "Category Deleted Succesfully";
            return RedirectToAction("Index"); //Je redirige directement sur ma mon Index qui affiche mon tableau
            
        }
    }
}

