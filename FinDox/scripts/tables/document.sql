-- Table: core.document

-- DROP TABLE IF EXISTS core.document;

CREATE TABLE IF NOT EXISTS core.document
(
    document_id integer NOT NULL DEFAULT nextval('core.document_id_seq'::regclass),
    file_id integer NOT NULL,
    posted_date date,
    name character varying(100) COLLATE pg_catalog."default" NOT NULL,
    description character varying(500) COLLATE pg_catalog."default",
    category character varying(100) COLLATE pg_catalog."default",
    content_type character varying(100) COLLATE pg_catalog."default" NOT NULL,
    size bigint,
    CONSTRAINT document_pkey PRIMARY KEY (document_id),
    CONSTRAINT fk_document_file FOREIGN KEY (file_id)
        REFERENCES core.file (file_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS core.document
    OWNER to postgres;