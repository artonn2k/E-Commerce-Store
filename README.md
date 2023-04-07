# Ecommerce Project with C# .NET and React TypeScript

# My project is online now
You can click and visit my project [onlinestore.fly.dev](https://onlinestore.fly.dev/)


This is an ecommerce project built with C# .NET and React TypeScript. The project is intended to provide a fully functional ecommerce website 
here customers can browse products, add products to their cart, make payments, and receive orders. I did this project to implement my fully knowledge followed by
instructions from Udemy course for .NET and ReactTS.

## Backend
- The backend folder contains the C# .NET code for the server-side of the project. It is responsible for handling requests and returning responses to the frontend.

## Frontend
- React
- React Router
- Redux
- Redux Toolkit
- Typescript
- Material UI


## Other libraries
- Stripe
-EF(Entity Framework)
...many more libraries.

**Stripe** is a popular payment processing platform that allows customers to make payments using credit cards, debit cards, and other payment methods. 
The project includes TypeScript classes in the services folder that interact with the Stripe API to create payments and receive payments.

**Redux** is a state management library that allows the application to store and manage data in a predictable and consistent way. 
The project includes a Redux store in the store folder that holds the state of the application, as well as actions and reducers that modify the state in response to user input.

**Docker** Docker was used to deploy and run a PostgreSQL container, allowing for a consistent and isolated environment for the database, without worrying about compatibility issues

# Installation
To install and run the project, follow these steps:

## Backend part
- Clone the repository to your local machine
- Open the backend folder in Visual Studio or your preferred C# IDE(I have used Visual Studio Code)
- Install all the packages from the NuGet Gallery
- Open terminal and navigate to the API folder(path)
- Type `dotnet watch run` command to start the backend server 

## Frontend part
- Open a new terminal and navigate to the client folder(path) 
- Install the dependencies by running `npm install`
- Run the project by running `npm start`
