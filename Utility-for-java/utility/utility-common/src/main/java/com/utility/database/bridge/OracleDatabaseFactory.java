package com.utility.database.bridge;

public  class OracleDatabaseFactory  extends AbstractDatabaseFactory implements DatabaseFactory {

    public OracleDatabaseFactory(){
        super(new com.utility.database.OracleFactory());
    }

    public OracleDatabaseFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}