[console]::InputEncoding = [console]::OutputEncoding = New-Object System.Text.UTF8Encoding

New-Module -Name LibraryMigration -ScriptBlock {

    Function Migration() {
        [CmdletBinding()]
        Param (
            [Parameter(Mandatory=$true)]
            [string]$Service,

            [Parameter(Mandatory=$true)]
            [string]$Command,

            [Parameter(Mandatory=$true)]
            [string]$Context,

            [Parameter(Mandatory=$false)]
            [string]$Name
        )

        $basePath = $pwd;
        $rootPath = Split-Path $pwd -Parent
        $servicePath = "$($rootPath)\Services\$($Service)";

        Write-Host;

        if (-not(Test-Path -Path $servicePath)) {
            Write-Host "Invalid service. Below is a list of available services:";

            $availableServices = Get-ChildItem "$($rootPath)\Services" -Directory | % {$_.FullName}

            foreach ($availableService in $availableServices) {
                $nameService = Split-Path $availableService -Leaf;

                Write-Host "- $nameService";
            }

            Write-Host;

            break;
        }

        $commands = [Ordered]@{
            "Add" = 'dotnet ef migrations add';
            "Remove" = 'dotnet ef migrations remove';
        }

        if ($commands.Keys -notcontains $Command) {
            Write-Host "Invalid command. Below is a list of available commands:";

            foreach ($command in $commands.Keys) {
                Write-Host "- $command";
            }

            Write-Host;

            break;
        }

        if ($([string]::IsNullOrWhiteSpace($Context)) -eq $true) {
            Write-Host "Invalid context. Provide a valid value.";
            Write-Host;

            break;
        }

        if ($Command -eq "Add") {
            if ($([string]::IsNullOrWhiteSpace($Name)) -eq $true) {
                Write-Host "Invalid name. Provide a valid value.";
                Write-Host;

                break;
            }

            $command = "$($commands.$Command) $Name";
        } else {
            $command = $commands.$Command;
        }

        $command = "$command --context $Context";

        Set-Location -Path $servicePath;

        Write-Host "Current path: $servicePath";
        Write-Host "Running command: $command";
        Write-Host;

        PowerShell -Command $command;

        Set-Location $basePath;

        Write-Host;
    }

    Export-ModuleMember -Function Migration;

} | Import-Module
