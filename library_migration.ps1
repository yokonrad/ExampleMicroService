[console]::InputEncoding = [console]::OutputEncoding = New-Object System.Text.UTF8Encoding

New-Module -Name LibraryMigration -ScriptBlock {

    Function Migration() {
        [CmdletBinding()]
        Param (
            [Parameter(Mandatory=$true)]
            [string]$Service,

            [Parameter(Mandatory=$true)]
            [string]$Command,

            [Parameter(Mandatory=$false)]
            [string]$Name
        )

        Clear;

        $servicePath = "$($pwd)\Services\$($Service)";

        if (-not(Test-Path -Path $servicePath)) {
            Write-Host "Invalid service. Below is a list of available services:";

            $availableServices = Get-ChildItem "$($pwd)\Services" -Directory | % {$_.FullName}

            foreach ($availableService in $availableServices) {
                $nameService = Split-Path $availableService -Leaf;

                Write-Host "- $($nameService)";
            }

            Write-Host;

            break;
        }

        $commands = @{
            "Add" = 'dotnet ef migrations add';
            "List" = 'dotnet ef migrations list';
            "Remove" = 'dotnet ef migrations remove';
        }

        if ($commands.Keys -notcontains $Command) {
            Write-Host "Invalid command. Below is a list of available commands:";

            foreach ($command in $commands.Keys) {
                Write-Host "- $($command)";
            }

            Write-Host;

            break;
        }

        if ($Command -eq "Add") {
            if ($([string]::IsNullOrWhiteSpace($Name)) -eq $true) {
                Write-Host "Invalid name of migration. Provide a valid value.";
                Write-Host;

                break;
            }

            $command = "$($commands.$Command) $Name";
        } else {
            $command = "$($commands.$Command)";
        }

        $basePath = $pwd;

        Set-Location -Path $servicePath;

        Write-Host "Running command: $command";
        Write-Host;

        $result = PowerShell -Command $command;

        Set-Location $basePath;

        Write-Host $result;
    }

    Export-ModuleMember -Function Migration;

} | Import-Module
