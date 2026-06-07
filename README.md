Proyectos simples de Web
-------------------Comandos --------------------------------


dotnet ef migrations add NombreMigracion
2. Aplicar migración a la BD
dotnet ef database update
3. (Opcional) Eliminar última migración
dotnet ef migrations remove

-----------EJECUTAR BACKEND (.NET)
Ejecutar normal
dotnet run
Ejecutar con recarga automática
dotnet watch run

-----------FRONTEND (React/Vite con pnpm)
Instalar dependencias (una vez)
pnpm install
Ejecutar frontend
pnpm dev
