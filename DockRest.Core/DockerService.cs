using System;
using Docker.DotNet;
using Docker.DotNet.Models;
using DockRest.Core.Models;

namespace DockRest.Core;

public class DockerService
{
    private readonly DockerClient client;

    public DockerService()
    {
        client = new DockerClientConfiguration(
            new Uri("unix:///var/run/docker.sock"))
            .CreateClient();
    }

    public async Task<List<Container>> GetContainers()
    {
        IList<ContainerListResponse> dockerContainers = await client.Containers.ListContainersAsync(
            new ContainersListParameters()
            {
                Limit = 20,
            });

        List<Container> containers = new List<Container>();

        foreach (ContainerListResponse dockerContainer in dockerContainers)
        {
            string name = dockerContainer.Names.First().Replace("/", "");
            string status = FormatStatus(dockerContainer.Status);
            bool isRunning = false;
            if (dockerContainer.State.ToLower() == "running") isRunning = true;

            containers.Add(new Container(this, dockerContainer.ID, name, status, dockerContainer.Image, isRunning));
        }

        return containers;
    }
    private string FormatStatus(string status)
    {
        if (status.ToLower().Contains("up"))
        {
            try
            {
                status = status.Replace("Up ", "");
                status = status.Split('(')[0];
            }
            catch (Exception)
            {
            }

        }
        if (status.ToLower().Contains("exited"))
        {
            try
            {
                status = status.Split(')')[1];
            }
            catch (Exception)
            {
            }
        }
        return status;
    }

    public async Task Start(string id)
    {
        await client.Containers.StartContainerAsync(
            id,
            new ContainerStartParameters()
            );
    }
    public async Task Stop(string id)
    {
        var stopped = await client.Containers.StopContainerAsync(
            id,
            new ContainerStopParameters());
    }
}
