package com.utility.database.bridge;

public  class PostgreTableFactory extends AbstractTableFactory implements TableFactory {

    public PostgreTableFactory(){
        super(new com.utility.database.PostgreFactory());
    }

    public PostgreTableFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}