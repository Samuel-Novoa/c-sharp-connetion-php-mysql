CREATE DATABASE sampledb;

CREATE TABLE IF NOT EXISTS product (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre_producto VARCHAR(15) NOT NULL,
    codigo_producto VARCHAR(50) NOT NULL,
    precio DOUBLE NOT NULL
);