-- Type: document_entry

-- DROP TYPE IF EXISTS core.document_entry;

CREATE TYPE core.document_entry AS
(
	file_id integer,
	posted_date date,
	name character varying(100),
	description character varying(500),
	category character varying(100),
	content_type character varying(100),
	size bigint
);

ALTER TYPE core.document_entry
    OWNER TO postgres;
