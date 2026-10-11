$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
Push-Location $root
try {
    dotnet --version
    node --version
    npm --version
    docker --version
    docker compose version
    java -version
    docker compose -f scripts/infra.yml config --quiet
    docker compose -f scripts/app.yml config --quiet
    Write-Output 'Environment commands and Compose syntax verified.'
}
finally {
    Pop-Location
}
