﻿Projekt temat: Administrator systemu informatycznego
Grupa GKiO1
skład seckji: Katarzyna Biernat
	      Joanna Deorowicz
	      Wojtek Walczyszyn
	      Paweł Kluba
	      Łukasz Tenerowicz - kierownik
semestr: szósty
rok:     2014
=================

Jest to aplikacja tworzona w celu realizacji projektu z przedmiotu Bazy Danych III. Szczegółowa specyfikacja jak i początkowy schemat bazy danych znajduje się w folderze [Docs](https://github.com/konkit/UserManagementDLL/tree/master/Docs).
Projekt składa się z DLL + aplikacja ASP.NET MVC.
Projekt stworzony w Visual Studio 2013.


#####Ważne - instalacja programu

Po sciągnięciu repozytorium należy wykonać następujące kroki:
- otwórz projekt w Visual Studio 2013
- otwórz konsole Package Manager Console
- wpisz w konsolę "Enable-Migrations -F"
- Przejdź do pliku Migrations/Configuration.cs
- zmień linijkę AutomaticMigrationsEnabled = false na AutomaticMigrationsEnabled = true;
- wróć do konsoli, wpisz "Update-Database -F"
- programuj :)

#####Do poczytania:

- http://www.asp.net/mvc/tutorials/mvc-5/introduction/getting-started - tutorial STEP BY STEP. Obowiązkowa lektura ;)

#####Dodatkowo:

- http://msdn.microsoft.com/en-us/library/dd381412(v=vs.108).aspx - ogólnie o ASP.NET
- http://msdn.microsoft.com/en-us/library/3707x96z(v=vs.80).aspx - o tworzeniu DLL
- http://www.asp.net/mvc/tutorials/older-versions/movie-database/create-a-movie-database-application-in-15-minutes-with-asp-net-mvc-cs oraz http://www.asp.net/mvc/tutorials/hands-on-labs/aspnet-mvc-4-models-and-data-access - trochę o tworzeniu baz danych dla aplikacji ASP. Co prawda dla starszych wersji, ale tu się wiele nie zmieniło
