
    PRAGMA foreign_keys = OFF

    drop table if exists t_catagory

    drop table if exists t_address

    drop table if exists t_bank

    drop table if exists t_city

    drop table if exists t_menu

    drop table if exists t_token

    drop table if exists t_user_friend

    drop table if exists t_user_log

    drop table if exists t_source_material

    drop table if exists t_edution

    drop table if exists t_icon

    drop table if exists t_user_menu

    drop table if exists t_work

    drop table if exists t_user

    drop table if exists t_admin

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

    create table t_address (
        id INTEGER not null,
       contact_name TEXT,
       address TEXT,
       area TEXT,
       city TEXT,
       province TEXT,
       country TEXT,
       memo TEXT,
       phone TEXT,
       post_code TEXT,
       is_default INTEGER,
       user_id INTEGER,
       primary key (id)
    )

    create table t_bank (
        id INTEGER not null,
       bank_id TEXT,
       bank_name TEXT,
       bank_photo1 TEXT,
       bank_photo2 TEXT,
       bank_photo_id1 INTEGER,
       bank_photo_id2 INTEGER,
       bank_address TEXT,
       bank_user_name TEXT,
       bank_user_address TEXT,
       is_default INTEGER,
       user_id INTEGER,
       primary key (id)
    )

    create table t_city (
        id INTEGER not null,
       prodive TEXT,
       city TEXT,
       area TEXT,
       prodive_code TEXT,
       city_code TEXT,
       area_code TEXT,
       parent_id INTEGER,
       primary key (id)
    )

    create table t_menu (
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
       constraint fk_parent_id foreign key (parent_id) references t_menu
    )

    create table t_token (
        id INTEGER not null,
       token TEXT,
       token_expried INTEGER,
       refresh_token TEXT,
       refresh_token_expried INTEGER,
       create_date INTEGER,
       user_id INTEGER,
       flag INTEGER,
       primary key (id)
    )

    create table t_user_friend (
        id INTEGER not null,
       user_id INTEGER,
       friend_id INTEGER,
       agree INTEGER,
       delete_flag INTEGER,
       primary key (id)
    )

    create table t_user_log (
        id INTEGER not null,
       user_id INTEGER,
       account TEXT,
       email TEXT,
       phone TEXT,
       old_pwd TEXT,
       new_pwd TEXT,
       new_account TEXT,
       new_email TEXT,
       new_phone TEXT,
       add_date INTEGER,
       primary key (id)
    )

    create table t_source_material (
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

    create table t_user (
        id  integer primary key autoincrement,
       edution TEXT,
       school TEXT,
       job_company TEXT,
       job TEXT,
       likes TEXT,
       marital INTEGER,
       description TEXT,
       card_id TEXT,
       card_photo1 TEXT,
       card_photo2 TEXT,
       hand_card_photo1 TEXT,
       hand_card_photo2 TEXT,
       card_photo_status INTEGER,
       card_photo_id1 TEXT,
       card_photo_id2 TEXT,
       hand_card_photo_id1 TEXT,
       hand_card_photo_id2 TEXT,
       level INTEGER,
       bank_id TEXT
    )

    create table t_admin (
        id  integer primary key autoincrement,
       account TEXT,
       email TEXT,
       phone TEXT,
       pwd TEXT,
       nick_name TEXT,
       real_name TEXT,
       sex INTEGER,
       birthday INTEGER,
       head_pic TEXT,
       head_pic_id INTEGER,
       status INTEGER,
       register_date INTEGER,
       login_date INTEGER,
       register_ip INTEGER,
       login_ip INTEGER,
       description TEXT,
       fail_count INTEGER,
       parent_id INTEGER,
       role_id INTEGER
    )
