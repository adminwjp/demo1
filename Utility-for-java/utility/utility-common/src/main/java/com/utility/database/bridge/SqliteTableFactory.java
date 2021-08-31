package com.utility.database.bridge;

public  class SqliteTableFactory extends AbstractTableFactory implements TableFactory {

    public SqliteTableFactory(){
        super(new com.utility.database.SqliteFactory());
    }

    public SqliteTableFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}