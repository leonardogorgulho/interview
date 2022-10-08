CREATE OR REPLACE FUNCTION core.add_user(
	p_user core.user_entry)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    INSERT INTO core.user(name, login, password)
    VALUES(p_user.name, p_user.login, p_user.password);
	SELECT LASTVAL();
$BODY$;

ALTER FUNCTION core.add_user(core.user_entry)
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION core.get_user(
	p_id integer)
    RETURNS SETOF core."user" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
    SELECT 
		id, 
		name,
		login,
		null as password 
	FROM core.user 
	WHERE id = p_id;
$BODY$;

ALTER FUNCTION core.get_user(integer)
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION core.delete_user(
	p_id integer)
    RETURNS void
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DELETE FROM core.user WHERE id = p_id;
$BODY$;

ALTER FUNCTION core.delete_user(integer)
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION core.update_user(
	p_id integer,
	p_user core.user_entry)
    RETURNS void
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    UPDATE core.user
	SET login = p_user.login,
		name = p_user.name,
		password = p_user.password
	WHERE id = p_id;
$BODY$;

ALTER FUNCTION core.update_user(integer, core.user_entry)
    OWNER TO postgres;



-----------------------------------------------------------------



CREATE OR REPLACE FUNCTION core.add_group(
	p_name character varying)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    INSERT INTO core.group(name)
    VALUES(p_name);
	SELECT LASTVAL();
$BODY$;

ALTER FUNCTION core.add_group(character varying)
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION core.delete_group(
	p_id integer)
    RETURNS void
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DELETE FROM core.group WHERE id = p_id;
$BODY$;

ALTER FUNCTION core.delete_group(integer)
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION core.get_group(
	p_id integer)
    RETURNS SETOF core."group" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
    SELECT 
		id, 
		name
	FROM core.group 
	WHERE id = p_id;
$BODY$;

ALTER FUNCTION core.get_group(integer)
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION core.update_group(
	p_id integer,
	p_name character varying)
    RETURNS void
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    UPDATE core.group
	SET name = p_name
	WHERE id = p_id;
$BODY$;

ALTER FUNCTION core.update_group(integer, character varying)
    OWNER TO postgres;