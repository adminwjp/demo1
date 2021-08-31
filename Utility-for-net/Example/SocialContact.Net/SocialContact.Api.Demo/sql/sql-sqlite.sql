
    PRAGMA foreign_keys = OFF

    drop table if exists t_article

    drop table if exists t_catalog

    drop table if exists t_comment

    drop table if exists t_icon

    drop table if exists t_user_relation

    drop table if exists work_info

    PRAGMA foreign_keys = ON

    create table t_article (
        id INTEGER not null,
       user_id INTEGER,
       username TEXT,
       context TEXT,
       images TEXT,
       likes INTEGER,
       comment_num INTEGER,
       catalog_id INTEGER,
       create_date INTEGER,
       update_date INTEGER,
       primary key (id),
       constraint fk_catalog_id foreign key (catalog_id) references t_catalog
    )

    create table t_catalog (
        id INTEGER not null,
       category TEXT,
       accept TEXT,
       flag INTEGER,
       description TEXT,
       parent_id INTEGER,
       admin_id INTEGER,
       create_date INTEGER,
       update_date INTEGER,
       primary key (id),
       constraint fk_parent_id foreign key (parent_id) references t_catalog
    )

    create table t_comment (
        id INTEGER not null,
       user_id INTEGER,
       username TEXT,
       context TEXT,
       images TEXT,
       article_id INTEGER,
       create_date INTEGER,
       update_date INTEGER,
       primary key (id),
       constraint fk_article_id foreign key (article_id) references t_article
    )

    create table t_icon (
        id INTEGER not null,
       name TEXT,
       style TEXT,
       description TEXT,
       create_date INTEGER,
       update_date INTEGER,
       primary key (id)
    )

    create table t_user_relation (
        id INTEGER not null,
       user_id INTEGER,
       username TEXT,
       catalog_id INTEGER,
       create_date INTEGER,
       update_date INTEGER,
       primary key (id),
       constraint fk_catalog_id foreign key (catalog_id) references t_catalog
    )

    create table work_info (
        id INTEGER not null,
       company_name TEXT,
       job TEXT,
       description TEXT,
       start_date INTEGER,
       end_date INTEGER,
       user_id INTEGER,
       username TEXT,
       catalog_name TEXT,
       catalog_id INTEGER,
       create_date INTEGER,
       update_date INTEGER,
       primary key (id),
       constraint fk_catalog_id foreign key (catalog_id) references t_catalog
    )
