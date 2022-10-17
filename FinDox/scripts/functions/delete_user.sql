-- FUNCTION: core.delete_user(integer)

-- DROP FUNCTION IF EXISTS core.delete_user(integer);

CREATE OR REPLACE FUNCTION core.delete_user(
	p_user_id integer)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DELETE FROM core.user_group WHERE user_id = p_user_id;
	DELETE FROM core.document_permission WHERE user_id = p_user_id;
	WITH deleted AS (
		DELETE FROM core.user WHERE user_id = p_user_id RETURNING 1
	)
	SELECT COUNT(*) FROM deleted;
$BODY$;

ALTER FUNCTION core.delete_user(integer)
    OWNER TO postgres;
