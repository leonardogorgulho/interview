-- FUNCTION: core.add_user(core.user_entry)

-- DROP FUNCTION IF EXISTS core.add_user(core.user_entry);

CREATE OR REPLACE FUNCTION core.add_user(
	p_user core.user_entry)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    INSERT INTO core.user(name, login, password, role)
    VALUES(p_user.name, p_user.login, p_user.password, p_user.role);
	SELECT LASTVAL();
$BODY$;

ALTER FUNCTION core.add_user(core.user_entry)
    OWNER TO postgres;
