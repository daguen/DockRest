# DockRest

Proof of Concept for a Docker API with Swagger 
> this project is not fully tested and should not be used in a production environment!

## Installation

### Docker Run
```sh
docker run \
--name dockrest \
-p 88:8080 \
-v /var/run/docker.sock:/var/run/docker.sock \
ghcr.io/daguen/dockrest
```

### Docker Compose

```yaml
services:
  dockrest:
    container_name: dockrest
    image: ghcr.io/daguen/dockrest
    restart: unless-stopped
    ports:
    - 88:8080
    volumes:
    - /var/run/docker.sock:/var/run/docker.sock
```

## Usage

### Open Api (Swagger)
Open the swagger site using `http://127.0.0.1:88/swagger/index.html` from the computer your running the container or `http://[your-ip]:88/swagger/index.html` from a remote computer.
