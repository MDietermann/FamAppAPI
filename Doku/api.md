
# <a name="top" style="color:orange">*FamApp-API Dokumentation*</a>

Die FamApp-API (im Folgenden *API*) sowie die folgende Dokumentation wurde von mir, Marvin Dietermann, im Zusammenhang mit unserer Entwicklung der Familienplaner-App *FamApp* (im Folgenden *App*) erstellt.

### **Dieses Projekt wird durchgeführt von Marvin Dietermann und Timea Linke.**

## <span style="color:orange">Inhaltsverzeichnis</span> 


 - [Grundlegendes](#grundlegendes)
 - [Definition: API](#definition)
    - ["Application"](#application)
    - ["Programming"](#programming)
    - ["Interface"](#interface)
 - [Definition: REST](#rest)
 - [Codische Funktionsweise einer API](#apiCode)
 - [Funktionen der API](#apiFunc)
 - [Datenbank](#database)


## <a name="grundlegenes" style="color:orange">Grundlegendes</a>

Die *API* ist eine Web-API für die FamApp, welche die Kommunikation mit dem Backendserver ermöglicht.
Die Schnittstelle ermöglicht es, diese Kommunikation zu sichern sowie eine ortsunabhängige Funktionalität der App zu realisieren.
Dies ist erforderlich, da die Anwendung die Koordination der Finanz- und Terminplanung für mehrere Personen an einem zentralen Ort ermöglichen soll. 
Dadurch kann jedes Gruppenmitglied die Informationen einsehen, bearbeiten oder neue Einträge erstellen. Grundgedanke hierbei wäre die Nutzung innerhalb von Familien.

Um dies zu ermöglichen, wird eine *REST*-Schnittstelle verwendet.

## <a name="definition" style="color:orange">Definition: API</a>

Zunächst ein kleiner Exkurs zum allgemeinen Thema *REST-API*:
**API** ist eine Abkürzung für **Application Programming Interface**.

- ### <a name="application" style="color: lightblue">**Application**:</a>
>*"Application"* steht für "Anwendung" oder "Applikation". In diesem Kontext bezieht sich "Application" auf die Softwareanwendung oder das Computerprogramm, das die API nutzt oder darauf zugreift.				
>
>Eine API ist eine Schnittstelle, die es Anwendungen ermöglicht, miteinander zu kommunizieren oder auf bestimmte Funktionen und Dienste zuzugreifen. Wenn wir also von "Application" in Bezug auf eine API sprechen, meinen wir die Software oder Anwendung, die die API verwendet, um Daten auszutauschen, Funktionen aufzurufen oder andere Aufgaben zu erledigen.
>
>Beispiel: Wenn Sie eine Anwendung entwickeln, die auf Wetterdaten zugreifen möchte, könnten Sie eine Wetter-API verwenden. In diesem Fall wäre Ihre Anwendung die "Application", die die Wetter-API nutzt, um aktuelle Wetterinformationen abzurufen und in Ihrer Anwendung anzuzeigen.


- ### <a name="programming" style="color: lightblue">**Programming**:</a>
>*"Programming"* steht für das Entwickeln von Software oder das Schreiben von Code, insbesondere für die Interaktion mit der API. Das Wort "Programming" bezieht sich hier auf die Aktivität des Programmierens oder Codierens, um eine Anwendung zu erstellen oder zu modifizieren, die auf die Funktionen und Dienste einer API zugreift.
>
>APIs bieten eine Schnittstelle, über die Softwareanwendungen miteinander kommunizieren können. Die Entwickler programmieren (schreiben Code für) ihre Anwendungen so, dass sie die API verwenden, um Daten auszutauschen, Funktionen aufzurufen oder andere Aktionen durchzuführen. Dieser Prozess des Programmierens beinhaltet die Integration und Nutzung der Schnittstelle, die die API bereitstellt.
>
>Beispiel: Wenn eine Anwendung auf die Twitter-API zugreifen möchte, programmieren die Entwickler den Code der Anwendung so, dass er die Twitter-API nutzt, um Tweets abzurufen, zu posten oder andere Twitter-Funktionen zu verwenden. Daher bezieht sich "Programming" im Zusammenhang mit APIs auf die Entwicklung von Software, um die Funktionen der API in der Anwendung zu nutzen.

- ### <a name="interface" style="color: lightblue">**Interface**:</a>
>*"Interface"* bezeichnet die Schnittstelle, die eine Softwareanwendung bereitstellt, um mit anderen Anwendungen oder Komponenten zu interagieren. Es handelt sich um die definierten Methoden, Funktionen und Parameter, die von der API unterstützt werden und es anderen Programmen ermöglichen, auf diese zuzugreifen.
>
>Eine API-Schnittstelle legt fest, wie verschiedene Softwarekomponenten miteinander kommunizieren können. Es dient als Vertrag oder Vereinbarung zwischen den verschiedenen Teilen der Software. Entwickler, die eine API verwenden möchten, müssen sich an die Spezifikationen dieser Schnittstelle halten, um sicherzustellen, dass ihre Anwendungen ordnungsgemäß mit der API kommunizieren können.
>
>Es gibt zwei Haupttypen von Schnittstellen im Kontext von APIs:
>1. User Interface (Nutzer-Schnittstelle): Dies bezieht sich auf die Art und Weise, wie menschliche Benutzer mit einer Softwareanwendung interagieren. Es umfasst visuelle Elemente wie Buttons, Menüs und Formulare.
>2. Application Programming Interface (API): Dies ist die Schnittstelle, die von Softwareentwicklern verwendet wird, um auf Funktionen und Dienste zuzugreifen, die von anderen Softwarekomponenten oder Diensten bereitgestellt werden.
>
>Im API-Kontext bezieht sich "Interface" also auf die programmatische Schnittstelle, über die Softwareanwendungen miteinander kommunizieren können. Es definiert die Methoden, Parameter und Datenformate, die bei der Interaktion mit der API verwendet werden sollen.

---

## <a name="rest" style="color:orange">Definition: REST</a>
**REST** steht für *"Representational State Transfer"*. 
Es ist ein Architekturstil, der von *Roy Fielding* in seiner Dissertation von 2000 mit dem Titel **"Architectural Styles and the Design of Network-based Software Architectures"** eingeführt wurde. 

**REST** ist keine spezifische Technologie, sondern ein Satz von Prinzipien und Konventionen, die oft für den Entwurf von Web-Services, insbesondere *APIs*, verwendet werden.

In Bezug auf RESTful APIs steht REST für die Grundsätze, die bei der Gestaltung dieser APIs beachtet werden sollen. Einige der Kernprinzipien von REST sind:

- ### <span style="color: lightblue">**Zustandslose Kommunikation:** </span>
>Jede Anfrage vom Client an den Server enthält alle Informationen, die für den Server erforderlich sind, um die Anfrage zu verstehen und zu verarbeiten. Der Server speichert keine Informationen über den Zustand des Clients zwischen den Anfragen.

- ### <span style="color: lightblue">**Ressourcen:** </span>
>REST behandelt Daten und Dienste als Ressourcen, die durch URIs (Uniform Resource Identifiers) identifiziert werden. Diese Ressourcen können in verschiedenen Formaten repräsentiert werden, wie z.B. JSON oder XML.

- ### <span style="color: lightblue">**Standardmethoden:**</span>
>RESTful APIs verwenden standardisierte HTTP-Methoden wie GET, POST, PUT und DELETE, um CRUD-Operationen (Create, Read, Update, Delete) auf Ressourcen durchzuführen.

- ### <span style="color: lightblue">**Repräsentation:**</span>
>Ressourcen werden durch Repräsentationen dargestellt, die in der Regel in einem bestimmten Format wie JSON oder XML vorliegen. Clients können die Ressourcen manipulieren, indem sie die entsprechenden Repräsentationen senden.

Die Verwendung von REST-Prinzipien erleichtert die Entwicklung von skalierbaren und leicht verständlichen APIs. RESTful APIs sind weit verbreitet und bilden die Grundlage vieler moderner Web-Services.

## <a name="apiCode" style="color:orange">Codische Funktionsweise einer API</a>

Der codische Aufbau einer API besteht in der Regel aus 3 Teilen:

- ### <span style="color: lightblue">Controller</span>

>Der Controller verwaltet einkommende Abfragen[^1] und verarbeitet diese.
Es wird für jede Tabelle ein eigener Controller erstellt, der die jeweiligen Tabellenabfragen verwaltet.
Vorab wird im für jeden die Standartroute festgelegt:

```cs
01  namespace FamAppAPI.Controllers
02  {
03      // Der Controller zur Bearbeitung von API-Anfragen im Zusammenhang mit Gruppen
04    
05      [Route("api/[controller]")]
06      [ApiController]
07      ...
08      // Weiterfolgender Code des Controllers //
09      ...
10  }
```

>[controller] stellt hierbei eine Variable für die Tabellennamen dar, welche der Controller selbst verwaltet.
Daraufhin werden im Construktor des Controllers sowohl das dazugehörige *Repository-Interface* als auch wie in unserem Fall ein *Mapper* festgelegt:

```cs
01  // Konstruktor zur Initialisierung des GroupController
02     
03  public GroupController(IGroupsRepository groupsRepository, IMapper mapper)
04  {
05      _groupRepository = groupsRepository;
06      _mapper = mapper;
07  }
```

> Hiernach folgen die sogenannten *Response-Methods*. Diese definieren, wie die API bzw. dieser Controller mit den gegebenen Routen umgehen muss:

```cs
01  // Eine Gruppe anhand der ID abrufen
02
03  [HttpGet("id/{groupId}")]   <-- Route dieser Anfrage
04
05  [ProducesResponseType(200, Type = typeof(Group))]   <-- Statuscode und Typ (Hier 200 = Erfolg)
06  [ProducesResponseType(404)]     <-- Statuscode 404 = Fehler
07
08  public IActionResult GetGroupById(int groupId)
09      => !_groupRepository.GroupExistsById(groupId)   <-- Prüfung ob Gruppe existiert durch eine im Repository festgelegte Methode
10      ? NotFound()    <-- Statuscode 404 = nicht gefunden
11      : Ok(_mapper.Map<GroupsDto>(_groupRepository.GetGroupById(groupId)));   <-- Falls Gruppe existiert, wird die Gruppe in ein DTO umgewandelt und als Statuscode 200 zurückgegeben
```

>Das *DTO* (Data Transfer Object), was hier nun aufgetaucht ist, erstellen wir selbst. 
Dies ist ein Designmuster in der Softwareentwicklung, welches verwendet wird, um Daten zwischen Softwarekomponenten zu übertragen. 
Das Hauptziel eines DTO ist es, Daten zwischen verschiedenen Teilen eines Systems zu transportieren, ohne dass die darunter liegende Struktur direkt offengelegt wird.
Dies hilft, die Kopplung zwischen den verschiedenen Komponenten zu reduzieren.
Hier eine Gegenüberstellung des GroupDto mit der eigentlichen Group-Klasse:

```cs
01  public class GroupsDto
02  {
03      public int id { get; set; }
04      public string name { get; set; }
05      public int UserId { get; set; }
06      public bool premium { get; set; }
07  }
08
09  public class Groups
10  {
11      public int id { get; set; }
12      public string name { get; set; }
13      public int UserId { get; set; }
14      public bool premium { get; set; }
15
16      public User User { get; set; }                              <-- Nicht im GroupDto vorhanden!!
17      public ICollection<UserInGroup> UsersInGroups { get; set; } <-- Nicht im GroupDto vorhanden!!
18  }
```

> Das DTO verhindert hier, dass die 2 Parameter User und UsersInGroups mitgesendet werden. Diese würden `<null>` zurückgeben, da sie lediglich für die Logik der API über die Datenbank existieren. 
>
>So weiß nun die API, dass sie bei der Abfrage `https://www.api_test_abfrage.de/api/Group/id/256` die Methode `public IActionResult GetGroupById(int groupId)` ausführen müsste, welche weiter in das Repository leiten würde.
Hier würden wir ein JSON-Objekt zurückbekommen, welches die Gruppe mit der ID 256 und den Parametern der GroupDto repräsentiert, falls diese existiert.
Falls diese Gruppe nicht existiert, wird der Statuscode 404 (nicht gefunden) zurückgegeben.


- ### <span style="color: lightblue"> Repository</span>

>Repositories sind die "ausführende Kraft" der API. Der Controller leitet an das jeweilige Repository weiter, welches dann gewisse Funktionen ausführt, um mit der Datenbank bzw. in unserer API mit dem DataContext[^2] zu agieren.

- ### <span style="color: lightblue">Model</span>

>


## <a name="apiFunc" style="color:orange">Funktionen der API</a>

## <a name="database" style="color:orange">Datenbank</a>

Derzeitig besteht die *API* aus 3 Datenbanktabellen: [^1]

- ### <span style="color: lightblue">Users</span>
> Diese Tabelle wird zur ==Verwaltung von Benutzern== verwendet.
Hier werden die Daten der registrierten Benutzer aufbewahrt, die bspw. zum ==Anmelden des Benutzers== genutzt werden.
Des Weiteren werden hier auch neue Benutzer angelegt.
>
> Erreicht wird die Tabelle durch den Pfad: <span style="color:green">'/api/User'</span>
> Das Design der Tabelle sieht wie folgt aus:

| Spalte | Beschreibung | Datentyp |
| --- | --- | --- |
| id | Id des Benutzers | int (Primary Key) | 
| first_name | Vorname des Benutzers | longtext |
| last_name | Nachname des Benutzers | longtext |
| email | E-Mail-Adresse des Benutzers | longtext |
| password | Passwort des Benutzers | longtext |

---

- ### <span style="color: lightblue">Groups</span>
> Die Tabelle *Groups* wird zum ==Verwaltung von Gruppen== verwendet.
Hier werden sämtliche erstellten Gruppen gespeichert. Da beim ==Registrieren direkt eine eigene Gruppe erstellt== wird, wird diese auch direkt hier gespeichert.
>
> Erreicht wird die Tabelle durch den Pfad: <span style="color:green">'/api/Group'</span>
> Das Design der Tabelle sieht wie folgt aus:

| Spalte | Beschreibung | Datentyp |
| --- | --- | --- |
| id | Id der Gruppe | int (Primary Key) |
| name | Name der Gruppe | longtext |
| UserId | int | Die ID des Gruppenadmins (Foreign Key) |
| premium | tinyint(1) | Ob diese Gruppe den "Premium-Status" besitzt |

---

- ### <span style="color: lightblue">UsersInGroups</span>
> Die Tabelle <span style="color: lightblue">*UsersInGroups*</span> repräsentiert die ==Beziehung zwischen Benutzer und Gruppen==, also welche Benutzer in welchen Gruppen existieren. 
Dies erfolgt über eine *Many-To-Many Verbindung*, welche aussagt, dass ==ein Benutzer in mehreren Gruppen== existieren darf und dass ==eine Gruppe mehrere Benutzer== beinhalten darf.
Der Sinn dieser Tabelle ist es, nachzuverfolgen, welcher Benutzer auf welche Gruppeninhalte Zugriff hat.
>
> Erreicht wird die Tabelle durch den Pfad: <span style="color:green">'/api/UsersInGroups'</span>
> Das Design der Tabelle sieht wie folgt aus:

| Spalte | Beschreibung | Datentyp |
| --- | --- | --- |
| UserId | int | Die ID des Benutzers (Foreign Key) |
| GroupId | int | Die ID der Gruppe (Foreign Key) |






[Zurück zum Anfang](#top)

[^1]: Beispielabfragen folgen im Laufe der Dokumentation
[^2]: Eine Klasse, welche über das von EntityFramework bereitgestellte DBContextOptions die Grundstruktur der Datenbank kennt und somit handgeschriebene SQL-Abfragen obsolet macht