package com.utility.database.bridge;

public  class PostgreDatabaseFactory extends AbstractDatabaseFactory implements DatabaseFactory {

    public PostgreDatabaseFactory(){
        super(new com.utility.database.PostgreFactory());
    }

    public PostgreDatabaseFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}