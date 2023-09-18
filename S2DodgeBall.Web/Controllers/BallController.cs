using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using S2DodgeBall.Web.Models;

namespace S2DodgeBall.Web.Controllers;

public class BallController : Controller
{
    private readonly string _viewModelPath = "data.json";

    // GET
    public IActionResult Index()
    {
        BallIndexViewModel viewModel;

        if (!System.IO.File.Exists(_viewModelPath))
        {
            viewModel = new BallIndexViewModel();
        }
        else
        {
            string json = System.IO.File.ReadAllText(_viewModelPath);
            BallIndexViewModel? deserialized = JsonSerializer.Deserialize<BallIndexViewModel>(json);
            viewModel = deserialized ?? throw new InvalidOperationException("This should not happen. Back to Timo!");
        }

        return View(viewModel);
    }

    // POST
    [HttpPost]
    public IActionResult Index(BallIndexViewModel viewModel)
    {
        Random random = new Random();
        bool doesCpuThrowLeft = random.Next(0, 2) == 0;

        if (doesCpuThrowLeft && viewModel.Direction == "left")
        {
            viewModel.HealthPoints--;
            viewModel.GotHit = true;
        }
        else
        {
            viewModel.DidDodge = true;
        }

        using StreamWriter writer = new StreamWriter(_viewModelPath);
        string json = JsonSerializer.Serialize(viewModel);
        writer.Write(json);

        return View(viewModel);
    }
}