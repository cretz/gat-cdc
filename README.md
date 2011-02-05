#gat-cdc

**This application is not yet built and is under active development. 
It cannot even be used in its current state. The contents below are
really just the beginnings of Readme Driven Development (RDD).**

gat-cdc is a Change Data Capture (CDC) framework that is primarily trigger
based. The goal for gat-cdc is to provide fast, easy to configure CDC
database facilities on several different vendors including SQL Server,
MySQL, PostgreSQL, and Oracle.

## Command Line Interface

gat-cdc provides a common command line interface to manage gat-cdc projects
within databases. Successful executions return an exit code of 0 whereas
failures return an exit code of 1.

    gat-cdc [options] [action]

Options:

* _-project, -p XML_FILE_ - The gat-cdc XML project file to use. This is required
  for several actions. This cannot be provided in conjunction with the
  -project-name option.
* _-project-name, -n PROJECT_NAME_ - The gat-cdc project name to use. This is 
  required for several actions. This cannot be provided in conjunction with
  the -project option.
* _-conditional, -c CONDITIONAL_NAME_ - The name of the conditional in the referenced
  project. Some actions use this to narrow queries.
* _-db-vendor, -dbv VENDOR_ - The vendor for the database. This can be "mssql", "mysql",
  "postgres", or "oracle".
* _-db-host, -dbh HOST_ - The hostname or IP address of the database.
* _-db-port, -dbp PORT_ - The port the database listens on. If this is not provided,
  default ports will be used. This is 1433 for SQL Server, 3306 for MySQL, 5432 for
  PostgreSQL, and 1521 for Oracle.
* _-db-name, -dbn NAME_ - The name of the database. If not provided, the default
  database will be used.
* _-db-user, -dbu USERNAME_ - The username for the database. If this is not provided,
  anonymous access will be assumed unless it's SQL Server. If it's SQL server, the
  currently logged in user's domain credentials will be used.
* _-db-pass, -dbp [PASSWORD]_ - The password for the database. If the option is given
  but the password is not, the user will be prompted to enter a masked password at
  runtime. This is required if _-db-user_ is given.
* _-oracle-server, -os SERVER_TYPE_ - The server type for an Oracle database. This can
  be "default", "dedicated", or "shared".
* _-oracle-service-name, -osn SERVICE_NAME_ - The service name for an Oracle database. 
  This cannot be used in conjunction with _-oracle-service-identifier_.
* _-oracle-service-identifier, -osi SID_NAME_ - The SID for an Oracle database. This
  cannot be used in conjunction with _-oracle-service-name_.
* _-strict, -s_ - If set, this will do more strict checking on validations, installations,
  and updates. Currently, this will perform the following additional assertions:
** Every conditional must have at least one applicable table
  
Action:

* _-install, -i_ - Installs the given project XML into the database. This will fail
  if a project with the same name is already installed; use _-update_ instead.
* _-update, -ud_ - Updates the given project XML or name in the database. If the XML
  file for the project is provided, this essentially does an atomic _-uninstall_
  followed by a _-install_. If the name of the project is provided, it simply updates
  the conditionals from the last known project XML specification. When only a name
  of a project is provided, no externally referenced source files or assemblies will
  be updated. This will fail if the project does not currently exist in the database.
* _-uninstall, -u_ - Uninstalls the given project in the database. This takes either a
  project XML file or a project name. This will fail if the project does not currently
  exit in the database.
* _-validate, -v_ - Validates the given project XML file. Errors are returned one per 
  line. This will do more advanced validation if database information is provided.
* _-list-projects, -lp_ - Lists all known projects in the given database, one per line. 
  This does not accept a project XML file or project name. If no projects are present,
  nothing is returned.
* _-list-conditionals, -lc_ - Lists all conditionals for a given project name in the
  given database.
* _-dump-project, -dp_ - Dumps the complete XML for the given project name in the given 
  database as it was given when installed or updated. Note, the indentation may not
  be exactly as it was when initially installed or updated.
* _-dump-conditional, -dc_ - Dumps the complete conditional XML for the given project 
  name and conditional name in the given database as it was given when installed or
  updated. Note, the indentation may not be exactly as it was when initially installed
  or updated.
* _-list-applied-schemas, -las_ - Lists every schema the given project applies to in the 
  given database. If a project XML file is provided, this will list every schema this
  will apply to if it is installed or updated. If a project's name is provided, this
  will list every schema the project is currently applied to. A conditional name can
  be provided to narrow the results. The output is a schema name per line. They are
  returned in alphabetical order.
* _-list-applied-tables, -lat_ - Lists every table the given project applies to in the 
  given database. If a project XML file is provided, this will list every table this
  will apply to if it is installed or updated. If a project's name is provided, this
  will list every table the project is currently applied to. A conditional name can
  be provided to narrow the results. The output is a table name per line in the format
  SCHEMA.TABLE_NAME. They are returned in alphabetical order.
* _-list-applied-columns, -lac_ - Lists every column the given project applies to in the 
  given database. If a project XML file is provided, this will list every column this
  will apply to if it is installed or updated. If a project's name is provided, this
  will list every column the project is currently applied to. A conditional name can
  be provided to narrow the results. The output is a column name per line in the format
  SCHEMA.TABLE_NAME.COLUMN. The order of the schema and table output is alphabetical
  by name, and the order of the resulting columns is the order they are defined in the
  table.