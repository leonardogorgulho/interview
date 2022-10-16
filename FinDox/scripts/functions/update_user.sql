-- FUNCTION: core.update_user(integer, core.user_entry)

-- DROP FUNCTION IF EXISTS core.update_user(integer, core.user_entry);

CREATE OR REPLACE FUNCTION core.update_user(
	p_user_id integer,
	p_user core.user_entry)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	WITH updated AS	(
		UPDATE core.user
		SET login = p_user.login,
			name = p_user.name,
			password = p_user.password,
			role = p_user.role
		WHERE user_id = p_user_id
		RETURNING 1
	)
	SELECT COUNT(*) FROM updated;
$BODY$;

ALTER FUNCTION core.update_user(integer, core.user_entry)
    OWNER TO postgres;
