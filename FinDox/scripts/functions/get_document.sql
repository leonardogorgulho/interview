-- FUNCTION: core.get_document(integer)

-- DROP FUNCTION IF EXISTS core.get_document(integer);

CREATE OR REPLACE FUNCTION core.get_document(
	p_document_id integer)
    RETURNS SETOF core.document 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
    SELECT 
		document_id, 
		file_id,
		posted_date,
		name,
		description,
		category,
		content_type,
		size
	FROM core.document 
	WHERE document_id = p_document_id;
$BODY$;

ALTER FUNCTION core.get_document(integer)
    OWNER TO postgres;
