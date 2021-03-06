# MVVMExample
MVVM Prototype for Unity3d

MVVM Example based off of this gist : https://gist.github.com/NVentimiglia/3b0e651d2a902fa722d516e06e8c2c60

Verbage :

- Controller : Static, Architecturall, Only One.
  - Examples : Inject Controller, Game Controller
  - Should be used for sparingly, as the static reference can make it hard to test.
  - Somewhat replaced by services and ViewModels

- Logic : Game / Business Logic. 
  - Logic includes 'services' and 'models' and 'interfaces' and 'view models'
  - Examples : Error Logic, Score Logic, Account Logic, Room Logic
  - No unity dependencies
  - Testable, should live in a DLL
  
- Models : Simple DTOS used by a service
  - Should be mostly fields / properties. Some methods are ok (ToString())
  - Account Model, Score Model, FileInfo

- ViewModels : The View's Model 
  - AKA ViewControllers (on IOS) 
  - logical proxy class that make services / models easier to use (for a view)
  - Implement Observable to enable data binding
  - Should be optional for simple logic (AccountLogic does not need one, but, complex game logic might)
  - Example BingoViewModel, LobbyViewController
  
- Interfaces (Contracts) : Describes the functionality of a service
 - Examples : (IAccountService, IBingoService)
  
- Services : Implementations a contract
 - Examples : MockAccountService, SmartFoxAccountService, RestApiAccountService
 
- Infrastructure : A service which implements a hardware dependency
 - Mock, SmartFox, RestApi, SQLITE, Parse, JsonDB, CouchBase
 - Internal implementation should be  hidden agnosstic (service) contract
 - JsonDatabase, SQLLITEDatabase, SmartFoxConnection, PusherConnection

- Views : Presents someting to the user
  - Imports Logic (Viewmodels, models, services, ect) 
  - May or may not be observable.
  - Unity dependencies ok
