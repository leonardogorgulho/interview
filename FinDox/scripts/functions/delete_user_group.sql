-- FUNCTION: core.delete_user_group(integer, integer)

-- DROP FUNCTION IF EXISTS core.delete_user_group(integer, integer);

CREATE OR REPLACE FUNCTION core.delete_user_group(
	p_group_id integer,
	p_user_id integer)
    RETURNS boolean
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
	p_affected int := 0;
BEGIN
	delete from core.user_group where user_id = p_user_id and group_id = p_group_id;
	get diagnostics p_affected = row_count;
	return p_affected > 0;
END;
$BODY$;

ALTER FUNCTION core.delete_user_group(integer, integer)
    OWNER TO postgres;
