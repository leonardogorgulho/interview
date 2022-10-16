-- FUNCTION: core.get_users_from_group(integer)

-- DROP FUNCTION IF EXISTS core.get_users_from_group(integer);

CREATE OR REPLACE FUNCTION core.get_users_from_group(
	p_group_id integer)
    RETURNS SETOF core."user" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	SELECT u.user_id, u.name, u.login, null as password, u.role 
	FROM core.user_group ug
	INNER JOIN core.user u ON ug.user_id = u.user_id
	WHERE ug.group_id = p_group_id;
$BODY$;

ALTER FUNCTION core.get_users_from_group(integer)
    OWNER TO postgres;
