-- FUNCTION: core.get_user(integer)

-- DROP FUNCTION IF EXISTS core.get_user(integer);

CREATE OR REPLACE FUNCTION core.get_user(
	p_user_id integer)
    RETURNS SETOF core."user" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
    SELECT 
		user_id, 
		name,
		login,
		null as password,
		role
	FROM core.user 
	WHERE user_id = p_user_id;
$BODY$;

ALTER FUNCTION core.get_user(integer)
    OWNER TO postgres;
