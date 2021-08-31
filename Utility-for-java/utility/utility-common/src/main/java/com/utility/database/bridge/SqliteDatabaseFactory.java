package com.utility.database.bridge;

public  class SqliteDatabaseFactory extends AbstractDatabaseFactory implements DatabaseFactory {

    public SqliteDatabaseFactory(){
        super(new com.utility.database.SqliteFactory());
    }

    public SqliteDatabaseFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}