package com.utility.database.bridge;

public  class SqlServerTableFactory extends AbstractTableFactory implements TableFactory {

    public SqlServerTableFactory(){
        super(new com.utility.database.SqlServerFactory());
    }
    
    public SqlServerTableFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}