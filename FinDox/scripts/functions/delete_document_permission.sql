-- FUNCTION: core.delete_document_permission(integer, integer[], integer[])

-- DROP FUNCTION IF EXISTS core.delete_document_permission(integer, integer[], integer[]);

CREATE OR REPLACE FUNCTION core.delete_document_permission(
	p_document_id integer,
	p_group_ids integer[],
	p_user_ids integer[])
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
	p_group integer;
	p_user integer;
BEGIN
	IF p_group_ids IS NOT NULL THEN
		FOREACH p_group IN ARRAY p_group_ids
		LOOP
			DELETE FROM core.document_permission WHERE document_id = p_document_id AND group_id = p_group;
		END LOOP;
	END IF;
	
	IF p_user_ids IS NOT NULL THEN
		FOREACH p_user IN ARRAY p_user_ids
		LOOP
			DELETE FROM core.document_permission WHERE document_id = p_document_id AND user_id = p_user;
		END LOOP;
	END IF;
END
$BODY$;

ALTER FUNCTION core.delete_document_permission(integer, integer[], integer[])
    OWNER TO postgres;
