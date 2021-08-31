package com.utility.database.bridge;



public  class MySqlDatabaseFactory extends AbstractDatabaseFactory implements DatabaseFactory {

    public MySqlDatabaseFactory(){
        super(new com.utility.database.MySqlFactory());
    }
    
    public MySqlDatabaseFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}