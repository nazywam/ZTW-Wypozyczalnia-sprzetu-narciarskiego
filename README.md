# SkiRent

# Requirements

 * Visual Studio 2017 (including .NET framework)
 * `MySQL Connector Net 6.9.10` from [here](https://dev.mysql.com/downloads/connector/net/6.9.html), add references

# Building

## Database

 * Setup a MySQL database
 * Create a database and a user
 * Generate the database structure from `/Misc/db.sql`
 * Correct the databse connection string in `/SkiRent/SkiRent/Web.config`

## Application
 * Open the solution file(`SkiRent.sln`) in Visual Studio, build the project and navigate to http://localhost:49698/

# Misc
 * 🇵🇱/🇬🇧 lang
 * The application uses public api from nbp: http://api.nbp.pl/api/exchangerates/tables/A/ to calculate payment values for various currencies

# Features

## Logging in
![](https://i.imgur.com/29hZc0X.png)


## Employee management

### Browsing

![](img/employee_browse.png)


### CRUD

![](img/employee_crud.png)

## Inventory management

### Browsing

![](img/inventory.png)

### CRUD

![](img/items.png)


## Clients management

![](img/clients.png)

### CRUD

![](img/client.png)


## Orders managamenet

![](img/orders.png)

### CRUD

![](img/client.png)


## Fast barcode scanning

![](img/barcode_fast.png)


## Order payment in various currencies

![](img/order_payment.png)

![](img/order_payments.png)


## Build status

![](img/tests.png)