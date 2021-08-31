package com.utility.database.bridge;

 class AbstractDatabaseFactory extends AbstractTableFactory implements DatabaseFactory  {
    public AbstractDatabaseFactory(){
        super();
    }
    public AbstractDatabaseFactory(com.utility.database.DatabaseFactory databaseFactory) {
        super(databaseFactory);
        //this.databaseFactory=databaseFactory;
    }

    public void getDatabases() {
        databaseFactory.getDatabases();
    }
}