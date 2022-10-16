-- FUNCTION: core.add_document_permission(integer, integer[], integer[])

-- DROP FUNCTION IF EXISTS core.add_document_permission(integer, integer[], integer[]);

CREATE OR REPLACE FUNCTION core.add_document_permission(
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
	FOREACH p_group IN ARRAY p_group_ids                             
    LOOP
		IF NOT EXISTS (SELECT 1 FROM core.document_permission WHERE document_id = p_document_id AND group_id = p_group) THEN
			INSERT INTO core.document_permission (document_id, group_id)
			VALUES (p_document_id, p_group);
		END IF;
    END LOOP;
	
	FOREACH p_user IN ARRAY p_user_ids                             
    LOOP
		IF NOT EXISTS (SELECT 1 FROM core.document_permission WHERE document_id = p_document_id AND user_id = p_user) THEN
			INSERT INTO core.document_permission (document_id, user_id)
			VALUES (p_document_id, p_user);
		END IF;
    END LOOP;
END
$BODY$;

ALTER FUNCTION core.add_document_permission(integer, integer[], integer[])
    OWNER TO postgres;
