$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
Push-Location $root
try {
    if (-not (Test-Path (Join-Path $root 'Web/dist/index.html'))) {
        throw 'Web/dist is missing. Run scripts/build.ps1 before building Docker images.'
    }
    docker info | Out-Null
    if ($LASTEXITCODE -ne 0) { throw 'Docker Engine is not running. Start Docker Desktop first.' }
    docker compose -f scripts/infra.yml up -d --build
    if ($LASTEXITCODE -ne 0) { throw 'Infrastructure startup failed' }
    docker compose -f scripts/app.yml up -d --build
    if ($LASTEXITCODE -ne 0) { throw 'Application startup failed' }
    docker compose -f scripts/infra.yml ps
    docker compose -f scripts/app.yml ps
}
finally {
    Pop-Location
}
