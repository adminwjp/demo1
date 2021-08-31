package com.utility.database.adapter;

import com.utility.database.*;

public  class SqliteColumnAdapter extends AbstractColumnAdapter implements ColumnAdapter {
    @Override
    protected DatabaseFactory getDatabaseFactory(){
        if(databaseFactory==null){
            databaseFactory=new SqliteFactory();
        }
        return databaseFactory;
    }
}