-- FUNCTION: core.delete_user(integer)

-- DROP FUNCTION IF EXISTS core.delete_user(integer);

CREATE OR REPLACE FUNCTION core.delete_user(
	p_user_id integer)
    RETURNS void
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DELETE FROM core.user WHERE user_id = p_user_id;
$BODY$;

ALTER FUNCTION core.delete_user(integer)
    OWNER TO postgres;
