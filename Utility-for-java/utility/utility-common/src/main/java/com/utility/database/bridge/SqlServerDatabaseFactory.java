package com.utility.database.bridge;

public  class SqlServerDatabaseFactory extends AbstractDatabaseFactory implements DatabaseFactory {

    public SqlServerDatabaseFactory(){
        super(new com.utility.database.SqlServerFactory());
    }
    
    public SqlServerDatabaseFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}