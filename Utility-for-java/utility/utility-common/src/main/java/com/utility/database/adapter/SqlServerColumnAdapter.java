package com.utility.database.adapter;

import com.utility.database.*;

public  class SqlServerColumnAdapter extends AbstractColumnAdapter  implements ColumnAdapter{
    @Override
    protected DatabaseFactory getDatabaseFactory(){
        if(databaseFactory==null){
            databaseFactory=new SqlServerFactory();
        }
        return databaseFactory;
    }
}