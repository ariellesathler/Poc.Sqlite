#!/usr/bin/env pwsh
# Define the source file and destination directory
$sourceFile = "LocalDatabaseNoSql.db"
$destinationDirectory = "E:\GIT\database-bkp"

# Check if the destination directory exists, create it if not
if (-not (Test-Path -Path $destinationDirectory -PathType Container)) {
    New-Item -ItemType Directory -Path $destinationDirectory | Out-Null
}

# Copy the file to the destination directory
Copy-Item -Path $sourceFile -Destination $destinationDirectory -Force

Write-Host "File copied successfully to $destinationDirectory"

# Navigate to the destination directory
Set-Location $destinationDirectory

# Git commands
git add .
git commit -m "Committing changes"
git push
