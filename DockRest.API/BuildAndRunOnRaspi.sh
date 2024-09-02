
#git clone https://github.com/retaweb/DockRest.git
#cd DockRest

docker build -f DockRest.API/Dockerfile --force-rm -t dockrest .
docker run --name DockRest -p 88:8080 -v /var/run/docker.sock:/var/run/docker.sock dockrest