-- FUNCTION: core.add_file(bytea)

-- DROP FUNCTION IF EXISTS core.add_file(bytea);

CREATE OR REPLACE FUNCTION core.add_file(
	p_file bytea)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    INSERT INTO core.file(content)
    VALUES (p_file);
	SELECT LASTVAL();
$BODY$;

ALTER FUNCTION core.add_file(bytea)
    OWNER TO postgres;
