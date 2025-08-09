use BDNeteros

-- Elimina la tabla si ya existe
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CLIENTE_BCP')
BEGIN
    DROP TABLE CLIENTE_BCP;
    PRINT 'Tabla CLIENTE_BCP eliminada.';
END

-- Crea la tabla nuevamente
CREATE TABLE CLIENTE_BCP (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NOMBRES VARCHAR(100) NOT NULL,
    APELLIDOS VARCHAR(100) NOT NULL,
    DNI VARCHAR(8) NOT NULL,
    DIRECCION VARCHAR(200) NOT NULL,
    NUMEROCUENTA VARCHAR(20) NOT NULL
);

PRINT 'Tabla CLIENTE_BCP creada exitosamente.';
go


-- Elimina el procedimiento si existe
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ClienteBCP_Insertar')
BEGIN
    DROP PROCEDURE sp_ClienteBCP_Insertar;
    PRINT 'Procedimiento sp_ClienteBCP_Insertar eliminado.';
END
GO

-- Crea el procedimiento
CREATE PROCEDURE sp_ClienteBCP_Insertar
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @DNI VARCHAR(8),
    @Direccion VARCHAR(200),
    @NumeroCuenta VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Valida duplicados por DNI (opcional)
    IF EXISTS (SELECT 1 FROM CLIENTE_BCP WHERE DNI = @DNI)
    BEGIN
        RAISERROR('El cliente con este DNI ya existe.', 16, 1);
        RETURN;
    END
    
    INSERT INTO CLIENTE_BCP (NOMBRES, APELLIDOS, DNI, DIRECCION, NUMEROCUENTA)
    VALUES (@Nombres, @Apellidos, @DNI, @Direccion, @NumeroCuenta);
    
    SELECT SCOPE_IDENTITY() AS Id; -- Retorna el ID generado
END
GO

PRINT 'Procedimiento sp_ClienteBCP_Insertar creado exitosamente.';
go


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ClienteBCP_Actualizar')
BEGIN
    DROP PROCEDURE sp_ClienteBCP_Actualizar;
    PRINT 'Procedimiento sp_ClienteBCP_Actualizar eliminado.';
END
GO


CREATE PROCEDURE sp_ClienteBCP_Actualizar
    @Id INT,
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @DNI VARCHAR(8),
    @Direccion VARCHAR(200),
    @NumeroCuenta VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Valida que el cliente exista
    IF NOT EXISTS (SELECT 1 FROM CLIENTE_BCP WHERE ID = @Id)
    BEGIN
        RAISERROR('El cliente no existe.', 16, 1);
        RETURN;
    END
    
    UPDATE CLIENTE_BCP 
    SET 
        NOMBRES = @Nombres,
        APELLIDOS = @Apellidos,
        DNI = @DNI,
        DIRECCION = @Direccion,
        NUMEROCUENTA = @NumeroCuenta
    WHERE ID = @Id;
    
    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO

PRINT 'Procedimiento sp_ClienteBCP_Actualizar creado exitosamente.';
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ClienteBCP_Eliminar')
BEGIN
    DROP PROCEDURE sp_ClienteBCP_Eliminar;
    PRINT 'Procedimiento sp_ClienteBCP_Eliminar eliminado.';
END
GO

CREATE PROCEDURE sp_ClienteBCP_Eliminar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM CLIENTE_BCP WHERE ID = @Id)
    BEGIN
        RAISERROR('El cliente no existe.', 16, 1);
        RETURN;
    END
    
    DELETE FROM CLIENTE_BCP WHERE ID = @Id;
    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO

PRINT 'Procedimiento sp_ClienteBCP_Eliminar creado exitosamente.';
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ClienteBCP_ObtenerTodos')
BEGIN
    DROP PROCEDURE sp_ClienteBCP_ObtenerTodos;
    PRINT 'Procedimiento sp_ClienteBCP_ObtenerTodos eliminado.';
END
GO

CREATE PROCEDURE sp_ClienteBCP_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID, 
        NOMBRES, 
        APELLIDOS, 
        DNI, 
        DIRECCION, 
        NUMEROCUENTA
    FROM CLIENTE_BCP;
END
GO

PRINT 'Procedimiento sp_ClienteBCP_ObtenerTodos creado exitosamente.';
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ClienteBCP_ObtenerPorId')
BEGIN
    DROP PROCEDURE sp_ClienteBCP_ObtenerPorId;
    PRINT 'Procedimiento sp_ClienteBCP_ObtenerPorId eliminado.';
END
GO

CREATE PROCEDURE sp_ClienteBCP_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID, 
        NOMBRES, 
        APELLIDOS, 
        DNI, 
        DIRECCION, 
        NUMEROCUENTA
    FROM CLIENTE_BCP
    WHERE ID = @Id;
END

PRINT 'Procedimiento sp_ClienteBCP_ObtenerPorId creado exitosamente.';
GO


-- Elimina registros existentes (opcional)
DELETE FROM CLIENTE_BCP;

-- Inserta 10 clientes de prueba
INSERT INTO CLIENTE_BCP (NOMBRES, APELLIDOS, DNI, DIRECCION, NUMEROCUENTA)
VALUES
    ('Juan', 'Pérez Rodríguez', '46738291', 'Av. Arequipa 123, Lima', '001234567890'),
    ('María', 'García López', '30124567', 'Jr. Huancavelica 456, Lima', '002345678901'),
    ('Carlos', 'Martínez Fernández', '18765432', 'Calle Trujillo 789, Lima', '003456789012'),
    ('Lucía', 'Díaz Sánchez', '40235678', 'Av. Brasil 1011, Lima', '004567890123'),
    ('Pedro', 'Torres Vargas', '28765431', 'Jr. Ayacucho 1213, Lima', '005678901234'),
    ('Ana', 'Flores Medina', '36543210', 'Av. Javier Prado 1415, Lima', '006789012345'),
    ('Luis', 'Ruíz Castro', '51234567', 'Calle Lima 1617, Lima', '007890123456'),
    ('Sofía', 'Herrera Mendoza', '42345678', 'Av. La Marina 1819, Lima', '008901234567'),
    ('Jorge', 'Silva Rojas', '34567890', 'Jr. Cusco 2021, Lima', '009012345678'),
    ('Elena', 'Vega Gutiérrez', '25678901', 'Av. Alfonso Ugarte 2223, Lima', '010123456789');

-- Verifica los registros
SELECT * FROM CLIENTE_BCP;