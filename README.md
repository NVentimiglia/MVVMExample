# MVVMExample
MVVM Prototype for Unity3d

MVVM Example based off of this gist : https://gist.github.com/NVentimiglia/3b0e651d2a902fa722d516e06e8c2c60

Verbage :

- Controller : Static, Architecturall, Only One.
  - Examples : Inject Controller, Game Controller

- Logic : Game / Business Logic. Logic encapsilates 'services' and 'models' and 'interfaces'
  - Examples : Error Logic, Score Logic, Account Logic, Room Logic
  
- Models : Simple DTOS used by a service
  - Should be mostly fields / properties. Some methods are ok (ToString())
  - Account Model, Score Model, FileInfo
  
- Interfaces (Contracts) : Describes the functionality of a service
 - Examples : (Mock, SmartFox, Local, Http)
  
- Services : Implementations a contract
 - Examples : MockAccountService, SmartFoxAccountService, RestApiAccountService
 
- Infrastructure : A service which implements a hardware dependency
 - Mock, SmartFox, RestApi, SQLITE, Parse, JsonDB, CouchBase
 - Internal implementation should be  hidden agnosstic contract