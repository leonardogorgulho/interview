-- FUNCTION: core.update_group(integer, character varying)

-- DROP FUNCTION IF EXISTS core.update_group(integer, character varying);

CREATE OR REPLACE FUNCTION core.update_group(
	p_group_id integer,
	p_name character varying)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	WITH updated AS (
		UPDATE core.group
		SET name = p_name
		WHERE group_id = p_group_id
		RETURNING 1
	)
	SELECT COUNT(*) FROM updated;
$BODY$;

ALTER FUNCTION core.update_group(integer, character varying)
    OWNER TO postgres;
