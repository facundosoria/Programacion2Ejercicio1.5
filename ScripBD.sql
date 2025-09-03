-- Crear la base de datos
CREATE DATABASE FacturacionDB;
GO

USE FacturacionDB;
GO

-- Tabla de Formas de Pago
CREATE TABLE FormaPago (
    IdFormaPago INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL
);

-- Tabla de Artículos
CREATE TABLE Articulo (
    IdArticulo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL
);

-- Tabla de Facturas
CREATE TABLE Factura (
    NroFactura INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    IdFormaPago INT NOT NULL,
    Cliente NVARCHAR(100) NOT NULL,
    FOREIGN KEY (IdFormaPago) REFERENCES FormaPago(IdFormaPago)
);

-- Tabla de Detalle de Factura
CREATE TABLE DetalleFactura (
    IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
    NroFactura INT NOT NULL,
    IdArticulo INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    FOREIGN KEY (NroFactura) REFERENCES Factura(NroFactura),
    FOREIGN KEY (IdArticulo) REFERENCES Articulo(IdArticulo),
    CONSTRAINT UQ_Factura_Articulo UNIQUE (NroFactura, IdArticulo) -- Evita duplicados del mismo artículo en una factura
);

-- Insertar Formas de Pago
INSERT INTO FormaPago (Nombre) VALUES ('Efectivo');
INSERT INTO FormaPago (Nombre) VALUES ('Tarjeta de Crédito');
INSERT INTO FormaPago (Nombre) VALUES ('Tarjeta de Débito');
INSERT INTO FormaPago (Nombre) VALUES ('Transferencia');
INSERT INTO FormaPago (Nombre) VALUES ('Cheque');

-- Insertar Artículos
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Notebook', 120000.00);
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Mouse', 2500.00);
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Teclado', 4000.00);
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Monitor', 35000.00);
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Impresora', 18000.00);

-- Insertar Facturas (usando IdFormaPago existentes)
INSERT INTO Factura (Fecha, IdFormaPago, Cliente) VALUES ('2025-08-01', 1, 'Juan Pérez');
INSERT INTO Factura (Fecha, IdFormaPago, Cliente) VALUES ('2025-08-02', 2, 'Ana Gómez');
INSERT INTO Factura (Fecha, IdFormaPago, Cliente) VALUES ('2025-08-03', 3, 'Carlos Ruiz');
INSERT INTO Factura (Fecha, IdFormaPago, Cliente) VALUES ('2025-08-04', 4, 'María López');
INSERT INTO Factura (Fecha, IdFormaPago, Cliente) VALUES ('2025-08-05', 5, 'Pedro Sánchez');

-- Insertar DetalleFactura (usando NroFactura y IdArticulo existentes)
INSERT INTO DetalleFactura (NroFactura, IdArticulo, Cantidad) VALUES (1, 1, 2);
INSERT INTO DetalleFactura (NroFactura, IdArticulo, Cantidad) VALUES (1, 2, 1);
INSERT INTO DetalleFactura (NroFactura, IdArticulo, Cantidad) VALUES (2, 3, 3);
INSERT INTO DetalleFactura (NroFactura, IdArticulo, Cantidad) VALUES (3, 4, 1);
INSERT INTO DetalleFactura (NroFactura, IdArticulo, Cantidad) VALUES (4, 5, 2);


-- =========================
-- FormaPago
-- =========================
CREATE PROCEDURE GetAllFormaPago
AS
BEGIN
    SELECT * FROM FormaPago
END
GO

CREATE PROCEDURE GetFormaPagoById
    @IdFormaPago INT
AS
BEGIN
    SELECT * FROM FormaPago WHERE IdFormaPago = @IdFormaPago
END
GO

CREATE PROCEDURE InsertFormaPago
    @Nombre NVARCHAR(50)
AS
BEGIN
    INSERT INTO FormaPago (Nombre) VALUES (@Nombre)
END
GO

CREATE PROCEDURE UpdateFormaPago
    @IdFormaPago INT,
    @Nombre NVARCHAR(50)
AS
BEGIN
    UPDATE FormaPago SET Nombre = @Nombre WHERE IdFormaPago = @IdFormaPago
END
GO

CREATE PROCEDURE DeleteFormaPago
    @IdFormaPago INT
AS
BEGIN
    DELETE FROM FormaPago WHERE IdFormaPago = @IdFormaPago
END
GO

-- =========================
-- Articulo
-- =========================
CREATE PROCEDURE GetAllArticulo
AS
BEGIN
    SELECT * FROM Articulo
END
GO

CREATE PROCEDURE GetArticuloById
    @IdArticulo INT
AS
BEGIN
    SELECT * FROM Articulo WHERE IdArticulo = @IdArticulo
END
GO

CREATE PROCEDURE InsertArticulo
    @Nombre NVARCHAR(100),
    @PrecioUnitario DECIMAL(18,2)
AS
BEGIN
    INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES (@Nombre, @PrecioUnitario)
END
GO

CREATE PROCEDURE UpdateArticulo
    @IdArticulo INT,
    @Nombre NVARCHAR(100),
    @PrecioUnitario DECIMAL(18,2)
AS
BEGIN
    UPDATE Articulo SET Nombre = @Nombre, PrecioUnitario = @PrecioUnitario WHERE IdArticulo = @IdArticulo
END
GO

CREATE PROCEDURE DeleteArticulo
    @IdArticulo INT
AS
BEGIN
    DELETE FROM Articulo WHERE IdArticulo = @IdArticulo
END
GO

-- =========================
-- Factura
-- =========================
CREATE PROCEDURE GetAllFactura
AS
BEGIN
    SELECT * FROM Factura
END
GO

CREATE PROCEDURE GetFacturaById
    @NroFactura INT
AS
BEGIN
    SELECT * FROM Factura WHERE NroFactura = @NroFactura
END
GO

CREATE PROCEDURE InsertFactura
    @Fecha DATE,
    @IdFormaPago INT,
    @Cliente NVARCHAR(100)
AS
BEGIN
    INSERT INTO Factura (Fecha, IdFormaPago, Cliente) VALUES (@Fecha, @IdFormaPago, @Cliente)
END
GO

CREATE PROCEDURE UpdateFactura
    @NroFactura INT,
    @Fecha DATE,
    @IdFormaPago INT,
    @Cliente NVARCHAR(100)
AS
BEGIN
    UPDATE Factura SET Fecha = @Fecha, IdFormaPago = @IdFormaPago, Cliente = @Cliente WHERE NroFactura = @NroFactura
END
GO


GO
CREATE PROCEDURE DeleteFactura
    @NroFactura INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE NroFactura = @NroFactura;
    DELETE FROM Factura WHERE NroFactura = @NroFactura;
END
GO

-- =========================
-- DetalleFactura
-- =========================
CREATE PROCEDURE GetAllDetalleFactura
AS
BEGIN
    SELECT * FROM DetalleFactura
END
GO

CREATE PROCEDURE GetDetalleFacturaById
    @IdDetalle INT
AS
BEGIN
    SELECT * FROM DetalleFactura WHERE IdDetalle = @IdDetalle
END
GO

CREATE PROCEDURE GetDetalleFacturaByFactura
    @NroFactura INT
AS
BEGIN
    SELECT * FROM DetalleFactura WHERE NroFactura = @NroFactura
END
GO

CREATE PROCEDURE InsertDetalleFactura
    @NroFactura INT,
    @IdArticulo INT,
    @Cantidad INT
AS
BEGIN
    INSERT INTO DetalleFactura (NroFactura, IdArticulo, Cantidad) VALUES (@NroFactura, @IdArticulo, @Cantidad)
END
GO

CREATE PROCEDURE UpdateDetalleFactura
    @IdDetalle INT,
    @NroFactura INT,
    @IdArticulo INT,
    @Cantidad INT
AS
BEGIN
    UPDATE DetalleFactura SET NroFactura = @NroFactura, IdArticulo = @IdArticulo, Cantidad = @Cantidad WHERE IdDetalle = @IdDetalle
END
GO

CREATE PROCEDURE DeleteDetalleFactura
    @IdDetalle INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE IdDetalle = @IdDetalle
END
GO