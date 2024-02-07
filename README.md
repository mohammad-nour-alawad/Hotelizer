# **Hotelizer:**


ASP .NET Core MVC project, with SQL Server Database.

Built based on the following key concepts:
- **MVC** Architector.
- **Unit of Work** (UOW) and **Repository** design patterns.
- **REST APIs**.
- **Service Pool**
- **Generic CRUD Operations**
- **Entity Framework**.
- **SQL Server** with code first, i.e. migrate the database from the code.
- **Ajax Server** Side Pagination.
- **Asynchronous database operations**, to ensure Integrety.
- **Ceperation of Concepts**.
- **Facade** Design Pattern.


# **Architecture**:
To ensure the best practices, I built project using multiple layers, which are:

## **Client**

this project offers wide varaity of use cases for **Clients**, which include:
  - Filtering available rooms based on (Category, Specification, Price, Available date)
  - Booking a room.
  - Adding services to a booking (laudary, breakfast, ...)
  - Updating the booked servies if NOT consumed yet.
  - Room Servies like restaurants, Users can order from the restaurant and the prices will attached to the chosen booking.
  - Reviewing history of bookings and orders.
  - Exploring similar rooms.
  - Exploring most booked rooms.
  - Exporing Most requested services.
  - Exploring Weather using the integrated weather API.
  - Exploring Other hotels registered in the System using google maps API.
  - and many many other use cases.
    
## **Hotelizer**

this is the project that conserns with the **Hotel Staff**, which offers the following use cases:
  - seeding the database in case it is empty. (publishing rooms, services, products, and all other base tables)
  - accepting a booking request.
  - exploring charts for revenues/total users and others.
  - Marking a service as CONSUMED. (like breakfast, landuary, ...)
  - Updating ALL the tables informations (adding photos, updating names, adding new servies, changing prices, and many more others)
  - Adding admins.
  - It also offers, a server side pagination for tables, using **AJAX**.
    
## **Buisness Logic**

this layer offers the app services using a **Service Pool**, which is built to highly seperated the concets from each others, the moduls are:
  - Room service, all logic for services related to rooms only. 
  - Booking service, all logic for services related to bookings only.
  - Restaurant Service, all logic for services related to restaurant only.
  - it is uses a domain mapper, to map the resonse comming form the DB layer into the buisness logic models using **DTOs**.
  - it is resposible for creating the ViewModels, which hold the needed data that will passed to the controller of the needed ser

## **DBConnection**

this layer contains the logic needed to connect to the SQL Server database, it offers:
  - appility to migrate our models into database tables with the needed relationships (code first methodolgy).
  - offers a totaly GENERIC CRUD operations on our data, using a Base Repository.
