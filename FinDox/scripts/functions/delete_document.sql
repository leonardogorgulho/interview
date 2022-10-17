-- FUNCTION: core.delete_document(integer)

-- DROP FUNCTION IF EXISTS core.delete_document(integer);

CREATE OR REPLACE FUNCTION core.delete_document(
	p_document_id integer)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DELETE FROM core.document_permission WHERE document_id = p_document_id;
	
	WITH deleted AS (
		DELETE 
		FROM core.file f 
		USING core.document d 
		WHERE d.file_id = f.file_id
		AND d.document_id = p_document_id
		RETURNING 1
	)
	SELECT COUNT(*) FROM deleted;
$BODY$;

ALTER FUNCTION core.delete_document(integer)
    OWNER TO postgres;
