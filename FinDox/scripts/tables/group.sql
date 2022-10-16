-- Table: core.group

-- DROP TABLE IF EXISTS core."group";

CREATE TABLE IF NOT EXISTS core."group"
(
    group_id integer NOT NULL DEFAULT nextval('core.group_id_seq'::regclass),
    name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_group PRIMARY KEY (group_id),
    CONSTRAINT uq_group_name UNIQUE (name)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS core."group"
    OWNER to postgres;