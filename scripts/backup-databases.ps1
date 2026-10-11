$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$output = Join-Path $root 'output/database'
New-Item -ItemType Directory -Force -Path $output | Out-Null
$databases = @('lab_netmicro_payments', 'lab_netmicro_jobs', 'lab_netmicro_policy', 'lab_netmicro_pricing')
foreach ($database in $databases) {
    $containerFile = "/tmp/$database.dump"
    docker exec insurance-postgres pg_dump -U postgres -Fc --create --clean --if-exists -f $containerFile $database
    if ($LASTEXITCODE -ne 0) { throw "Backup failed for $database" }
    docker cp "insurance-postgres`:$containerFile" (Join-Path $output "$database.dump")
    if ($LASTEXITCODE -ne 0) { throw "Copy failed for $database" }
    docker exec insurance-postgres rm -f $containerFile
}
Write-Output "Database backups created in $output"
