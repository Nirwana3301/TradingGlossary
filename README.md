# TradingGlossary

TradingGlossary ist eine Full-Stack-Anwendung zur strukturierten Verwaltung und Darstellung eines Trading-Glossars. Das Projekt kombiniert ein modernes Angular-Frontend mit einer ASP.NET-Core-Web-API und einer PostgreSQL-Datenbank, sodass Fachbegriffe alphabetisch organisiert, per API gepflegt und im UI uebersichtlich angezeigt werden koennen.

Der aktuelle Stand des Projekts fokussiert sich auf die Kernobjekte eines Glossars:

- `GlossaryLetter` als alphabetische Gruppierung
- `GlossaryEntry` als eigentlicher Glossarbegriff mit Beschreibung
- `GlossaryTag` als vorbereitete Kategorisierung fuer Eintraege
- `User` und `Role` als Basis fuer spaetere Rechte- und Benutzerverwaltung

## Ueberblick

Das Frontend laedt zunaechst alle vorhandenen Glossarbuchstaben und zeigt diese als aufklappbare Sektionen an. Beim Oeffnen eines Buchstabens werden die zugehoerigen Begriffe lazy per API nachgeladen. Die API ist als mehrschichtige .NET-Loesung aufgebaut und trennt Web/API, Anwendungslogik und Datenzugriff in eigene Projekte.

Zusaetzlich stellt das Projekt eine Swagger/OpenAPI-Beschreibung bereit. Daraus wird im Frontend ein TypeScript-API-Client generiert, der bereits im Repository unter `TradingGlossary.UI/src/app/lib/tradingglossary-api` eingecheckt ist.

## Tech-Stack

### Frontend

- Angular 21
- TypeScript 5
- Tailwind CSS 4
- PrimeNG
- NgRx Store
- OpenAPI Generator (`typescript-angular`)

### Backend

- ASP.NET Core / .NET 10
- Entity Framework Core 10
- Npgsql / PostgreSQL
- Swagger / Swashbuckle
- JWT-Bearer-Authentifizierung (Grundgeruest vorhanden)

### Infrastruktur

- Docker Compose
- PostgreSQL
- pgAdmin
- Portainer

## Projektstruktur

```text
TradingGlossary/
|- README.md
|- TradingGlossary.Backend/
|  |- TradingGlossary.Backend.sln
|  |- docker-compose.yml
|  |- TradingGlossary.API/           -> ASP.NET Core Web API + Swagger + Auth + CORS
|  |- TradingGlossary.Application/   -> Controller, DTOs, Services, Middleware
|  |- TradingGlossary.Database/      -> EF Core DbContext, Entities, Configurations, Migrations
|
|- TradingGlossary.UI/
   |- package.json
   |- angular.json
   |- src/app/glossary/              -> Glossar-UI
   |- src/app/lib/tradingglossary-api/ -> generierter OpenAPI-Client
```

## Architektur

### Frontend

Das Angular-Frontend ist als Standalone-App aufgebaut. Die Glossaransicht laedt zunaechst alle Buchstaben und holt die zugehoerigen Begriffe erst beim Aufklappen eines Buchstabens nach. Dadurch bleibt die erste Ladeoperation klein und die API-Nutzung zielgerichtet.

Die API-Basis-URL ist aktuell direkt im Frontend konfiguriert:

- `http://localhost:5000`

Die Hauptansicht des Glossars ist derzeit unter folgender Route registriert:

- `http://localhost:4200/home`

### Backend

Die API verwendet eine klassische Schichtenaufteilung:

- `TradingGlossary.API`: Startpunkt, Dependency Injection, Swagger, Auth, CORS, Healthcheck
- `TradingGlossary.Application`: REST-Controller, DTOs, Service-Logik, Middleware
- `TradingGlossary.Database`: EF-Core-Modelle, Konfigurationen, Migrationen, `DbContext`

Wichtige Infrastruktur im Backend:

- Swagger UI im Development-Modus
- CORS-Freigabe fuer `http://localhost:4200`
- Healthcheck unter `/healthz`
- PostgreSQL-Anbindung ueber `DefaultConnection`
- Middleware zur Uebergabe von Benutzer- und Request-Kontext (`SessionInfo`)

### Datenmodell

Das Datenmodell bildet ein alphabetisch strukturiertes Glossar ab:

- `GlossaryLetter`: gruppiert Begriffe alphabetisch ueber `Code`, `Label` und `SortOrder`
- `GlossaryEntry`: enthaelt `Title` und `Description` und gehoert immer zu genau einem `GlossaryLetter`
- `GlossaryTag`: dient als vorbereitete Kategorisierung mit `Label` und `SortOrder`
- `GlossaryEntryTag`: bildet die n:m-Beziehung zwischen Eintraegen und Tags ab
- `User`: ist mit einer `Role` verknuepft und fuer kuenftige Authentifizierungs-/Autorisierungs-Szenarien vorbereitet

Alle zentralen Entitaeten besitzen Audit-Felder wie `CreatedAt`, `CreatedBy`, `ModifiedAt` und `ModifiedBy`.

## Verfuegbare API-Bereiche

### `GlossaryLetter`

- `GET /api/GlossaryLetter`
- `GET /api/GlossaryLetter/{id}`
- `POST /api/GlossaryLetter`
- `PUT /api/GlossaryLetter/{id}`
- `DELETE /api/GlossaryLetter/{id}`

### `GlossaryEntry`

- `GET /api/GlossaryEntry`
- `GET /api/GlossaryEntry/{id}`
- `GET /api/GlossaryEntry/letter/{letterId}`
- `POST /api/GlossaryEntry`
- `PUT /api/GlossaryEntry/{id}`
- `DELETE /api/GlossaryEntry/{id}`

### `GlossaryTag`

- `GET /api/GlossaryTag`
- `POST /api/GlossaryTag`
- `PUT /api/GlossaryTag`
- `DELETE /api/GlossaryTag/{id}`

### Weitere technische Endpunkte

- `GET /healthz`
- Swagger JSON: `/swagger/v1/swagger.json`
- Swagger UI: `/swagger`

## Authentifizierung und Sicherheit

Das Projekt enthaelt bereits ein JWT-Bearer-Grundgeruest. Die zentrale Konfiguration liegt in `TradingGlossary.Backend/TradingGlossary.API/appsettings.json`.

Aktueller Stand:

- Swagger ist fuer Bearer-Tokens vorbereitet
- `GlossaryTagController` und `UserController` sind mit `[Authorize]` geschuetzt
- `GlossaryLetterController` und `GlossaryEntryController` sind aktuell offen, da das Attribut dort auskommentiert ist
- `Authentication:Authority` steht noch auf `TBA` und muss fuer echte Identity-Provider ergaenzt werden

Damit ist die Sicherheitsstruktur vorbereitet, aber noch nicht vollstaendig produktionsreif konfiguriert.

## Voraussetzungen

Fuer die lokale Entwicklung solltest du Folgendes installiert haben:

- .NET SDK 10
- Node.js mit npm
- Docker Desktop

Optional, aber hilfreich:

- pgAdmin
- ein IDE-Setup fuer .NET und Angular

## Lokale Entwicklung starten

### 1. Repository oeffnen

Arbeite aus dem Projekt-Root:

```powershell
cd C:\Users\anton\Desktop\TradingGlossary\TradingGlossary
```

### 2. Infrastruktur per Docker starten

Im Backend-Verzeichnis liegt die Docker-Compose-Konfiguration fuer PostgreSQL, pgAdmin und Portainer.

```powershell
cd TradingGlossary.Backend
docker compose up -d
```

Die Konfiguration nutzt die Werte aus `TradingGlossary.Backend/.env`.

Enthaltene Services:

- PostgreSQL auf Port `5432`
- pgAdmin auf Port `8182`
- Portainer auf Port `8180`

### 3. Backend starten

```powershell
cd TradingGlossary.Backend
dotnet run --project .\TradingGlossary.API
```

Im Frontend und in der OpenAPI-Client-Generierung wird aktuell davon ausgegangen, dass die API unter `http://localhost:5000` erreichbar ist.

### 4. Frontend starten

```powershell
cd TradingGlossary.UI
npm install
npm run start
```

Falls deine PowerShell lokale Skriptausfuehrung blockiert, funktioniert unter Windows in der Regel auch:

```powershell
npm.cmd run start
```

Anschliessend erreichst du die Anwendung unter:

- `http://localhost:4200/home`

## Build und Qualitaetssicherung

### Backend bauen

```powershell
cd TradingGlossary.Backend
dotnet build TradingGlossary.Backend.sln
```

### Frontend bauen

```powershell
cd TradingGlossary.UI
npm.cmd run build
```

### Frontend-Tests

```powershell
cd TradingGlossary.UI
npm.cmd run test
```

Hinweis: Im aktuellen Repository sind zwar Build-Skripte vorhanden, aber keine dedizierten Testprojekte im Backend sichtbar.

## Datenbank und Migrationen

Das Projekt enthaelt bereits eine erste EF-Core-Migration:

- `20260317181545_InitalFirst`

Die automatische Migration beim API-Start ist im `Program.cs` aktuell auskommentiert. Wenn du das Schema manuell anwenden willst, kannst du das typischerweise mit EF Core erledigen:

```powershell
cd TradingGlossary.Backend
dotnet ef database update --project .\TradingGlossary.Database --startup-project .\TradingGlossary.API
```

Zusaetzlich existiert im Repository ein Datenbank-Dump fuer lokale Entwicklungszwecke im Docker-Datenbereich.

## OpenAPI-Client im Frontend

Das Frontend verwendet einen generierten TypeScript-Client auf Basis der Swagger-Spezifikation des Backends. Der Generator ist in `TradingGlossary.UI/package.json` bereits vorbereitet.

Client neu generieren:

```powershell
cd TradingGlossary.UI
npm.cmd run generate-api-client
```

Voraussetzung dafuer:

- Das Backend laeuft lokal
- Swagger ist unter `http://localhost:5000/swagger/v1/swagger.json` erreichbar

Der generierte Client wird unter folgendem Alias eingebunden:

- `@api/tradingglossary-api`

## Aktueller Funktionsstand

Bereits umgesetzt:

- Full-Stack-Grundstruktur mit getrenntem Frontend und Backend
- PostgreSQL-Anbindung mit EF Core
- CRUD-Endpunkte fuer Buchstaben, Eintraege und Tags
- Alphabetische Darstellung der Glossarstruktur im Frontend
- Lazy Loading der Eintraege pro Buchstabe
- Swagger/OpenAPI-Integration
- Generierter Angular-API-Client

Vorbereitet, aber noch nicht vollstaendig ausgebaut:

- vollstaendige JWT-Integration
- produktive Benutzer- und Rollenlogik
- Tag-Darstellung im Frontend
- umfassende Testabdeckung
- produktionsreife Deployment-Konfiguration

## Entwicklungsnotizen

Ein paar Punkte, die beim Arbeiten am Projekt hilfreich sind:

- Das Frontend verwendet derzeit eine feste API-Basis-URL statt Environment-Dateien.
- Der Route-Einstieg fuer die Glossaransicht ist aktuell `/home`.
- Die Welcome-Splash-Komponente ist vorhanden, wird im aktuellen App-Template aber nicht verwendet.
- Die automatische Datenbankmigration beim Start der API ist deaktiviert.

## Warum dieses Projekt sinnvoll aufgebaut ist

TradingGlossary ist mehr als eine einfache Liste von Begriffen. Durch die Trennung von UI, API und Datenzugriff ist die Codebasis gut erweiterbar, zum Beispiel fuer:

- Such- und Filterfunktionen
- Tag-basierte Navigation
- Admin-Oberflaechen fuer Content-Pflege
- rollenbasierte Freigaben
- Mehrsprachigkeit
- Deployment in Cloud- oder Container-Umgebungen

Damit eignet sich das Projekt sowohl als produktnahe Glossar-Anwendung als auch als solide Grundlage fuer ein groesseres Lern-, Demo- oder Fachportal im Trading-Kontext.
