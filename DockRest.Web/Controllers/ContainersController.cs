using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DockRest.Web.Models;
using DockRest.Core;
using DockRest.Core.Models;

namespace DockRest.Web.Controllers;

public class ContainersController : Controller
{
    private readonly ILogger<ContainersController> logger;
    private readonly DockerService dockerService;

    public ContainersController(ILogger<ContainersController> logger, DockerService dockerService)
    {
        this.logger = logger;
        this.dockerService = dockerService;
    }
    public IActionResult Index()
    {
        IEnumerable<Container> containers = dockerService.GetContainers().Result;
        return View(containers);
    }
    [HttpPost]
    public IActionResult StartContainer(string id)
    {
        dockerService.Start(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult StopContainer(string id)
    {
        dockerService.Stop(id);
        return RedirectToAction("Index");
    }
}
