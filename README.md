# Inmobiliary

```Bash
├── Inmobiliaria-backend
│   ├── webInmobiliary.Api
│   │   ├── .config
│   │   │   └── dotnet-tools.json
│   │   ├── Controllers
│   │   │   ├── AuthController.cs
│   │   │   └── PropertyControlle.cs
│   │   ├── Properties
│   │   │   └── launchSettings.json
│   │   ├── Program.cs
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── webInmobiliary.Api.csproj
│   │   └── webInmobiliary.Api.http
│   ├── webInmobiliary.Application
│   │   ├── Dto
│   │   │   ├── LoginRequest.cs
│   │   │   ├── PropertyCreateDto.cs
│   │   │   ├── PropertyUpdateDto.cs
│   │   │   └── RegisterRequest.cs
│   │   ├── Interfaces
│   │   │   ├── ILoginService.cs
│   │   │   ├── IPasswordHasher.cs
│   │   │   └── IPropertyService.cs
│   │   ├── Services
│   │   │   ├── LoginService.cs
│   │   │   ├── PasswordHasher.cs
│   │   │   └── PropertyService.cs
│   │   └── webInmobiliary.Application.csproj
│   ├── webInmobiliary.Domain
│   │   ├── Entities
│   │   │   ├── Admin.cs
│   │   │   ├── Client.cs
│   │   │   ├── Property.cs
│   │   │   ├── Role.cs
│   │   │   └── User.cs
│   │   ├── Interfaces
│   │   │   ├── IAdminRepository.cs
│   │   │   ├── IPropertyRepository.cs
│   │   │   └── IUserRepository.cs
│   │   └── webInmobiliary.Domain.csproj
│   ├── webInmobiliary.Infrastructure
│   │   ├── Data
│   │   │   └── AppDbContext.cs
│   │   ├── Extensions
│   │   │   └── ServiceCollectionExtensions.cs
│   │   ├── Migrations
│   │   │   ├── 20251114120630_InitialCreation.Designer.cs
│   │   │   ├── 20251114120630_InitialCreation.cs
│   │   │   └── AppDbContextModelSnapshot.cs
│   │   ├── Repositories
│   │   │   ├── AdminRepository.cs
│   │   │   ├── PropertyRepository.cs
│   │   │   └── UserRepository.cs
│   │   └── webInmobiliary.Infrastructure.csproj
│   ├── .gitignore
│   ├── README.md
│   └── webInmobiliary.sln
```


##  Lista de Endpoints

  ---------------------------------------------------------------------------
Método           Endpoint                    Descripción
  ---------------- --------------------------- ------------------------------
POST             `/api/Auth/register`        Registrar un nuevo usuario

POST             `/api/Auth/login`           Iniciar sesión y obtener
tokens

POST             `/api/Auth/refresh-token`   Solicitar un nuevo Access
Token usando Refresh Token

POST             `/api/Auth/revoke-token`    Invalidar un Refresh Token
---------------------------------------------------------------------------

------------------------------------------------------------------------

# 1.  Registro de Usuario

### **POST `/api/Auth/register`**

###  **Body (JSON)**

``` json
{
  "name": "Juan Perez",
  "email": "juan@example.com",
  "password": "MiPassword123",
  "role": 0,
  "phone": "3001234567"
}
```

###  **Campos**

Campo      Tipo                    Descripción
  ---------- ----------------------- --------------------
name       string                  Nombre del usuario
email      string                  Correo único
password   string                  Contraseña
role       int (0=Admin, 1=User)   Rol asignado
phone      string                  Teléfono

###  **Respuesta 200 OK**

``` json
{
  "message": "User registered successfully"
}
```

### **Uso desde Frontend**

Ejemplo en **JavaScript / fetch**:

``` js
const res = await fetch("https://TU_API/api/Auth/register", {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify({
    name: nameInput,
    email: emailInput,
    password: passInput,
    role: 1,
    phone: phoneInput
  })
});
```

------------------------------------------------------------------------

# 2. Login

### **POST `/api/Auth/login`**

###  Body (JSON)

``` json
{
  "email": "juan@example.com",
  "password": "MiPassword123"
}
```

### ️ Respuesta esperada

``` json
{
  "accessToken": "jwt_token_aqui",
  "refreshToken": "refresh_token_aqui",
  "expiresIn": 3600
}
```

### Uso desde Frontend

``` js
const res = await fetch("/api/Auth/login", {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify({ email, password })
});
const data = await res.json();

localStorage.setItem("accessToken", data.accessToken);
localStorage.setItem("refreshToken", data.refreshToken);
```

------------------------------------------------------------------------

# 3. Refresh Token

### **POST `/api/Auth/refresh-token`**

Se usa cuando el **access token expira**.

### Body

``` json
{
  "refreshToken": "el_token_guardado_en_localstorage"
}
```

### Respuesta

``` json
{
  "accessToken": "nuevo_jwt",
  "refreshToken": "nuevo_refresh_token"
}
```

### Lógica recomendada en Frontend

``` js
async function refreshToken() {
  const token = localStorage.getItem("refreshToken");

  const res = await fetch("/api/Auth/refresh-token", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ refreshToken: token })
  });

  const data = await res.json();

  localStorage.setItem("accessToken", data.accessToken);
  localStorage.setItem("refreshToken", data.refreshToken);
}
```

------------------------------------------------------------------------

# 4.  Revocar Token

### **POST `/api/Auth/revoke-token`**

Deshabilita un refresh token específico.

###  Body

``` json
{
  "refreshToken": "token_a_revocar"
}
```

### Respuesta esperada

``` json
{
  "message": "Refresh token revoked"
}
```

------------------------------------------------------------------------

#  ¿Cómo usa el frontend estos endpoints?

## Login

→ Guardar JWT y Refresh token.

## Peticiones protegidas

→ Enviar `Authorization: Bearer <accessToken>`.

## Si expira

→ Llamar `/refresh-token`.

## Logout

→ Llamar `/revoke-token` y borrar tokens del storage.

------------------------------------------------------------------------

# Ejemplo general de llamada protegida

``` js
async function getProtectedData() {
  const token = localStorage.getItem("accessToken");

  const res = await fetch("/api/protected-route", {
    headers: { Authorization: `Bearer ${token}` }
  });

  if (res.status === 401) {
    await refreshToken();
    return getProtectedData();
  }

  return res.json();
}
```

------------------------------------------------------------------------

# librerias 
El proyecto "webInmobiliary.Api" tiene las referencias de paquete siguientes
[net8.0]:
Paquete de nivel superior                       Solicitado   Resuelto
> Microsoft.EntityFrameworkCore.Design          8.0.0        8.0.0   
> Microsoft.EntityFrameworkCore.Relational      8.0.0        8.0.0   
> Microsoft.IdentityModel.Logging               7.5.1        7.5.1   
> Microsoft.IdentityModel.Tokens                7.5.1        7.5.1   
> Swashbuckle.AspNetCore                        6.6.2        6.6.2   
> System.IdentityModel.Tokens.Jwt               7.5.1        7.5.1

El proyecto "webInmobiliary.Application" tiene las referencias de paquete siguientes
[net8.0]:
Paquete de nivel superior                                    Solicitado   Resuelto
> Microsoft.EntityFrameworkCore                              8.0.0        8.0.0   
> Microsoft.Extensions.Configuration.Abstractions            8.0.0        8.0.0   
> Microsoft.Extensions.DependencyInjection.Abstractions      8.0.0        8.0.0   
> Microsoft.Extensions.Primitives                            8.0.0        8.0.0   
> Microsoft.IdentityModel.Tokens                             7.5.1        7.5.1   
> System.IdentityModel.Tokens.Jwt                            7.5.1        7.5.1

El proyecto "webInmobiliary.Domain" tiene las referencias de paquete siguientes
[net8.0]:
Paquete de nivel superior                 Solicitado   Resuelto
> Microsoft.Extensions.Identity.Core      8.0.0        8.0.0

El proyecto "webInmobiliary.Infrastructure" tiene las referencias de paquete siguientes
[net8.0]:
Paquete de nivel superior                              Solicitado   Resuelto
> BCrypt.Net-Next                                      4.0.3        4.0.3   
> Microsoft.AspNetCore.Authentication.JwtBearer        8.0.0        8.0.0   
> Microsoft.EntityFrameworkCore                        8.0.0        8.0.0   
> Microsoft.EntityFrameworkCore.Design                 8.0.0        8.0.0   
> Microsoft.EntityFrameworkCore.Tools                  8.0.0        8.0.0   
> Microsoft.Extensions.Configuration.Abstractions      10.0.0       10.0.0  
> Microsoft.IdentityModel.Logging                      7.5.1        7.5.1   
> Microsoft.IdentityModel.Tokens                       7.5.1        7.5.1   
> Pomelo.EntityFrameworkCore.MySql                     8.0.0        8.0.0   
> System.IdentityModel.Tokens.Jwt                      7.5.1        7.5.1

# Mostar todas las libreirar y verisones que se aplican en el proyecto 
    - dotnet list package

# Cloudinary 

    - Instalamos en infrastructure:

        dotnet add package CloudinaryDotNet

    - crear interface en domain 