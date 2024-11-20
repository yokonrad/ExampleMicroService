$commandParam = $args[0];
$nameParam = $args[1];

$commands = [Ordered]@{
    "DatabaseUpdate" = 'Database -Command Update';
    "DatabaseRevert" = 'Database -Command Revert';

    "DockerUp" = 'Docker -Command Up';
    "DockerDown" = 'Docker -Command Down';

    "MigrationAdd" = 'Migration -Command Add';
    "MigrationRemove" = 'Migration -Command Remove';

    "ToolInstall" = 'Tool -Command Install';
    "ToolUninstall" = 'Tool -Command Uninstall';
    "ToolUpdate" = 'Tool -Command Update';
}

$commandsWithNameParam = @(
    "MigrationAdd"
)

Clear;

if (($([string]::IsNullOrWhiteSpace($commandParam)) -eq $true) -or ($commands.Keys -notcontains $commandParam)) {
    Write-Host;
    Write-Host "Invalid command. Below is a list of available commands:";

    foreach ($command in $commands.Keys) {
        Write-Host "- $($command)";
    }

    Write-Host;

    exit;
}

$command = $commands.$commandParam;

if ($commandsWithNameParam -contains $commandParam) {
    if ($([string]::IsNullOrWhiteSpace($nameParam)) -eq $true) {
        Write-Host;
        Write-Host "Invalid parameter. Provide a valid value.";
        Write-Host;

        exit;
    } else {
        $command = "$command -Name $nameParam";
    }
}

switch -Wildcard ($commandParam) {
    'Database*' {
        PowerShell -Command "
            Import-Module -Force '.\library_database.ps1';
            $command -Service PostMicroService -Context AppDbContext;
            $command -Service PostMicroService -Context PostStateDbContext;
            $command -Service CommentMicroService -Context AppDbContext;
        ";
    }
    'Docker*' {
        PowerShell -Command "
            Import-Module -Force '.\library_docker.ps1';
            $command;
        ";
    }
    'Migration*' {
        PowerShell -Command "
            Import-Module -Force '.\library_migration.ps1';
            $command -Service PostMicroService -Context AppDbContext;
            $command -Service PostMicroService -Context PostStateDbContext;
            $command -Service CommentMicroService -Context AppDbContext;
        ";
    }
    'Tool*' {
        PowerShell -Command "
            Import-Module -Force '.\library_tool.ps1';
            $command;
        ";
    }
}
