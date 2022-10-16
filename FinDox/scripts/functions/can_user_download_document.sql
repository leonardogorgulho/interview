-- FUNCTION: core.can_user_download_document(integer, integer)

-- DROP FUNCTION IF EXISTS core.can_user_download_document(integer, integer);

CREATE OR REPLACE FUNCTION core.can_user_download_document(
	p_user_id integer,
	p_document_id integer)
    RETURNS boolean
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
	p_allowed boolean := false;
BEGIN
	SELECT count(*) > 0 into p_allowed
	FROM core.document d
	INNER JOIN core.document_permission dp ON dp.document_id = d.document_id
	WHERE d.document_id = p_document_id 
	AND dp.user_id = p_user_id;
	
	IF p_allowed = false THEN
		SELECT count(*) > 0 into p_allowed
		FROM core.document d
		INNER JOIN core.document_permission dp ON dp.document_id = d.document_id
		INNER JOIN core.user_group ug ON ug.group_id = dp.group_id
		WHERE d.document_id = p_document_id
		AND ug.user_id = p_user_id;
	END IF;
	
	RETURN p_allowed;
END;
$BODY$;

ALTER FUNCTION core.can_user_download_document(integer, integer)
    OWNER TO postgres;
