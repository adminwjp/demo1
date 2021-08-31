
    PRAGMA foreign_keys = OFF

    drop table if exists t_catagory

    drop table if exists t_s_menu

    drop table if exists t_s_source_material

    drop table if exists t_edution

    drop table if exists t_icon

    drop table if exists t_user_menu

    drop table if exists t_work

    PRAGMA foreign_keys = ON

    create table t_catagory (
        id INTEGER not null,
       code TEXT,
       category TEXT,
       description TEXT,
       accept TEXT,
       flag INTEGER,
       parent_id INTEGER,
       primary key (id),
       constraint fk_parent_id foreign key (parent_id) references t_catagory
    )

    create table t_s_menu (
        id INTEGER not null,
       parent_id INTEGER,
       soure TEXT,
       orders INTEGER,
       creation_time INTEGER,
       last_modification_time INTEGER,
       is_deleted INTEGER,
       deletion_time INTEGER,
       text TEXT,
       state TEXT,
       checked INTEGER,
       attributes_json TEXT,
       icon_cls TEXT,
       name TEXT,
       collpse INTEGER,
       groups TEXT,
       icon TEXT,
       href TEXT,
       description TEXT,
       hui_icon TEXT,
       id_name TEXT,
       ace_icon TEXT,
       primary key (id),
       constraint fk_parent_id foreign key (parent_id) references t_s_menu
    )

    create table t_s_source_material (
        id INTEGER not null,
       src TEXT,
       key INTEGER,
       base64 TEXT,
       description INTEGER,
       buket TEXT,
       object_name TEXT,
       primary key (id)
    )

    create table t_edution (
        id INTEGER not null,
       create_date INTEGER,
       update_date INTEGER,
       user_id INTEGER,
       catagory_id INTEGER,
       first_edution TEXT,
       first_school TEXT,
       first_start_date INTEGER,
       first_end_date INTEGER,
       second_edution TEXT,
       second_school TEXT,
       second_start_date INTEGER,
       second_end_date INTEGER,
       three_edution TEXT,
       three_school TEXT,
       three_start_date INTEGER,
       three_end_date INTEGER,
       four_edution TEXT,
       four_school TEXT,
       four_start_date INTEGER,
       four_end_date INTEGER,
       five_edution TEXT,
       five_school TEXT,
       five_start_date INTEGER,
       five_end_date INTEGER,
       primary key (id)
    )

    create table t_icon (
        id INTEGER not null,
       create_date INTEGER,
       update_date INTEGER,
       name TEXT,
       style TEXT,
       description TEXT,
       primary key (id)
    )

    create table t_user_menu (
        id INTEGER not null,
       create_date INTEGER,
       update_date INTEGER,
       enable INTEGER,
       add1 INTEGER,
       modify1 INTEGER,
       delete1 INTEGER,
       query INTEGER,
       type TEXT,
       val INTEGER,
       primary key (id)
    )

    create table t_work (
        id INTEGER not null,
       create_date INTEGER,
       update_date INTEGER,
       company_name TEXT,
       job TEXT,
       description TEXT,
       start_date INTEGER,
       end_date INTEGER,
       user_id INTEGER,
       catagory_id INTEGER,
       primary key (id)
    )
