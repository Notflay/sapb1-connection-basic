# 🔗 SAP Business One Service Layer Connector (C#)

Este repositorio contiene un conector básico en C# para interactuar con **SAP Business One Service Layer**.  
La idea es tener una base limpia para realizar peticiones **GET** y **POST**, junto con un esquema seguro para manejar credenciales.

---

## 📂 Estructura del proyecto

```
sapb1-connection-basic/
│── ServiceLayer.config # Archivo de configuración (ignorado en Git)
│── Helpers/
│ └── Logs.cs # Clase auxiliar para logs
│── Models/
│   ├── SBO.cs                    # Entidad de configuración de conexión SAP
│   ├── B1SLLogin.cs              # Modelo para login en Service Layer
│   └── B1SLLoginResponse.cs      # Modelo para respuesta de login
│── Services/
│   ├── ServiceLayerConnector.cs  # Conector HTTP (GET/POST con RestSharp)
│   ├── ServiceLayerEndpoint.cs   # Implementación de las operaciones con SAP
│   ├── TransactionService.cs     # Clase base para operaciones
│   └── SessionHandler.cs         # Manejo de sesión/login
│── Program.cs # Punto de entrada del proyecto
│── README.md # Este archivo
```

---

## ⚙️ Configuración

El archivo `ServiceLayer.config` (no subido al repo por seguridad) contiene las credenciales y parámetros de conexión:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<CONEXION>
	<SBO>
		<add key="SAP_SERVIDOR" value="servidor" />
		<add key="SAP_BASE" value="SBODEMOCL" />
		<add key="SAP_TIPO_BASE" value="SQL19" />
		<add key="SAP_DBUSUARIO" value="user" />
		<add key="SAP_DBPASSWORD" value="password" />
		<add key="SAP_USUARIO" value="manager" />
		<add key="SAP_PASSWORD" value="passord" />
		<add key="SL_SCHEME" value="SBODEMOCL" />
		<add key="SL_HOST" value="servidor" />
		<add key="SL_PORT" value="50000" />
		<add key="SL_BASEPATH" value="b1s/v1" />
	</SBO>
</CONEXION>
```

⚠️ Este archivo está en .gitignore para evitar exponer credenciales.

▶️ Ejecución

Clonar el repositorio:
```
git clone https://github.com/Notflay/sapb1-connection-basic.git
```

Restaurar dependencias y compilar:
```
dotnet restore
dotnet build
```

Ejecutar el proyecto:
```
dotnet run
```

```
📑 Ejemplo de uso
var connector = new ServiceLayerConnector("https://servidor:50000/b1s/v1/");
var loginResponse = connector.httpPOST("Login", "", new {
    UserName = "manager",
    Password = "password",
    CompanyDB = "SBODEMOCL"
});
```

🛡️ Notas de seguridad

Usa archivos de configuración ignorados en Git (.gitignore).

Opcionalmente, utiliza variables de entorno para credenciales en entornos productivos.

📌 Licencia

Este proyecto está bajo la licencia MIT.
Puedes usarlo y adaptarlo libremente.

---
