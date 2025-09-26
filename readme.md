SAP Business One Service Layer Connector (C#)

Este proyecto muestra c�mo conectarse a SAP Business One Service Layer desde una aplicaci�n en C#, usando RestSharp para consumir servicios REST y log4net para manejo de logs.

El objetivo es proporcionar un ejemplo simple, modular y documentado, que pueda servir como base para integraciones m�s complejas.

?? Estructura del proyecto
sapb1-connection-basic/
�-- ServiceLayer.config       	  # Archivo de configuraci�n (credenciales y par�metros de conexi�n)
�-- Helpers/
�   +-- Logs.cs                   # Clase de utilidades para manejar log4net
�-- Models/
�   +-- SBO.cs                    # Entidad de configuraci�n de conexi�n SAP
�   +-- B1SLLogin.cs              # Modelo para login en Service Layer
�   +-- B1SLLoginResponse.cs      # Modelo para respuesta de login
�-- Services/
�   +-- ServiceLayerConnector.cs  # Conector HTTP (GET/POST con RestSharp)
�   +-- ServiceLayerEndpoint.cs   # Implementaci�n de las operaciones con SAP
�   +-- TransactionService.cs     # Clase base para operaciones
�   +-- SessionHandler.cs         # Manejo de sesi�n/login
�-- Program.cs                    # Clase principal, entry point
�-- README.md                     # Documentaci�n del proyecto
�-- .gitignore                    # Exclusi�n de archivos sensibles

?? Configuraci�n

El archivo ServiceLayer.config contiene los par�metros de conexi�n.
Ejemplo:

<Connections>
  <SBO>
    <add key="SAP_SERVIDOR" value="servidor-sap" />
    <add key="SAP_BASE" value="SBODEMOCL" />
    <add key="SAP_TIPO_BASE" value="MSSQL2019" />
    <add key="SAP_DBUSUARIO" value="sa" />
    <add key="SAP_DBPASSWORD" value="password-db" />
    <add key="SAP_USUARIO" value="manager" />
    <add key="SAP_PASSWORD" value="password-sap" />
    <add key="SL_SCHEME" value="https" />
    <add key="SL_HOST" value="servidor-sap" />
    <add key="SL_PORT" value="50000" />
    <add key="SL_BASEPATH" value="b1s/v1/" />
  </SBO>
</Connections>


?? Nota: El archivo ServiceLayer.config debe estar en .gitignore para no exponer credenciales.

?? Ejecuci�n

Clonar el repositorio:

git clone https://github.com/usuario/sapb1-connection-basic.git


Configurar ServiceLayer.config con tus credenciales SAP B1.

Compilar y ejecutar:

dotnet build
dotnet run

??? Funcionalidades principales

?? Login autom�tico al Service Layer y obtenci�n de SessionId.

?? M�todos gen�ricos httpGET y httpPOST para consumir recursos de SAP B1.

?? Manejo de logs con log4net (request, response, errores).

? Dise�o modular con separaci�n en capas:

Connector ? Comunicaci�n HTTP

TransactionService ? L�gica de sesi�n y operaciones

Endpoint ? Extensiones para GET/POST espec�ficos

?? Ejemplo de uso
Obtener lista de art�culos
var service = new ServiceLayerEndpoint();
var items = service.Get<List<Item>>("Items?$top=5");
foreach (var item in items)
{
    Console.WriteLine($"{item.ItemCode} - {item.ItemName}");
}

?? Roadmap (ideas futuras)

 Agregar pruebas unitarias con xUnit

 Implementar m�todos PUT y DELETE

 Manejo seguro de credenciales con dotnet user-secrets

 Dockerfile para levantar un microservicio de integraci�n

?? Licencia

Este proyecto se distribuye bajo la licencia MIT.