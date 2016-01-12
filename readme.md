# Codetable Toolbox

The Codetable Toolbox offers following functionalities for ASP.NET 5 Web Api projects :

- Generic base classes to deal with code tables in a uniform way.
- Create a codetable discovery framework that adds an endpoint on which the list of Codetables can be retrieved.

## Installation
Adding the toolbox to a project can be done via the NuGet Package Manager in Visual Studio or by adding the package to the project.json :

``` json
 "dependencies": {
    ...,
    "Toolbox.Codetable":  "1.1.3",
    ...
 }
```

### Setup Codetable Discovery Framework

At startup, the Code Table Discovery Framework goes through the list of controllers indicated as Code Table via a custom attribute (see below) via reflection. It adds an endpoint to the Web API where this list of Codetables can be requested. The default URL of the endpoint is **_admin/CodeTable_**, but this can be changed through the options at startup.
This endpoint can be used for example to make the list of codetables available to a generic management screen.

The framework is added to an ASP.NET project in the **_Startup_** class. First the necessary services are configured in the Configure Services Startup method of the class :

``` csharp
  services.AddCodetableDiscovery();
```

In the Configure method of the startup class, the framework is launched and you can pass options as an argument. If you are satisfied with the default options, simply pass **_CodetableDiscoveryOptions.Default_** :

``` csharp
   app.UseCodetableDiscovery(CodetableDiscoveryOptions.Default);
```

Passing deviating options can be done by instantiating a new Code Table Discovery Options object and fill in the necessary properties :

``` csharp
   app.UseCodetableDiscovery(new CodetableDiscoveryOptions() { Route = "api/myCodetables" });
```

Following options can be set :

Option              | Description                                                | Default
------------------ | ----------------------------------------------------------- | --------------------------------------
Route              | the url where the list of codetables can be requested | admin/Codetable
ControllerAssembly | the assembly in which the codetable controllers can be searched | the assembly that contains the Startup class  


### Installation of DataAccess
This toolbox has a dependency on the Toolbox.DataAccess package which must also be set in the startup.

For detailed instructions, refer to the ReadMe.md of the Toolbox.DataAccess repository on Github: https://github.com/digipolisantwerp/dataaccess_aspnet5


## Base Classes

### CodetableEntityBase

From this base class CodeTable entities can be inherited. It provides a uniform method for dealing with Codetables. It contains the following properties :

Naam         | Type        | Required
------------ | ----------- | ---------
Id           | int         | key
Code         | string(50)  | required  
Value       | string(100) | required
Description | string(250) | optional         
Serialnumber   | int         | required  
Disabled     | bool        | required


``` csharp
using Toolbox.Codetable.Entities;
    namespace xxx.Entities
	{
	    public class Theme : CodetableEntityBase {    }
	}
```

### CodetableModelBase

This base class can be used to inherit from for the CodeTable models. She has the same properties as the CodeTable EntityBase class.


``` csharp
using Digipolis.Codetable.Models;
    namespace xxx.Api.Models
	{
	    public class Theme : CodetableModelBase {    }
	}
```

### CodetableControllerBase

This generic base class provides CRUD endpoints for the CodeTable. The example below shows a fully developed controller for a CodeTable 'Theme'

``` csharp
    [Route("api/[Controller]")]
    [CodetableController]
    public class ThemeController : CodetableControllerBase<Entities.Theme, Api.Models.Theme>
    {
        public ThemeController(IServiceCollection collection) : base(collection)
        {
        }
    }
```

To see this as a working example an AutoMapper configuration should be added
``` csharp
    Mapper.CreateMap<Entities.Theme, Api.Models.Theme>();
```

### CodetableControllerAttribute
If this attribute is used with a controller that exposes a CodeTable on the API, the discovery framework can recognize and add it to the list. The above example illustrates this for the theme CodeTable.
By default, the name of the controller (without controller) is taken as the name of the CodeTable (in the above example **_theme_**). You can also give a different name to the attribute :

``` csharp
[CodetableController("DisplayTheme")]
public class ThemeController
{
   ...
}
```
This generates **DisplayTheme** as the name for the Codetable.


### CodetableInfo
The api endpoint returns a list of objects of the type **_CodetableInfo_**. CodetableInfo contains the following properties :

Property | Type | Description
-------- | ------ | -------------------------
Name     | string | The name of the Codetable.
Route    | string | The url of the Codetable.


### ICodetableProvider
If you need the list of Codetables in the application itself or if the default supplied endpoint is not enough, you can directly address the ICodetableProvider. The provider is registered in the ASP.NET dependency container during startup and you can then inject this in your own code :

``` csharp
public class MyController
{
   public MyController(ICodetableProvider CodetableProvider)
   {
      _CodetableProvider = CodetableProvider;
   }
   ...
}
```
After that the list is available via the **_Codetables_** property :

``` csharp
   var Codetables = _CodetableProvider.Codetables;
```


# Versions

Versie | Author                                  | Description
------ | ----------------------------------------| --------------------------------------------------------------------------
1.0.12 | Steven Vanden Broeck                    | Initiële versie. CodetableDiscovery en base classes.
1.1.1  | Sven Noreillie				 | Uitwerking ControllerBase, Generic readers & writers, Access naar database
1.1.2  | Koen Stroobants				 | Translation to English
