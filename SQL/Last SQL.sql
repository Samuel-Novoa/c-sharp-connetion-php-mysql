CREATE DATABASE productos;

CREATE TABLE productos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    codigo VARCHAR(50) NOT NULL,
    precio DOUBLE NOT NULL,
    cantidad_stock INT NOT NULL,
    fecha_creacion DATETIME NOT NULL,
    categoria VARCHAR(100) NOT NULL
);
