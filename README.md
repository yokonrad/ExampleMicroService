# ExampleMicroService

ExampleMicroService is a solution with an example implementation of microservice architecture with usage of REST API architecture and event-driven architecture which is used to communicate between services. There is also an implementation of Ocelot as an API Gateway which is handling whole communication and distributing requests between services.

Within the solution there are tests covering the PostMicroService project with usage of mocking technique.

## Techniques
- API Gateway
- REST API Architecture
- Event-Driven Architecture
- Repository Layer
- Requests Layer
- Service Layer
- Unit Testing with Mocks

## Technologies
- Ocelot
- C# Web API
- AutoMapper
- Entity Framework Core
- MassTransit
- NUnit
- AutoFixture
- Fluent Assertions
- Moq

## Setup

Before starting the application some commands are required to be run to create and configure the environment. All commmands are needed to be run from the Scripts directory inside the root of the solution.

### Entity Framework Core (can be ommited if already installed)

Use command below to install the EF Core tools:

```powershell
helper.ps1 ToolInstall
```

You can find more informations about EF Core tools on the [microsoft.com](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) page.

### Docker

Use command below to start up the docker containers:

```powershell
helper.ps1 DockerUp
```

### Migrations

Initial migrations are created and ready to be used. If you need to modify them, please check the [Script commands](doc:script-commands) section to find how to use helper script.

### Database

Use command below to create the database:

```powershell
helper.ps1 DatabaseUpdate
```

## Script commands

The helper.ps1 script contains more commands which are helping in development.

### EF Core tool
- `ToolInstall`
- `ToolUpdate`
- `ToolUninstall`

### Docker
- `DockerUp`
- `DockerDown`

### Migration
- `MigrationAdd name-of-migration`
- `MigrationRemove name-of-migration`

### Database
- `DatabaseUpdate`
- `DatabaseRevert`

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)