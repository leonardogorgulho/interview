-- Table: core.document_permission

-- DROP TABLE IF EXISTS core.document_permission;

CREATE TABLE IF NOT EXISTS core.document_permission
(
    document_permission_id integer NOT NULL DEFAULT nextval('core.document_permission_id_seq'::regclass),
    document_id integer NOT NULL,
    group_id integer,
    user_id integer,
    CONSTRAINT pk_document_permission PRIMARY KEY (document_permission_id),
    CONSTRAINT uq_permission UNIQUE (document_id, group_id, user_id),
    CONSTRAINT fk_document_permission_document FOREIGN KEY (document_id)
        REFERENCES core.document (document_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_document_permission_group FOREIGN KEY (group_id)
        REFERENCES core."group" (group_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_document_permission_user FOREIGN KEY (user_id)
        REFERENCES core."user" (user_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS core.document_permission
    OWNER to postgres;