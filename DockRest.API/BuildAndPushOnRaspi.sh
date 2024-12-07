
#git clone https://github.com/retaweb/DockRest.git
#cd DockRest

git pull

#build
docker build -f DockRest.API/Dockerfile --force-rm -t ghcr.io/retaweb/dockrest .

#login to ghrc.io before pus
docker push ghcr.io/retaweb/dockrest:latest

#docker run --name dockrest -p 88:8080 -v /var/run/docker.sock:/var/run/docker.sock ghcr.io/retaweb/dockrest