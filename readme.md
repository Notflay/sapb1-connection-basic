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
<?xml version="1.0" encoding="utf-8" ?>
<ServiceLayerConfig>
  <BaseURL>https://servidor:50000/b1s/v1/</BaseURL>
  <CompanyDB>SBODEMOCL</CompanyDB>
  <UserName>manager</UserName>
  <Password>password</Password>
</ServiceLayerConfig>
```

⚠️ Este archivo está en .gitignore para evitar exponer credenciales.

▶️ Ejecución

Clonar el repositorio:
```
git clone https://github.com/tuusuario/sapb1-connection-basic.git
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
