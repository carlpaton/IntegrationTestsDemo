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
  `created` DATETIME NULL DEFAULT CURRENT_TIMESTAMP);

CREATE TABLE foo_api.package_version (
  `id` BINARY(16) PRIMARY KEY,
  `id_package` BINARY(16) NOT NULL,
  `version` VARCHAR(50) NULL,
  FOREIGN KEY (id_package) REFERENCES package(id));

CREATE TABLE foo_api.vulnerabilities (
  `id` BINARY(16) PRIMARY KEY,
  `id_package_version` BINARY(16) NOT NULL,
  `title` VARCHAR(100) NULL,
  `description` VARCHAR(500) NULL,
  `cvss_score` VARCHAR(50) NULL,
  `reference` VARCHAR(250) NULL,
  FOREIGN KEY (id_package_version) REFERENCES package_version(id));

SET @id_package = UUID();
SET @id_package_version = UUID();

INSERT INTO foo_api.package
VALUES(UUID_TO_BIN(@id_package),'log4net','log4net is a tool to help the programmer ....', 107054643, NOW());

INSERT INTO foo_api.package_version
VALUES(UUID_TO_BIN(@id_package_version), UUID_TO_BIN(@id_package),'1.2.10');

INSERT INTO foo_api.vulnerabilities
VALUES(UUID_TO_BIN(UUID()), UUID_TO_BIN(@id_package_version),'[CVE-2018-1285] Apache log4net before 2.0.8 does not disable XML external entities when parsing ...', 'Apache log4net before 2.0.8 does not disable XML external entities when parsing log4net configuration files ... ', '9.8', 'https://ossindex.sonatype.org/vulnerability/c4ac70fa-d3ce-4153-b4e9-e1a9d193be8c?component-type=nuget&component-name=log4net&utm_source=postmanruntime&utm_medium=integration&utm_content=7.28.4');

SELECT BIN_TO_UUID(id) id, name, description, total_downloads, created FROM foo_api.package;
SELECT BIN_TO_UUID(id), BIN_TO_UUID(id_package), version FROM foo_api.package_version;
SELECT BIN_TO_UUID(id), BIN_TO_UUID(id_package_version), title, description, cvss_score, reference FROM foo_api.vulnerabilities; 
```

MySQL UUID support:

* https://dev.mysql.com/blog-archive/mysql-8-0-uuid-support/
* https://www.mysqltutorial.org/mysql-uuid/