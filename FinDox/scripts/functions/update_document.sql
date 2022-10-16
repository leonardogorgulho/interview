-- FUNCTION: core.update_document(integer, core.document_entry)

-- DROP FUNCTION IF EXISTS core.update_document(integer, core.document_entry);

CREATE OR REPLACE FUNCTION core.update_document(
	p_document_id integer,
	p_document core.document_entry)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	WITH updated AS (
		UPDATE core.document
		SET file_id = p_document.file_id,
			posted_date = p_document.posted_date,
			name = p_document.name,
			description = p_document.description,
			category = p_document.category,
			content_type = p_document.content_type,
			size = p_document.size
		WHERE document_id = p_document_id
		RETURNING 1
	)
	SELECT COUNT(*) FROM updated;
$BODY$;

ALTER FUNCTION core.update_document(integer, core.document_entry)
    OWNER TO postgres;
