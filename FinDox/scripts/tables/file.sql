-- Table: core.file

-- DROP TABLE IF EXISTS core.file;

CREATE TABLE IF NOT EXISTS core.file
(
    file_id integer NOT NULL DEFAULT nextval('core.file_id_seq'::regclass),
    content bytea NOT NULL,
    CONSTRAINT file_pkey PRIMARY KEY (file_id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS core.file
    OWNER to postgres;