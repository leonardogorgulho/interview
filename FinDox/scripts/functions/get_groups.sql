-- FUNCTION: core.get_groups(character varying, integer, integer)

-- DROP FUNCTION IF EXISTS core.get_groups(character varying, integer, integer);

CREATE OR REPLACE FUNCTION core.get_groups(
	p_name character varying,
	p_offset integer,
	p_limit integer)
    RETURNS SETOF core."group" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	SELECT g.* 
	FROM core.group g
	WHERE LOWER(g.name) LIKE LOWER(p_name)
	ORDER BY g.name
	LIMIT p_limit
	OFFSET p_offset;
$BODY$;

ALTER FUNCTION core.get_groups(character varying, integer, integer)
    OWNER TO postgres;
