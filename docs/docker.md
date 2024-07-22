## Docker

It is used to create an instance of the SQL Server database used in this project.

- [Docker](#docker)
  - [Starting SQL Server](#starting-sql-server)
  - [Stoping SQL Server](#stoping-sql-server)

[Back to Index](../README.md)

### Starting SQL Server

```powershell

$sa_password = "[SA PASSWORD]Pass@word123"

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest

```
[Back to top](#docker)

### Stoping SQL Server

```powershell

docker stop mssql

```

[Back to top](#docker)