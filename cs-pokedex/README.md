# cs-pokedex

This is a Blazor server-side application that consumes the PokéAPI and stores Pokémon metadata locally using Entity Framework Core with SQLite.

## Prerequisites

- .NET 10 SDK (install from https://dotnet.microsoft.com)
- Optional: Visual Studio 2022/2023 or Visual Studio Code
- Git (optional)
- dotnet-ef global tool (used to generate and apply EF migrations)

## Getting started

1. Clone the repository:

```bash
git clone https://github.com/PrivatePrinny/cs-pokedex.git
cd cs-pokedex
```

2. Install dotnet-ef (if not installed):

```bash
dotnet tool install --global dotnet-ef --version 10.*
```

3. Restore packages and build:

```bash
dotnet restore
dotnet build
```

4. Database configuration

The application uses SQLite by default. Connection string is defined in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=cs-pokedex.db"
}
```

5. Apply EF migrations (migrations are included):

```bash
dotnet ef database update --project cs-pokedex --startup-project cs-pokedex
```

6. Run the app:

```bash
dotnet run --project cs-pokedex
```

Then open the URL shown in the console (usually `https://localhost:5001`).

## Key project files

- `cs-pokedex/Program.cs` — application startup and DI registration
- `cs-pokedex/Utilities/PokeApiClient.cs` — client for PokéAPI
- `cs-pokedex/Data` — EF models and `ApplicationDbContext`
- `cs-pokedex/Components` — Blazor components
- `cs-pokedex/Migrations` — EF migrations

## Common commands

- Add a new migration:

```bash
dotnet ef migrations add <Name> --project cs-pokedex --startup-project cs-pokedex
```

- Revert last migration:

```bash
dotnet ef migrations remove --project cs-pokedex --startup-project cs-pokedex
```

- Run unit tests (if present):

```bash
dotnet test
```

## Notes and recommendations

- Avoid creating `ApplicationDbContext` manually with `new`. Use the `IPokemonRepository`/DI services registered in `Program.cs`.
- `JsonNode` helpers should avoid `GetValue<string>()` for numeric fields — use `ToString()` or typed accessors.
- Consider adding `IPokeApiClient` interface to make the `PokeApiClient` testable and injectable.

If you want, I can also:
- Add contributing guidelines and license file
- Commit the migration files to the repo (if not already committed)
- Update `PokeApiClient` to be DI-friendly and register it in `Program.cs`
