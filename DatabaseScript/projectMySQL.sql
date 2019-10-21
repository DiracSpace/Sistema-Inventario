create database mysqlProject;
use mysqlProject;

create table usuarios (
	id_usuario int not null auto_increment,
    username varchar(30) not null,
    userpassword varchar(30) not null,
    primary key(id_usuario)
);

drop table usuarios;

create table datos (
	id_producto int not null auto_increment,
    nomProd varchar(30) not null,
    descProd varchar(50) not null,
    precio int,
    primary key(id_producto)
);

drop table datos;
 /* ignora esto */
 /* 
	|  
	v
 */
insert into mysqlProject.usuarios(username,userpassword)values("DiracSpace2","Roberto33498");

select * from usuarios where username = "roberto" and userpassword ="9mAqmCJFpVa3HpXtn15xgoSe3t8=";

select * from usuarios;