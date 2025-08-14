use master

go

create database CarFactory

go

use CarFactory

go

CREATE TABLE dbo.VehicleMake
(
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Abrv NVARCHAR(20)  NOT NULL
);

CREATE TABLE dbo.VehicleModel
(
    Id     INT IDENTITY PRIMARY KEY,
    MakeId INT NOT NULL FOREIGN KEY REFERENCES dbo.VehicleMake(Id),
    Name   NVARCHAR(100) NOT NULL,
    Abrv   NVARCHAR(20)  NOT NULL
);