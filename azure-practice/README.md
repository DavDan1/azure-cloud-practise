# Canine Cloud - Stepping into to the cloud

## A. Scenario

It finally happened. After years of negligence the pre-Y2K server in the basement has died. Not much of value was lost, but one thing of importance, the website used by the neighborhoods local dog shelter website, **Canine Cloud**, is now gone.

The pictures, the website, all gone... But we can rebuild it! We have the technology! We can make it better than it was. Better, stronger, faster. We can remake it, in the cloud!

## B. What you will be working on

You will be creating the start of Canine Cloud, the replacement to the old static 'HTML-only' website previously used by the neighboring dog shelter. 

We will work on this application throughout the week and refine it step by step. With the power of the cloud with quick turnarounds and deployments!

The initial goal is to create a WebAPI with connection to an Azure database, while also using and Azure Blob storage.

## C. Setup

Ensure that you have created an Azure account with a free subscription. At least one active per mob. You will need to be able to set up storage like it will be mentioned day 2 the in slides.

A word of caution! We are creating resources that are publicly available on the internet this time and with this, it's important to not make sensitive information such as connection strings and passwords public. One way of keeping things secure during development is to use `dotnet user-secrets`  (https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

To set this up, run the following commands:
```
dotnet user-secrets init
dotnet user-secrets set "KeyName" "<YourSecretKey>"
```

With the secrets setup locally, you will have then to add them then to the Azure applications App Settings later when the application is deployed.

## D. Lab instructions

### Day 1 - Individual practice:

Using the Azure-CLI, follow the instructions from the slides or [tutorial](https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api) and deploy a WebApp to Azure.

- Make sure that CORS works for local as well as cloud development, see this <https://docs.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api#add-cors-functionality>
- With everything up and running, delete the now created resource group: ``az group delete --name myResourceGroup``

With this done, try to do the same as a mob with your tool of choice. You are free to use VS Code, Rider, Azure Portal or any other you see fit.

Since Azure is a service, the goal of today is to familiarize yourself with the environment. See if you can get [GitHub Actions](https://learn.microsoft.com/en-us/azure/app-service/deploy-github-actions?tabs=applevel) working! And see if you can deploy your HackDay project! Your front-end can be deployed either on GitHub pages or as a [Static Web App](https://learn.microsoft.com/en-us/azure/static-web-apps/overview) on Azure. You just need to remember to build the SPA instead of just running it in dev mode.

### Day 2 - Canine Cloud

- Create a WebAPI application, to keep things simple
- After you have created this empty application, immediatly move it to the cloud!
- Create a Azure SQL database on Azure with a name such as CanineDB, this will be the main database used to store all the data about dogs. make sure you pick the correct free one!
- Connect the WebAPI to the newly created database and setup a Dog entity with fitting fields (name, birth-year etc.). The dog shelter is trusting you with the logic on what fields should be added!
- Create a blob container for your photos and write the boilerplate code needed to upload the photos to it.
  - You might find this [tutorial helpful](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images)
- Setup a controller so that you can GET, POST and DELETE dogs from the database.

Good luck and have fun!
