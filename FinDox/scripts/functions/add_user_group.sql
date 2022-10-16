-- FUNCTION: core.add_user_group(integer, integer)

-- DROP FUNCTION IF EXISTS core.add_user_group(integer, integer);

CREATE OR REPLACE FUNCTION core.add_user_group(
	p_group_id integer,
	p_user_id integer)
    RETURNS boolean
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
	p_link_exists int := 0;
BEGIN
	SELECT count(*) INTO p_link_exists FROM core.user_group WHERE user_id = p_user_id and group_id = p_group_id;

	if p_link_exists = 0 then
		INSERT INTO core.user_group(group_id, user_id)
		VALUES(p_group_id, p_user_id);
	end if;
	
	return p_link_exists = 0;
END;
$BODY$;

ALTER FUNCTION core.add_user_group(integer, integer)
    OWNER TO postgres;
