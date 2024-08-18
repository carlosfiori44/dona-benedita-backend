CREATE DATABASE loja_Benedita
default character set utf8
default collate utf8_general_ci;
use loja_Benedita;
show tables;
select * from cliente;
Insert into cliente(nome, email, senha) Values("admin", "admin@admin", "admin");

Create table cliente(
id_cliente smallint(4) primary key NOT NULL auto_increment,
nome varchar(30) NOT NULL,
cidade varchar(40) default 'Não especificado',
sexo enum('M','F') NOT NULL,
cep int(8) NULL,
logradouro varchar(40) NULL,
bairro varchar(40) NULL,
email varchar(30) NOT NULL,
senha varchar(16) NOT NULL
) default charset = utf8;

create table telefoneDoCliente(
cliente_id smallint(4) not null,
telefone_cliente int (10) NOT NULL
)default charset = utf8;

ALTER TABLE telefoneDoCliente ADD
CONSTRAINT cliente_id FOREIGN KEY (cliente_id) REFERENCES cliente(id_cliente);

create table compra(
id_compra smallint(4) primary key not null auto_increment,
data_venda date not null,
data_entrega date not null,
frete decimal(2,2) not null,
total decimal(4,2) not null,
quantidade_prod smallint(3) not null,
id_produto smallint(4) not null,
id_cliente smallint(4) not null
)default charset = utf8;

ALTER TABLE compra ADD
CONSTRAINT id_cliente FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente);

ALTER TABLE compra ADD
CONSTRAINT id_produto FOREIGN KEY (id_produto) REFERENCES produto(id_produto);

create table produto(
id_produto smallint(4) primary key not null auto_increment,
preco_custo decimal(4,2) null,
preco_venda decimal(3,2) null,
descricao varchar(65) null
)default charset = utf8;

create table categoria(
id_categoria smallint(4) primary key not null auto_increment,
cor varchar(16) null,
formato varchar(20) null,
tipo_tecido varchar(20) null,
personalizado mediumblob null
)default charset = utf8;

