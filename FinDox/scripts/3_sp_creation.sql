/*
CREATE OR REPLACE FUNCTION core.login(p_login character varying, p_password character varying)
    RETURNS SETOF core.user
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	SELECT u.id, u.name, u.login, null as password 
	FROM core.user u
	WHERE u.login = p_login AND u.password = p_password;
$BODY$;

ALTER FUNCTION core.login(character varying, character varying)
    OWNER TO postgres;

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


CREATE OR REPLACE FUNCTION core.add_user_group(
	p_groupid integer,
	p_userid integer)
    RETURNS boolean
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
	p_link_exists int := 0;
BEGIN
	SELECT count(*) INTO p_link_exists FROM core.user_group WHERE user_id = p_userid and group_id = p_groupid;

	if p_link_exists = 0 then
		INSERT INTO core.user_group(group_id, user_id)
		VALUES(p_groupid, p_userid);
	end if;
	
	return p_link_exists = 0;
END;
$BODY$;

ALTER FUNCTION core.add_user_group(integer, integer)
    OWNER TO postgres;


CREATE OR REPLACE FUNCTION core.remove_user_group(
	p_groupid integer,
	p_userid integer)
    RETURNS boolean
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
	p_affected int := 0;
BEGIN
	delete from core.user_group where user_id = p_userid and group_id = p_groupid;
	get diagnostics p_affected = row_count;
	return p_affected > 0;
END;
$BODY$;

ALTER FUNCTION core.remove_user_group(integer, integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION core.get_users_from_group(p_groupid integer)
    RETURNS SETOF core.user
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	SELECT u.id, u.name, u.login, null as password 
	FROM core.user_group ug
	INNER JOIN core.user u ON ug.user_id = u.id
	WHERE ug.group_id = p_groupid;
$BODY$;

ALTER FUNCTION core.get_users_from_group(integer)
    OWNER TO postgres;
*/