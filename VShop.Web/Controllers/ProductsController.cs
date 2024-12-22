using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers
{

    [Authorize(Roles = Role.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var result = await _productService.GetAllProducts(await GetBearerTokenInRequest());

            if (result is null)
                return View("Error");

            return View(result);
        }

        private async Task<string?> GetBearerTokenInRequest()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.categoryid = new SelectList(await
                 _categoryService.GetAllCategories(await GetBearerTokenInRequest()), "categoryid", "name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productVM, await GetBearerTokenInRequest());
                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.categoryid = new SelectList(await
                 _categoryService.GetAllCategories(await GetBearerTokenInRequest()), "categoryid", "name");
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.categoryid = new SelectList(await _categoryService.GetAllCategories(await GetBearerTokenInRequest()), "categoryid", "name");
            var result = await _productService.FindProductById(id, await GetBearerTokenInRequest());
            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(productVM, await GetBearerTokenInRequest());
                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
        {
            var result = await _productService.FindProductById(id, await GetBearerTokenInRequest());
            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<IActionResult> DeletConfirmed(int id)
        {
            var result = await _productService.DeleteProductById(id, await GetBearerTokenInRequest());
            if (!result)
                return View("Error");
            return RedirectToAction("Index");
        }
    }
}
