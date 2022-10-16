-- FUNCTION: core.add_group(character varying)

-- DROP FUNCTION IF EXISTS core.add_group(character varying);

CREATE OR REPLACE FUNCTION core.add_group(
	p_name character varying)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    INSERT INTO core.group(name)
    VALUES(p_name);
	SELECT LASTVAL();
$BODY$;

ALTER FUNCTION core.add_group(character varying)
    OWNER TO postgres;
