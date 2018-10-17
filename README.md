# WikiBlog

A fun way to express your opinions and tamper with other peoples' in real-time.

## Live Demo
Live demo hosted here: coming soon...

## Getting Started

WikiBlog is written in Visual Studio Community 2017 with C# and ASP.NET 2.1 Core. Simply clone or download the repository, then open WikiBlog.sln and run in VS2017. Be sure to create a SQL database and connect it properly using the connection string located in Startup.cs::Configure(). There needs to be one table, called Posts, with the following columns: ID (int), Title (varchar), Author (varchar), Content (text).

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
