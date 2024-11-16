﻿[console]::InputEncoding = [console]::OutputEncoding = New-Object System.Text.UTF8Encoding

New-Module -Name LibraryDatabase -ScriptBlock {

    Function Database() {
        [CmdletBinding()]
        Param (
            [Parameter(Mandatory=$true)]
            [string]$Service,

            [Parameter(Mandatory=$true)]
            [string]$Command
        )

        $basePath = $pwd;
        $rootPath = Split-Path $pwd -Parent
        $servicePath = "$($rootPath)\Services\$($Service)";

        Write-Host;

        if (-not(Test-Path -Path $servicePath)) {
            Write-Host "Invalid service. Below is a list of available services:";

            $availableServices = Get-ChildItem "$($rootPath)\Services" -Directory | % {$_.FullName};

            foreach ($availableService in $availableServices) {
                $nameService = Split-Path $availableService -Leaf;

                Write-Host "- $($nameService)";
            }

            Write-Host;

            break;
        }

        $commands = [Ordered]@{
            "Update" = 'dotnet ef database update';
            "Revert" = 'dotnet ef database update 0';
        }

        if ($commands.Keys -notcontains $Command) {
            Write-Host "Invalid command. Below is a list of available commands:";

            foreach ($command in $commands.Keys) {
                Write-Host "- $($command)";
            }

            Write-Host;

            break;
        }

        $command = $commands.$Command;

        Set-Location -Path $servicePath;

        Write-Host "Current path: $servicePath";
        Write-Host "Running library command: $command";
        Write-Host;

        PowerShell -Command $command;

        Set-Location $basePath;

        Write-Host;
    }

    Export-ModuleMember -Function Database;

} | Import-Module