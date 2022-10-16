-- FUNCTION: core.get_document_with_file(integer)

-- DROP FUNCTION IF EXISTS core.get_document_with_file(integer);

CREATE OR REPLACE FUNCTION core.get_document_with_file(
	p_document_id integer)
    RETURNS TABLE(document_id integer, file_id integer, posted_date date, name character varying, description character varying, category character varying, content_type character varying, size bigint, content bytea) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
    SELECT 
		d.document_id, 
		d.file_id,
		d.posted_date,
		d.name,
		d.description,
		d.category,
		d.content_type,
		d.size,
		f.content
	FROM core.document d
	INNER JOIN core.file f on d.file_id = f.file_id
	WHERE d.document_id = p_document_id;
$BODY$;

ALTER FUNCTION core.get_document_with_file(integer)
    OWNER TO postgres;
