# Azure DevOps Builds
Contains all build definitions to be used with Azure DevOps:
- **CI build** will validate every Pull Request (PR) by building all codebase & Docker images. Once merged to master, the same build will be triggered but also push all Docker images to [Docker Hub](https://hub.docker.com/u/coditeu)