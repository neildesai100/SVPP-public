Welcome to the SVPP Data Hub Application!

The Data Hub serves as support tool for SVPP in managing Partners, Nonprofits, and Guests. 

The application is currently deployed to Microsoft Azure. When the application is updated, updates must be published.
  To deploy new versions of the application, please see this tutorial: 
    https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-webapp-using-vs?view=aspnetcore-3.1

Outside of deployment, there are a few features of the application which may be changed as SVPP evolves:

  List of Partner Skills: 
    To change the available dropdown selection of Partner Skills, 
      Navigate to SVPP/Controllers/PartnersController.cs
        Search for the "GetAllSkills" function
          Change the list of skills, surrounding each skill with "" and separating each skill with a comma
      Navigate to SVPP/Controllers/NonprofitsController.cs
        Search for the "GetAllSkills" function
          Change the list of skills, surrounding each skill with "" and separating each skill with a comma
  
  List of Nonprofit Focus Areas:
    To change the available dropdown selection of Nonprofit Focus Areas, 
      Navigate to SVPP/Controllers/PartnersController.cs
        Search for the "GetAllPreferences" function
          Change the list of focus areas, surrounding each focus area with "" and separating each focus area with a comma
      Navigate to SVPP/Controllers/NonprofitsController.cs
        Search for the "GetAllPreferences" function
          Change the list of skills, surrounding each focus area with "" and separating each focus area with a comma
          
To run the application locally:
  Download Microsoft Visual Studio (Complete the Prerequisite Step: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-3.1&tabs=visual-studio#prerequisites)
  Git clone the database (Tutorial: https://git-scm.com/book/en/v2/Git-Basics-Getting-a-Git-Repository)
  Add the necessary NuGet Packages (Complete the Add NuGet Packages Step: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-3.1&tabs=visual-studio#add-nuget-packages)
  Initialize the Database
    Complete the Initialize Database Step: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-3.1&tabs=visual-studio#initial-migration
    However, run these commands with BOTH the SVPPContext then the DatabaseContext
    (e.g. in the Package Manager Console -- note that you can replace initDB and initSVPP with any strings you want:
      Add-Migration initDB -Context DatabaseContext
      Add-Migration initSVPP -Context SVPPContext
      Update-Database -Context DatabaseContext
      Update-Database -Context SVPPContext)
  Run the Application (Click Debug > Start Debugging on the top toolbar)


    
