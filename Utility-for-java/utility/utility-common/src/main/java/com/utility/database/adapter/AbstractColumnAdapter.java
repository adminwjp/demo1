package com.utility.database.adapter;

import com.utility.database.*;

abstract class AbstractColumnAdapter  implements ColumnAdapter{
    protected  DatabaseFactory databaseFactory;
    
    public void setDatabaseFactory(DatabaseFactory databaseFactory) {
        this.databaseFactory=databaseFactory;
    }

    protected abstract DatabaseFactory getDatabaseFactory();

    public void getColumns() {
        getDatabaseFactory().getColumns();
    }
}