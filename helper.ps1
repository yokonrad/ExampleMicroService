$commandParam = $args[0];
$serviceParam = $args[1];
$nameParam = $args[2];

$commands = @{
    "DatabaseUpdate" = 'Database -Command Update';

    "DockerUp" = 'Docker -Command Up';
    "DockerDown" = 'Docker -Command Down';

    "MigrationAdd" = 'Migration -Command Add';
    "MigrationList" = 'Migration -Command List';
    "MigrationRemove" = 'Migration -Command Remove';

    "ToolInstall" = 'Tool -Command Install';
    "ToolUninstall" = 'Tool -Command Uninstall';
    "ToolUpdate" = 'Tool -Command Update';
}

$commandsWithServiceParam = @(
    "DatabaseUpdate",

    "MigrationAdd",
    "MigrationList",
    "MigrationRemove"
)

$commandsWithNameParam = @(
    "MigrationAdd"
)

Clear;

if (($([string]::IsNullOrWhiteSpace($commandParam)) -eq $true) -or ($commands.Keys -notcontains $commandParam)) {
    Write-Host "Invalid command. Below is a list of available commands:";

    foreach ($command in $commands.Keys) {
        Write-Host "- $($command)";
    }

    Write-Host;

    exit;
}

$command = $commands.$commandParam;

if ($commandsWithServiceParam -contains $commandParam) {
    if ($([string]::IsNullOrWhiteSpace($serviceParam)) -eq $true) {
        Write-Host "Invalid first parameter. Provide a valid value.";
        Write-Host;

        exit;
    } else {
        $command = "$command -Service $serviceParam";
    }
}

if ($commandsWithNameParam -contains $commandParam) {
    if ($([string]::IsNullOrWhiteSpace($nameParam)) -eq $true) {
        Write-Host "Invalid second parameter. Provide a valid value.";
        Write-Host;

        exit;
    } else {
        $command = "$command -Name $nameParam";
    }
}

Write-Host "Running PowerShell command: $command";

switch -Wildcard ($commandParam) {
    'Database*' { PowerShell -Command "Import-Module -Force '.\library_database.ps1'; $command"; }
    'Docker*' { PowerShell -Command "Import-Module -Force '.\library_docker.ps1'; $command"; }
    'Migration*' { PowerShell -Command "Import-Module -Force '.\library_migration.ps1'; $command"; }
    'Tool*' { PowerShell -Command "Import-Module -Force '.\library_tool.ps1'; $command"; }
}
