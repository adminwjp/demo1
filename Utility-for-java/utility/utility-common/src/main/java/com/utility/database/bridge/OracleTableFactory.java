package com.utility.database.bridge;

public  class OracleTableFactory extends AbstractTableFactory implements TableFactory {

    public OracleTableFactory(){
        super(new com.utility.database.OracleFactory());
    }
    
    public OracleTableFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
    }

}