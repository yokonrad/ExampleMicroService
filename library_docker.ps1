[console]::InputEncoding = [console]::OutputEncoding = New-Object System.Text.UTF8Encoding

New-Module -Name LibraryDocker -ScriptBlock {

    Function Docker() {
        [CmdletBinding()]
        Param (
            [Parameter(Mandatory=$true)]
            [string]$Command
        )

        Clear;

        $commands = @{
            "Up" = 'docker-compose up -d';
            "Down" = 'docker-compose down';
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

        Write-Host "Running library command: $command";
        Write-Host;

        cmd /c "$command 2>&1";
    }

    Export-ModuleMember -Function Docker;

} | Import-Module
