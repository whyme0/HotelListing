# HotelListing
##### Is a project for my educational purposes, using ASP.NET Core Web Api technology.
# Project structure
|   Directory  |                                                                                     Specification                                                                                    |
|:------------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
| Controllers  | Face of the API. Client talk with server via HTTP through Controllers classes                                                                                                        |
| Data         | Here are the databse context and models which used to represent tables in database                                                                                                   |
| Logging      | Just a folder with logs files (ignored by .gitignore)                                                                                                                                |
| Mappers      | This folder contains Mapper's logic to automap DTOs and Base models (using automapper package)                                                                                       |
| Properties   | Folder that contains project's properties eg. launch settings                                                                                                                        |
| Migrations   | Just folder with results of Add-Migration's commands (ignored by .gitignore)                                                                                                         |
| Models       | This folder contains models that represenets data which transfering (DTO pattern) between database models (data folder) and user requests/responses                                  |
| Repositories | The catalog contains objects that include business logic between the database and models (tables)                                                                                    |
| Units        | This catalog contains objects that represents some kind of "session" for repositories (the concept of session is applied because object time is restricted by IDisposable interface) |
