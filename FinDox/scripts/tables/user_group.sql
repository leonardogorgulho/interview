-- Table: core.user_group

-- DROP TABLE IF EXISTS core.user_group;

CREATE TABLE IF NOT EXISTS core.user_group
(
    user_id integer NOT NULL,
    group_id integer NOT NULL,
    CONSTRAINT pk_user_group PRIMARY KEY (user_id, group_id),
    CONSTRAINT fk_user_group_group FOREIGN KEY (group_id)
        REFERENCES core."group" (group_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_user_group_user FOREIGN KEY (user_id)
        REFERENCES core."user" (user_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS core.user_group
    OWNER to postgres;