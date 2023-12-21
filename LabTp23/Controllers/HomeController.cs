using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LabTp23.Models;
using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Services.Interfaces;

namespace LabTp23.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICartService _cartService;

    public HomeController(
        ILogger<HomeController> logger,
        IProductRepository productRepository,
        IPurchaseRepository purchaseRepository,
        ICartRepository cartRepository,
        ICartService cartService)
    {
        _logger = logger;
        _productRepository = productRepository;
        _purchaseRepository = purchaseRepository;
        _cartRepository = cartRepository;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetProductsAsync();
        var cart = await _cartRepository.GetAsync();
        ViewBag.Message = null;
        ViewBag.Products = products;
        ViewBag.Cart = cart;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Index(Guid productId)
    {
        var added = await _cartService.AddProdcutToCart(productId);

        if (added)
        {
            ViewBag.ProductAdded = true;
            ViewBag.Message = "Product added";
        }
        else
        {
            ViewBag.ProductAdded = false;
            ViewBag.Message = "Product already in cart";
        }

        var products = await _productRepository.GetProductsAsync();
        var cart = await _cartRepository.GetAsync();
        ViewBag.Products = products;
        ViewBag.Cart = cart;
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Cart()
    {
        var cart = await _cartRepository.GetAsync();
        ViewBag.Cart = cart;

        return View();
    }

    [HttpGet]
    public ActionResult Buy()
    {
        return View();
    }

    [HttpPost]
    public async Task<string> Buy(Purchase purchase)
    {
        purchase.Date = DateTime.Now;
        await _cartService.BuyProdcutsInCart(purchase);
        return "Спасибо за покупку, " + purchase.Person + "!";
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
