-- Type: user_entry

-- DROP TYPE IF EXISTS core.user_entry;

CREATE TYPE core.user_entry AS
(
	name character varying(150),
	login character varying(30),
	password character varying(32),
	role character(1)
);

ALTER TYPE core.user_entry
    OWNER TO postgres;
