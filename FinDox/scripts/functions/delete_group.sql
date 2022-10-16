-- FUNCTION: core.delete_group(integer)

-- DROP FUNCTION IF EXISTS core.delete_group(integer);

CREATE OR REPLACE FUNCTION core.delete_group(
	p_group_id integer)
    RETURNS void
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DELETE FROM core.group WHERE group_id = p_group_id;
$BODY$;

ALTER FUNCTION core.delete_group(integer)
    OWNER TO postgres;
