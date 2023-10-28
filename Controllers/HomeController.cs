using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SessionWorkshop.Models;
using System;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SetName(UserModel user)
    {
        // Guarda el nombre en la sesión
        HttpContext.Session.SetString("UserName", user.UserName);
        HttpContext.Session.SetInt32("Value", 22); // Inicializa el valor en la sesión
        return RedirectToAction("Game");
    }

    [HttpPost]
    public IActionResult UpdateValue(int increment)
    {
        // Obtén el valor actual de la sesión
        int currentValue = HttpContext.Session.GetInt32("Value") ?? 0;

        // Realiza la operación según el incremento
        switch (increment)
        {
            case 1:
                currentValue += 1;
                break;
            case -1:
                currentValue -= 1;
                break;
            case 2:
                currentValue *= 2;
                break;
            case 0:
                // Generar un número aleatorio entre 1 y 10 (incluyendo 10)
                Random random = new Random();
                int randomIncrement = random.Next(1, 11);
                currentValue += randomIncrement;
                break;
        }

        // Guarda el nuevo valor en la sesión
        HttpContext.Session.SetInt32("Value", currentValue);

        return RedirectToAction("Game");
    }

    public IActionResult Game()
    {
        // Recupera el nombre de la sesión
        string userName = HttpContext.Session.GetString("UserName");

        // Recupera el valor de la sesión
        int value = HttpContext.Session.GetInt32("Value") ?? 22;

        // Crea una instancia del modelo para pasar datos a la vista
        UserModel user = new UserModel { UserName = userName, Value = value };

        return View("Game", user);
    }
}
