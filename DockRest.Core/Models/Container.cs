using System;

namespace DockRest.Core.Models;

public class Container
{

    public Container(DockerService docker, string id, string name, string status, string image, bool isRunning)
    {
        Id = id;
        Name = name;
        Status = status;
        Image = image;
        IsRunning = isRunning;
    }

    //properties
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Status { get; set; }
    public bool IsRunning { get; set; }

}
