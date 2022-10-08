CREATE OR REPLACE FUNCTION core.add_user(p_user core.user_entry)
RETURNS integer 
LANGUAGE SQL
AS $BODY$
    INSERT INTO core.user(name, login, password)
    VALUES(p_user.name, p_user.login, p_user.password);
	SELECT LASTVAL();
$BODY$;