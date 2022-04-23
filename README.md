
# RecapCarProject

RecapCarProject (RentACar) is a car rental project where I use C# in the Backend and Angular in the Frontend, and I use advanced techniques such as OOP and AOP.

## Table of contents
* [General Info](#general-info)
* [Setup](#setup)
* [Usage](#usage)
* [Technologies Used](#technologies-used)
* [Associated Project](#associated-project)
* [Contributions](#contributions)
* [Contact](#contact)
## General Info
The project consists of 6 layers: Core, Entities, DataAccess, Business, Console UI and WebAPI. Developed with .Net Core 3.1.
## Setup

 - Press the `Win+R` " keys on your keyboard. Type `cmd` in the window
   that opens. The screen that opens should look like this.
   
   <img src="https://i.ibb.co/ngw9gzC/cmd.png"  width="600"  height="200">
	
 - You can download the project by typing `git clone https://github.com/furkaancetiin/RecapCarProject` on the line you are on.
 - Open the `RecapCarProject.sln` file in Visual Studio to run the project.
 - Enter your own database connection string in `DataAccess>Concrete>EntityFramework>RentACarContext.cs.`

   <img src="https://i.ibb.co/cY1z4Hj/VS.png"  width="600"  height="200">

 - If `dotnet ef` is not installed, run
  ```
  dotnet tool install --global dotnet-ef
  ``` 
  in Package Manager Console.
  
 - If `dotnet ef` is installed run the command 
 ```
cd DataAccess
dotnet ef database update
``` 

## Usage

 - Right click on the  `WebAPI`  project (layer) from the  `Solution Explorer`  and select  `Set as Startup Project`
 -  Start the project with  `IIS Express`  in Visual Studio. On the page that opens, type `swagger` after `https://localhost:44306/`. Your screen should look like the picture.
 
	   <img src="https://i.ibb.co/1Mv4R6t/Swagger1.png"  width="600"  height="200">
 - -Let's make an example.
 - Under the Brands heading, click on `GET /api/brands/getbyid`. Then click the `Try it out` button. Please enter `id` available in our table. Click the `Execute` button. Congratulations, our method worked!
 
	  <img src="https://i.ibb.co/6BmnW7b/VS.png"  width="600"  height="200">


 ## Technologies Used

|Id| Versions |
|--|--|
|Microsoft.AspNetCore.Http.Features | 5.0.14|
|Microsoft.EntityFrameworkCore.SqlServer | 3.1.11|
|Autofac.Extensions.DependencyInjection| 7.2.0|
|FluentValidation| 9.5.1|Core
|Autofac.Extras.DynamicProxy | 6.0.0|
|Microsoft.AspNetCore.Http    | 2.2.2|
|Microsoft.AspNetCore.Http.Abstractions     | 2.2.0|
|Autofac | 6.1.0|Core
|Microsoft.Extensions.DependencyInjection  | 5.0.1|
|Swashbuckle.AspNetCore.SwaggerGen    | 6.2.3|
|Microsoft.AspNetCore.Authentication.JwtBearer| 3.1.12|
|Swashbuckle.AspNetCore.SwaggerUI     | 6.2.3|
## Associated Project
The frontend of this project [RecapProjectFrontend](https://github.com/furkaancetiin/RecapCarProjectFrontend)
## Contributions

Thanks to dear  [Engin Demiroğ](https://github.com/engindemirog)  for his contributions.
## Contact
For your questions:
furkaanncetiin@gmail.com
[LinkedIn](https://www.linkedin.com/in/furkaancetiin/)
