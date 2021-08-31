package com.utility.database.bridge;

 class AbstractTableFactory implements TableFactory  {
    public AbstractTableFactory(){

    }
    public AbstractTableFactory(com.utility.database.DatabaseFactory databaseFactory) {
        this.databaseFactory=databaseFactory;
    }

    protected  com.utility.database.DatabaseFactory databaseFactory;
    
    public void setDatabaseFactory(com.utility.database.DatabaseFactory databaseFactory) {
        this.databaseFactory=databaseFactory;
    }


    public void getColumns() {
        databaseFactory.getColumns();
    }

    public void getTables() {
        databaseFactory.getTables();
    }
}