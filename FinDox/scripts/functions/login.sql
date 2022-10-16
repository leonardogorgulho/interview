-- FUNCTION: core.login(character varying, character varying)

-- DROP FUNCTION IF EXISTS core.login(character varying, character varying);

CREATE OR REPLACE FUNCTION core.login(
	p_login character varying,
	p_password character varying)
    RETURNS SETOF core."user" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	SELECT u.user_id, u.name, u.login, null as password, u.role 
	FROM core.user u
	WHERE u.login = p_login AND u.password = p_password;
$BODY$;

ALTER FUNCTION core.login(character varying, character varying)
    OWNER TO postgres;
