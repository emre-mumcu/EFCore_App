```bash

#Dotnet-ef Tools
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

#Design Component
dotnet add package Microsoft.EntityFrameworkCore.Design

#Providers
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package MySql.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.EntityFrameworkCore.InMemory


dotnet ef database update [-p <ProjectHavingDbContext> -s <StartupProject> -o PathToMigrations]
dotnet ef database drop [-p <ProjectHavingDbContext> -s <StartupProject> ]
dotnet ef migrations remove [-p <ProjectHavingDbContext> -s <StartupProject> ]

#Database First
dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=MyDatabase;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
dotnet ef dbcontext scaffold "Server=127.0.0.1;Port=5454;Database=mumcu;User Id=postgres;Password=aA123456;" Npgsql.EntityFrameworkCore.PostgreSQL -o Models


#Code First
dotnet ef migrations add Init -o AppLib/Data/Migrations
dotnet ef database update


https://www.learnentityframeworkcore.com/
https://www.entityframeworktutorial.net/


```


ServiceProvider serviceProvider = new ServiceCollection()
    .AddDbContext<SampleDbContext>(options => options.UseNpgsql(connectionString: "Server=127.0.0.1;Port=5454;Database=sample;User Id=postgres;Password=aA123456;"))
    .BuildServiceProvider();


//IConnection connection = serviceProvider.GetService<IConnection>();
//ICommand command = serviceProvider.GetService<ICommand>();


    // dotnet add package Newtonsoft.Json
    dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation