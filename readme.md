# Codetabel Toolbox

De Codetabel Toolbox biedt volgende functionaliteiten aan voor ASP.NET 5 Web Api projecten :

- generieke base classes om met codetabellen om te gaan op een uniforme manier.
- een codetabel discovery framework dat een endpoint toevoegt waarop de lijst van codetabellen kan opgevraagd worden.

## Installatie
De toolbox toevoegen aan een project kan gedaan worden via de NuGet Package Manager in Visual Studio of door het package toe te voegen aan de project.json :

``` json
 "dependencies": {
    ...,
    "Digipolis.Codetabel":  "1.1.1", 
    ...
 }
```

### Codetabel Discovery Framework instellen

Het Codetabel Discovery Framework gaat bij het opstarten via reflectie de lijst van controllers overlopen die als codetabel aangegeven zijn via een custom attribute (zie lager). Het voegt een endpoint toe aan de Web API waar deze  lijst van codetabellen kan opgevraagd worden. De standaard url van het endpoint is **_admin/codetabel_**, maar dit kan gewijzigd worden via de opties bij het opstarten.
Dit endpoint kan bvb gebruikt worden om de lijst van codetabellen beschikbaar te maken aan een generiek beheerscherm.

Het framework wordt toegevoegd aan een ASP.NET 5 project in de **_Startup_** class. Eerst worden de nodige service configureert in de ConfigureServices method van de Startup class :

``` csharp
  services.AddCodetabelDiscovery();
```

In de Configure method van de Startup class, wordt het framework opgestart en kunnen opties meegegeven worden als argument. Als je tevreden bent met de standaard opties, volstaat het **_CodetabelDiscoveryOptions.Default_** mee te geven : 

``` csharp
   app.UseCodetabelDiscovery(CodetabelDiscoveryOptions.Default);
```

Afwijkende opties mee geven, kan door een nieuw CodetabelDiscoveryOptions object te instantiëren en de nodige properties inte vullen :

``` csharp
   app.UseCodetabelDiscovery(new CodetabelDiscoveryOptions() { Route = "api/mijncodetabellen" });
```

Volgende opties kunnen ingesteld worden :

Optie              | Omschrijving                                                | Default
------------------ | ----------------------------------------------------------- | --------------------------------------
Route              | de url waar de lijst van codetabellen kan opgevraagd worden | admin/codetabel
ControllerAssembly | de assembly waarin naar codetabel controllers wordt gezocht | de assembly die de Startup class bevat


### Installatie van DataAccess
Deze toolbox heeft een dependency op het Digipolis.DataAccess package welke ook ingesteld moet zijn in de startup. 

In de ConfigureServices method van de **_Startup_** class dient de DataAccess geregistreerd te worden met bijhorende DataAccessOptions en EntityContext voor het project

``` csharp
    ConnectionString dataConnectionString = new ConnectionString(config.DatabaseCongfiguration.Host, config.DatabaseCongfiguration.Port, config.DatabaseCongfiguration.Name, config.DatabaseCongfiguration.Userid, config.DatabaseCongfiguration.Password);
   DataAccessOptions dataOptions = new DataAccessOptions(dataConnectionString);
   services.AddDataAccess<DataAccess.Context.EntityContext>(dataOptions);
```

In de Configure method van de Startup class worden de configuratieinstellingen geladen voor EntityFramework. Men kan een DbConfiguration meegeven aan de UseDataAccess method, deze parameter is standaard een instantie van de **_PostgresDbConfiguration_** class welke hieronder beschreven staat.

``` csharp
    app.UseDataAccess();
```

De PostgresDbConfiguration class
``` csharp
    public class PostgresDbConfiguration : DbConfiguration
    {
        public PostgresDbConfiguration()
        {
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
            SetProviderFactory("Npgsql", Npgsql.NpgsqlFactory.Instance);
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
        }
    }
```

Volg de instructies in de ReadMe van Digipolis.DataAccess voor meer info. 


## Base Classes

### CodetabelEntityBase

Van deze base class kunnen codetabel entities overgeërfd worden. Ze biedt een uniforme manier om met codetabellen om te gaan. Ze bevat volgende properties :

Naam         | Type        | Verplicht 
------------ | ----------- | --------- 
Id           | int         | key 
Code         | string(50)  | required  
Waarde       | string(100) | required 
Omschrijving | string(250) | optioneel         
Volgnummer   | int         | required  
Disabled     | bool        | required 


``` csharp
using Digipolis.Codetabel.Entities;
    namespace xxx.Entities
	{
	    public class Thema : CodetabelEntityBase {    }
	}
```

### CodetabelModelBase

Deze base class kan gebruikt worden om van over te erven voor de codetabel models. Ze heeft dezelfde properties als de CodetabelEntityBase class. 

``` csharp
using Digipolis.Codetabel.Models;
    namespace xxx.Api.Models
	{
	    public class Thema : CodetabelModelBase {    }
	}
```

### CodetabelControllerBase

Deze generieke base class voorziet in CRUD endpoints voor de codetabel. Onderstaand voorbeeld toont een volledig uitgewerkte controller voor een codetabel 'Thema'

``` csharp
    [Route("api/[Controller]")]
    [CodetabelController]
    public class ThemaController : CodetabelControllerBase<Entities.Thema, Api.Models.Thema>
    {
        public ThemaController(IServiceCollection collection) : base(collection)
        {
        }
    }
```

Om dit als werkend voorbeeld te zien dient een Automapper configuratie worden toegevoegd
``` csharp
    Mapper.CreateMap<Entities.Thema, Api.Models.Thema>();
```

### CodetabelControllerAttribute
Als dit attribute gebruikt wordt bij een controller die een codetabel beschikbaar maakt op de API, kan het discovery framework deze herkennen en toevoegen aan de lijst. Bovenstaand voorbeeld toont dit voor de thema codetabel.
Standaard wordt de naam van de controller (zonder Controller) genomen voor de naam van de codetabel (in bovenstaand voorbeeld is dat **_Thema_**). Je kan ook een afwijkende naam meegeven met het attribute :

``` csharp
[CodetabelController("VoorstellingsThema")]
public class ThemaController
{
   ...
}
```
Dit geeft **_VoorstellingsThema_** als naam voor de codetabel.


### CodetabelInfo
Het api endpoint geeft een lijst van objecten van type **_CodetabelInfo_** terug. CodetabelInfo bevat volgende properties :

Property | Type | Omschrijving
-------- | ------ | -------------------------
Naam     | string | De naam van de codetabel.
Route    | string | De url van de codetabel. 


### ICodetabelProvider
Als je in de toepassing zelf nood hebt aan de lijst van codetabellen of als het standaard meegeleverd endpoint niet volstaat, kan je de ICodetabelProvider rechtstreeks aanspreken. De provider is geregistreerd in de dependency container van ASP.NET tijdens het opstarten en je kan deze dus injecteren in jouw eigen code :

``` csharp
public class MijnController
{
   public MijnController(ICodetabelProvider codetabelProvider)
   {
      _codetabelProvider = codetabelProvider;
   }
   ...
}
```
Daarna heb je de lijst beschikbaar via de **_Codetabellen_** property :

``` csharp
   var codetabellen = _codetabelProvider.Codetabellen;
```


# Versies

Versie | Auteur                                  | Omschrijving
------ | ----------------------------------------| --------------------------------------------------------------------------
1.0.12 | Steven Vanden Broeck                    | Initiële versie. CodetabelDiscovery en base classes.
1.1.1  | Sven Noreillie				 | Uitwerking ControllerBase, Generic readers & writers, Access naar database	

