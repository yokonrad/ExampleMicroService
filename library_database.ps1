[console]::InputEncoding = [console]::OutputEncoding = New-Object System.Text.UTF8Encoding

New-Module -Name LibraryDatabase -ScriptBlock {

    Function Database() {
        [CmdletBinding()]
        Param (
            [Parameter(Mandatory=$true)]
            [string]$Service,

            [Parameter(Mandatory=$true)]
            [string]$Command
        )

        Clear;

        $servicePath = "$($pwd)\Services\$($Service)";

        if (-not(Test-Path -Path $servicePath)) {
            Write-Host "Invalid service. Below is a list of available services:";

            $availableServices = Get-ChildItem "$($pwd)\Services" -Directory | % {$_.FullName};

            foreach ($availableService in $availableServices) {
                $nameService = Split-Path $availableService -Leaf;

                Write-Host "- $($nameService)";
            }

            Write-Host;

            break;
        }

        $commands = @{
            "Update" = 'dotnet ef database update';
        }

        if ($commands.Keys -notcontains $Command) {
            Write-Host "Invalid command. Below is a list of available commands:";

            foreach ($command in $commands.Keys) {
                Write-Host "- $($command)";
            }

            Write-Host;

            break;
        }

        $basePath = $pwd;
        $command = $commands.$Command;

        Set-Location -Path $servicePath;

        Write-Host "Running library command: $command";
        Write-Host;

        $result = PowerShell -Command $command;

        Set-Location $basePath;

        Write-Host $result;
    }

    Export-ModuleMember -Function Database;

} | Import-Module
