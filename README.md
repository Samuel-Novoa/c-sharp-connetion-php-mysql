# C# Sharp Connetion to Php Mysql

## C#
El programa en C# consola que se conecta a una base de datos MySQL y permite crear empleados, eliminar, actualizar y consultar nóminas es una aplicación en la que se pueden realizar las siguientes operaciones:

- Crear empleados: La aplicación permite ingresar los datos de un nuevo empleado, como nombre, edad, salario, etc., y luego los guarda en la base de datos MySQL utilizando una sentencia INSERT INTO. Esto se puede lograr utilizando la biblioteca MySql.Data.MySqlClient para establecer la conexión con la base de datos y ejecutar la consulta.
- Eliminar empleados: La aplicación permite seleccionar un empleado de la base de datos y eliminar su registro utilizando una sentencia DELETE en SQL. Esto también se puede lograr utilizando la biblioteca MySql.Data.MySqlClient y ejecutando la consulta correspondiente.
- Actualizar empleados: La aplicación permite seleccionar un empleado de la base de datos y modificar sus datos, como nombre, edad, salario, etc. Esto se puede lograr utilizando una sentencia UPDATE en SQL para actualizar los valores correspondientes en la base de datos. Al igual que en las operaciones anteriores, se puede utilizar la biblioteca MySql.Data.MySqlClient para ejecutar la consulta.
- Consultar nóminas: La aplicación permite consultar las nóminas de los empleados almacenados en la base de datos. Esto se puede lograr utilizando una sentencia SELECT en SQL para recuperar los datos necesarios de la base de datos. Una vez más, la biblioteca MySql.Data.MySqlClient se puede utilizar para ejecutar la consulta y obtener los resultados.


## SQL
El código SQL proporcionado crea una base de datos llamada "sampledb" y una tabla llamada "empleados" con las siguientes columnas:

id: INT, autoincremental y clave primaria.
cedula: VARCHAR(15), no nulo.
nombre: VARCHAR(50), no nulo.
apellido: VARCHAR(50), no nulo.
horas_trabajadas: DOUBLE, no nulo.
sueldo_por_hora: DOUBLE, no nulo.
La sentencia CREATE DATABASE se utiliza para crear una nueva base de datos en SQL. En este caso, se crea una base de datos llamada "sampledb".

La sentencia CREATE TABLE se utiliza para crear una nueva tabla en la base de datos. En este caso, se crea una tabla llamada "empleados" con las columnas mencionadas anteriormente.
<p aling="center">It's me <b>SAM</b></p>
