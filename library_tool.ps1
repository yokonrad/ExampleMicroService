[console]::InputEncoding = [console]::OutputEncoding = New-Object System.Text.UTF8Encoding

New-Module -Name LibraryTool -ScriptBlock {

    Function Tool() {
        [CmdletBinding()]
        Param (
            [Parameter(Mandatory=$true)]
            [string]$Command
        )

        $commands = @{
            "Install" = 'dotnet tool install --global dotnet-ef';
            "Uninstall" = 'dotnet tool uninstall --global dotnet-ef';
            "Update" = 'dotnet tool update --global dotnet-ef';
        }

        Clear;

        if ($commands.Keys -notcontains $Command) {
            Write-Host "Invalid command. Below is a list of available commands:";

            foreach ($command in $commands.Keys) {
                Write-Host "- $($command)";
            }

            Write-Host;

            break;
        }

        $command = $commands.$Command;

        Write-Host "Running command: $command";
        Write-Host;

        $result = PowerShell -Command $command;

        Write-Host $result;
    }

    Export-ModuleMember -Function Tool;

} | Import-Module
