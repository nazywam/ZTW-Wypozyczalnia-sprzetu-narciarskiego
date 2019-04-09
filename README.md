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

## Build status

[Current Build](http://skirent-env.qmgk85mipb.eu-central-1.elasticbeanstalk.com/)

[![Build Status](https://travis-ci.com/nazywam/ZTW-Wypozyczalnia-sprzetu-narciarskiego.svg?branch=master)](https://travis-ci.com/nazywam/ZTW-Wypozyczalnia-sprzetu-narciarskiego)

[![Build status](https://ci.appveyor.com/api/projects/status/svbu5e3vnd4g5coc?svg=true)](https://ci.appveyor.com/project/nazywam/ztw-wypozyczalnia-sprzetu-narciarskiego)

![](img/tests.png)