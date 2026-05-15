# build.ps1 - Build and deploy mod files

$ElinPath    = "C:\Program Files (x86)\Steam\steamapps\common\Elin"
$PackageName = "SlaverReroll"
$PackageDest = "$ElinPath\Package\$PackageName"

Write-Host "=== $PackageName Build Script ===" -ForegroundColor Cyan

Write-Host "`n[1/3] Building..." -ForegroundColor Yellow
dotnet build ".\$PackageName.csproj" --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed." -ForegroundColor Red
    exit 1
}

Write-Host "`n[2/3] Copying package files..." -ForegroundColor Yellow
if (-not (Test-Path $PackageDest)) {
    New-Item -ItemType Directory -Path $PackageDest | Out-Null
}
Copy-Item ".\package\package.xml" -Destination $PackageDest -Force
if (Test-Path ".\package\preview.jpg") {
    Copy-Item ".\package\preview.jpg" -Destination $PackageDest -Force
}

Write-Host "`n[3/3] Done!" -ForegroundColor Green
Write-Host "Output: $PackageDest" -ForegroundColor Cyan
Write-Host "`nLaunch Elin and verify the mod works." -ForegroundColor White
