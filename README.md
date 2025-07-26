# 📦 Proyecto Full Stack (.NET 9 + Angular + SQL Server)

Este repositorio contiene un sistema creado con:

- 🖥 Backend: API REST construida con .NET 9
- 🌐 Frontend: Interfaz de usuario hecha con Angular.
- 🌐 Frontend: Uso de librerias como Material y bootstrap para elementos y estilos.
- 🗄 Base de Datos: Scripts de SQL Server para crear y poblar la base de datos

---

## Aplicativo web
<img width="1355" height="312" alt="imagen" src="https://github.com/user-attachments/assets/20bacbd4-0cd2-4723-90ff-1bb1d8d35202" />

*Página de productos disponibles*

<img width="407" height="292" alt="imagen" src="https://github.com/user-attachments/assets/ccf3947b-7488-4a2b-b7ae-7107e7a4d957" />

*Modal con inputs para movimiento de inventario*

<img width="1340" height="304" alt="imagen" src="https://github.com/user-attachments/assets/d51e24f0-8862-4694-b599-52f8276e8829" />

*Página de productos fuera del stock*


✅ Requisitos

Asegúrate de tener instaladas las siguientes herramientas:

| Herramienta       | Versión Requerida |
|-------------------|-------------------|
| [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) | .NET 9.0 Preview o superior |
| [Node.js](https://nodejs.org/)        | 18.x o superior |
| [NPM](https://www.npmjs.com/)         | 9.x o superior |
| [SQL Server](https://www.microsoft.com/en-us/sql-server/) | Express o superior |
| [Visual Studio Code](https://code.visualstudio.com/) | (opcional) |

---


## 🔨 1. Aplicar la migración existente

### 📁 Ubicación:
`/backend/FinanzautoAPI`

Puedes crear la primera migración que carga datos semilla con:
   ```bash
  dotnet ef migrations add InitialCreate
   ```

Este paso creará la base de datos con las tablas definidas por Entity Framework.
   ```bash
  dotnet ef database update
   ```
Si no tienes instalado dotnet-ef, puedes agregarlo con:
   ```bash
  dotnet tool install --global dotnet-ef
   ```
Puedes ejecutar el proyecto con:
   ```bash
  dotnet run
   ```


---

## ⚙️ 2. Ejecutar el Backend .NET 9

### 📁 Ubicación:
`/backend/ITSenseAPI`

Pasos:

1. Abre la terminal en la carpeta del backend.
2. Restaura los paquetes:
   ```bash
   dotnet restore

3. Ejecuta la API con el comando:
   ```bash
   dotnet run
   ```

### ⚙️ `Appsettings.json`

Configura `appsettings.json` con tu cadena de conexión a SQL Server. Es importante tener en cuenta que el nombre de la base de datos es `ITSenseDB` una vez se ejecuten los scripts.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ITSenseDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```
---

## 🌐 3. Ejecutar la aplicación para usuarios (Angular)

### 📁 Ubicación:
`/frontend/inventory-app`

Pasos:
1. Abre la terminal en la carpeta del frontend.
2. Instala dependencias:
  ```bash
  npm install
  ```
4. Ejecuta el servidor de desarrollo:
  ```bash
  ng serve
  ```
5. Abre en tu navegador:
  http://localhost:4200

