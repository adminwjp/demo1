package com.utility.database.bridge;

public  class MySqlTableFactory  extends AbstractTableFactory implements TableFactory {

    public MySqlTableFactory(){
        super(new com.utility.database.MySqlFactory());
    }
    
    public MySqlTableFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}