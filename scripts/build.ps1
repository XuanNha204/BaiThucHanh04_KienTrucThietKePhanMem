$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
Push-Location $root
try {
    dotnet restore InsuranceAgency.sln
    if ($LASTEXITCODE -ne 0) { throw 'dotnet restore failed' }
    dotnet build InsuranceAgency.sln --no-restore --nologo
    if ($LASTEXITCODE -ne 0) { throw 'dotnet build failed' }
    dotnet test InsuranceAgency.sln --no-build --no-restore --nologo
    if ($LASTEXITCODE -ne 0) { throw 'dotnet test failed' }
    npm ci --prefix Web --legacy-peer-deps
    if ($LASTEXITCODE -ne 0) { throw 'npm ci failed' }
    $env:NODE_OPTIONS = '--openssl-legacy-provider'
    npm run build --prefix Web
    if ($LASTEXITCODE -ne 0) { throw 'Vue build failed' }
}
finally {
    Pop-Location
}
