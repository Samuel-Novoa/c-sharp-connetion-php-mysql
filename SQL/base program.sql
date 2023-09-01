CREATE DATABASE products;

CREATE TABLE IF NOT EXISTS empleados (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cedula VARCHAR(15) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    horas_trabajadas DOUBLE NOT NULL,
    sueldo_por_hora DOUBLE NOT NULL
);