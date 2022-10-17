-- FUNCTION: core.delete_group(integer)

-- DROP FUNCTION IF EXISTS core.delete_group(integer);

CREATE OR REPLACE FUNCTION core.delete_group(
	p_group_id integer)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DELETE FROM core.user_group WHERE group_id = p_group_id;
	DELETE FROM core.document_permission WHERE group_id = p_group_id;
	WITH deleted AS (
		DELETE FROM core.group WHERE group_id = p_group_id RETURNING 1
	)
	SELECT COUNT(*) FROM deleted;
$BODY$;

ALTER FUNCTION core.delete_group(integer)
    OWNER TO postgres;
