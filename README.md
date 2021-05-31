# ObsidianSQL
ObidianSQL stellt eine HTTP-Schnittstelle zur Verfügung, mit deren Hilfe Datenbanken (zurzeit nur SQLite) verwaltet werden können.

## Build
Zum Erstellen des Projekts wird das .NET 5-SDK benötigt. Dieses steht bei Microsoft für alle gängigen Plattformen zur Verfügung: https://docs.microsoft.com/de-de/dotnet/core/install/

Anschließend kann das Projekt mit dem Befehl `dotnet build` erstellt werden.

## Ausführen
Um das Projekt auszuführen wird der folgende Befel verwendet:  
`dotnet run --project ObsidianSQL.server/ObsidianSQL.server.csproj`

Sobald der Server läuft, kann in Postman die mitgelieferte Collection `ObsidianSQL.postman_collection.json` importiert werden. Damit können alle HTTP-Aufrufe ausgeführt werden. Als erstes sollte hier der Login-Aufruf ausgeführt werden, damit ein Token generiert wird, welches für die anderen Aufrufe benötigt wird. Das Token wird beim Login automatisch in Postman gespeichert, weshalb hier nichts bei den weiteren Requests beachtet werden muss.

## Tests
Um die im Projekt enthaltenen Tests auszuführen, kann der folgende Befehl verwendet werden: `dotnet test`
