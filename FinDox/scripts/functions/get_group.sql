-- FUNCTION: core.get_group(integer)

-- DROP FUNCTION IF EXISTS core.get_group(integer);

CREATE OR REPLACE FUNCTION core.get_group(
	p_group_id integer)
    RETURNS SETOF core."group" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
    SELECT 
		group_id, 
		name
	FROM core.group 
	WHERE group_id = p_group_id;
$BODY$;

ALTER FUNCTION core.get_group(integer)
    OWNER TO postgres;
