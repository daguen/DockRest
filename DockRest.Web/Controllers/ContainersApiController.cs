using Microsoft.AspNetCore.Mvc;
using DockRest.Core;
using DockRest.Core.Models;

namespace DockRest.Web.Controllers;

[ApiController]
[Route("api/containers")]
public class ContainersApiController : ControllerBase
{
    private readonly ILogger<ContainersController> logger;
    private readonly DockerService dockerService;

    public ContainersApiController(ILogger<ContainersController> logger, DockerService dockerService)
    {
        this.logger = logger;
        this.dockerService = dockerService;
    }

    //GET /api/Containers
    [HttpGet]
    public IEnumerable<Container> GetContainers()
    {
        IEnumerable<Container> containers = dockerService.GetContainers().Result;
        return containers;
    }

    //GET /api/Containers/name
    [HttpGet("{name}")]
    public Container GetContainer(string name)
    {
        List<Container> containers = dockerService.GetContainers().Result;

        Container foundContainer = containers.Where(c => c.Name == name).FirstOrDefault();

        if (foundContainer == null) return null;
        return foundContainer;
    }
    //GET /api/Containers/name/start
    [HttpGet("{name}/start")]
    public Container StartContainer(string name)
    {
        List<Container> containers = dockerService.GetContainers().Result;

        Container foundContainer = containers.Where(c => c.Name == name).FirstOrDefault();

        if (foundContainer == null) return null;

        dockerService.Start(foundContainer.Id);
        return foundContainer;
    }
    //GET /api/Containers/name/stop
    [HttpGet("{name}/stop")]
    public Container StopContainer(string name)
    {
        List<Container> containers = dockerService.GetContainers().Result;

        Container foundContainer = containers.Where(c => c.Name == name).FirstOrDefault();

        if (foundContainer == null) return null;

        dockerService.Stop(foundContainer.Id);
        return foundContainer;
    }
    //POST /api/Containers/name
    [HttpPost("{name}")]
    public IActionResult PostState(string name, [FromBody] ContainerRequest request)
    {
        List<Container> containers = dockerService.GetContainers().Result;

        Container foundContainer = containers.Where(c => c.Name == name).FirstOrDefault();

        if (request == null || string.IsNullOrEmpty(request.Command))
        {
            return BadRequest("Invalid state.");
        }
        if (request.Command == "start")
        {
            if (foundContainer == null) return null;
            dockerService.Start(foundContainer.Id);
        }
        if (request.Command == "stop")
        {
            if (foundContainer == null) return null;
            dockerService.Stop(foundContainer.Id);
        }

        return BadRequest("Unknown state.");
    }
}
