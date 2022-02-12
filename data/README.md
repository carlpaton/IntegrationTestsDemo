# Data

A space to dump configuration. May move the infastructure concearns over to use Docker Compose - meh who doesnt love a bit of manual wizzard tricks :D

## MySQL

Database for persistent storage.

```sh
docker run --detach --name integration-tests-db -p 6001:3306 --env="MYSQL_ROOT_PASSWORD=root" mysql:8.0.28
```

Connect to the database manually with MySQL Workbench and run these commands

```sql
select @@version;

CREATE SCHEMA foo_api;

CREATE TABLE foo_api.artist (
  `id` BINARY(16) PRIMARY KEY,
  `name` VARCHAR(45) NULL);

CREATE TABLE foo_api.song (
  `id` BINARY(16) PRIMARY KEY,
  `id_artist` BINARY(16) NOT NULL,
  `name` VARCHAR(45) NULL,
  `created` DATETIME NULL,
  FOREIGN KEY (id_artist) REFERENCES artist(id));

INSERT INTO foo_api.artist(id, name)
VALUES(UUID_TO_BIN(UUID()),'Foo'),
      (UUID_TO_BIN(UUID()),'Bar');

SELECT BIN_TO_UUID(id) id, name 
FROM foo_api.artist;      
```

MySQL UUID support:

* https://dev.mysql.com/blog-archive/mysql-8-0-uuid-support/
* https://www.mysqltutorial.org/mysql-uuid/