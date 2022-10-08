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
	
