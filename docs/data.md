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

CREATE TABLE foo_api.package (
  `id` BINARY(16) PRIMARY KEY,
  `name` VARCHAR(150) NULL,
  `description` VARCHAR(150) NULL,
  `total_downloads` INT NULL,
  `created` DATETIME NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE foo_api.package_version (
  `id` BINARY(16) PRIMARY KEY,
  `id_package` BINARY(16) NOT NULL,
  `version` VARCHAR(50) NULL,
  FOREIGN KEY (id_package) 
    REFERENCES package(id)
    ON DELETE CASCADE
);

CREATE TABLE foo_api.vulnerability (
  `id` BINARY(16) PRIMARY KEY,
  `id_package_version` BINARY(16) NOT NULL,
  `title` VARCHAR(100) NULL,
  `description` VARCHAR(500) NULL,
  `cvss_score` VARCHAR(50) NULL,
  `reference` VARCHAR(250) NULL,
  FOREIGN KEY (id_package_version) 
    REFERENCES package_version(id)
    ON DELETE CASCADE
);
```

## References

MySQL UUID support:

* https://dev.mysql.com/blog-archive/mysql-8-0-uuid-support/
* https://www.mysqltutorial.org/mysql-uuid/