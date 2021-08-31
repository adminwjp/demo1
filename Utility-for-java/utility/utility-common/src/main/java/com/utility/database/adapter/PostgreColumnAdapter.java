package com.utility.database.adapter;

import com.utility.database.*;

public  class PostgreColumnAdapter extends AbstractColumnAdapter implements ColumnAdapter{
    @Override
    protected DatabaseFactory getDatabaseFactory(){
        if(databaseFactory==null){
            databaseFactory=new PostgreFactory();
        }
        return databaseFactory;
    }
    
}