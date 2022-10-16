-- FUNCTION: core.add_document(core.document_entry)

-- DROP FUNCTION IF EXISTS core.add_document(core.document_entry);

CREATE OR REPLACE FUNCTION core.add_document(
	p_document core.document_entry)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    INSERT INTO core.document(file_id, posted_date, name, description, category, content_type, size)
    VALUES (p_document.file_id, p_document.posted_date, p_document.name, p_document.description, p_document.category, p_document.content_type, p_document.size);
	SELECT LASTVAL();
$BODY$;

ALTER FUNCTION core.add_document(core.document_entry)
    OWNER TO postgres;
