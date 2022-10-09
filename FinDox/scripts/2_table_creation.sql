CREATE SCHEMA IF NOT EXISTS core
    AUTHORIZATION postgres;

CREATE TABLE IF NOT EXISTS core.user
(
    id serial NOT NULL,
    name character varying(150) NOT NULL,
    login character varying(30) NOT NULL,
    password character varying(32) NOT NULL,
    CONSTRAINT pk_user PRIMARY KEY (id), 
	CONSTRAINT uq_user_login UNIQUE (login)
);

ALTER TABLE IF EXISTS core.user
    OWNER to postgres;
	
-----------------------------

CREATE TABLE IF NOT EXISTS core.group
(
    id serial NOT NULL,
    name character varying(50) NOT NULL,
    CONSTRAINT pk_group PRIMARY KEY (id), 
	CONSTRAINT uq_group_name UNIQUE (name)
);

ALTER TABLE IF EXISTS core.group
    OWNER to postgres;

-----------------------------

CREATE TABLE IF NOT EXISTS core.user_group
(
    user_id integer NOT NULL,
	group_id integer NOT NULL,
    CONSTRAINT pk_user_group PRIMARY KEY (user_id, group_id),
	CONSTRAINT fk_user_group_user FOREIGN KEY (user_id)
		REFERENCES core.user (id),
	CONSTRAINT fk_user_group_group FOREIGN KEY (group_id)
		REFERENCES core.group (id),
);

ALTER TABLE IF EXISTS core.user_group
    OWNER to postgres;

