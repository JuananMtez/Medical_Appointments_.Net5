## About the project

Simple API for the creation of medical appointments. The frontend side is in [Citas_Medicas_Angular](https://github.com/JuananMtez/Citas_Medicas_Angular).

### Built With
![CSharp]
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

## Getting Started

### Prerequisites
* .Net5
* Oracle Database XE
* Visual Studio


### Installation
1. Clone the repo.
```sh
git clone https://github.com/JuananMtez/Medical_Appointments_.Net5.git
```

2. Create database in Oracle Database XE

3. Open the proyect with Visual Studio.


## Usage

Init the proyect with Visual Studio

## Aditional information

You can change the DBMS in ``./appsettings.json``.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=system;Password=admin;Data Source=INSERT THE DATABASE CONNECTION;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

```




## Author

* **Juan Antonio Martínez López** - [Website](https://juananmtez.github.io/) - [LinkedIn](https://www.linkedin.com/in/juanantonio-martinez/)


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

[CSharp]: https://img.shields.io/badge/CSharp-20232A?style=for-the-badge&logo=CSharp
