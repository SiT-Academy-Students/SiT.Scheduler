## Setup

### PostgreSql

You can setup a `PostgreSql` instance with Docker:

> docker volume create --name postgresql-sitscheduler
> <br />
> docker container create --name PostgreSql-SitScheduler -p 5432:5432 -v postgresql-sitscheduler:/var/lib/postgresql/data -e POSTGRES_USER=scheduler_tests_user -e POSTGRES_PASSWORD='123456-Aa' postgres
> <br />
> docker start PostgreSql-SitScheduler

## Database migration

Execute the following command in the `SiT.Scheduler.Data.{database_provider}` project's directory with the command line arguments specific to the requested provider:

> dotnet ef database update -- {arguments}

### PostgreSql

This database provider requires the connection string as the only command line argument.

> dotnet ef database update -- "Host=localhost;Database=Scheduler;Username=scheduler_tests_user;Password='123456-Aa'"
