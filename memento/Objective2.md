# New OBJECTIVE

Update container configuration to:
-Reflect new folder/project structure
-Ensure successful multi-stage builds
-Optimize Docker layer caching
-Reduce image size
-Maintain environment-specific configuration
-Preserve networking and service dependencies
-Ensure production-readiness
-Do not change runtime behavior unless necessary

## Phase 1 - Repository Analysis

-Analyze new solution structure:
-Location of API project
-Core project
-Application project
-Infrastructure project
Identify:
-Updated .csproj paths
-Solution file changes
-Project references
-Target framework version
-Review existing Dockerfile.
-Review existing docker-compose.yml.
-Identify breaking path changes.
-Produce an analysis report before modifying

## PHASE 2 – Dockerfile Refactor

Requirements:
Use multi-stage build
Use official .NET SDK and ASP.NET runtime images
Optimize restore caching
Copy only necessary project files before restore
Build only API project
Publish in Release mode
Use non-root user if possible
Expose correct port
Add health check if applicable

## PHASE 3 – docker-compose Update

Review and update:
-Build context path
-Dockerfile location
-Environment variables
-Connection strings
-ASPNETCORE_ENVIRONMENT
-Health checks
-Service dependencies (e.g., database, Redis)
-Network configuration
-Volume mounts (if needed)

## PHASE 4 – Production Hardening

Ensure:
-No secrets hardcoded
-Use .env file support
-Use non-root user where possible
-Add health checks:
-/health endpoint
-Add restart policy
-Enable container logging compatibility

## PHASE 5 – Optimization

Ensure:
-Docker layer caching optimized
-Only required files copied
-.dockerignore properly configured
-Final image size minimized
-SDK image not included in final stage

## PHASE 6 – Output Deliverables

Provide:
-Updated Dockerfile (final version)
-Updated docker-compose.yml
-.dockerignore recommendations
-Summary of changes
-Performance improvements
-Security improvements
-Migration instructions
-CI/CD compatibility notes

## CONSTRAINTS

Do not introduce breaking port changes without explanation.
Do not modify database schema.
Do not introduce unnecessary sidecar containers.
Maintain backward compatibility.

## OPTIONAL ENHANCEMENTS

If applicable, evaluate:
Using build args
Multi-arch support
Container scanning recommendations
Readiness/liveness probe improvements

## SQL database volume

I want that if database has created on docker container, it should be available all the time if container delated and data persist not deleted 
solve this problem 
when I build and up compose it should connect with the existing db
