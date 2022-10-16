-- Table: core.user

-- DROP TABLE IF EXISTS core."user";

CREATE TABLE IF NOT EXISTS core."user"
(
    user_id integer NOT NULL DEFAULT nextval('core.user_id_seq'::regclass),
    name character varying(150) COLLATE pg_catalog."default" NOT NULL,
    login character varying(30) COLLATE pg_catalog."default" NOT NULL,
    password character varying(32) COLLATE pg_catalog."default" NOT NULL,
    role character(1) COLLATE pg_catalog."default",
    CONSTRAINT pk_user PRIMARY KEY (user_id),
    CONSTRAINT uq_user_login UNIQUE (login)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS core."user"
    OWNER to postgres;
-- Index: ix_user_name

-- DROP INDEX IF EXISTS core.ix_user_name;

CREATE INDEX IF NOT EXISTS ix_user_name
    ON core."user" USING btree
    (name COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;