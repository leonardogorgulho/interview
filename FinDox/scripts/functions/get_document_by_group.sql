-- FUNCTION: core.get_document_by_group(integer)

-- DROP FUNCTION IF EXISTS core.get_document_by_group(integer);

CREATE OR REPLACE FUNCTION core.get_document_by_group(
	p_group_id integer)
    RETURNS TABLE(group_id integer, group_name character varying, document_id integer, file_id integer, post_date date, name character varying, description character varying, category character varying, content_type character varying, size bigint) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	select
		g.group_id, g.name as group_name,
		d.*
	FROM core.document d
	INNER JOIN core.document_permission dp ON dp.document_id = d.document_id
	INNER JOIN core.group g ON dp.group_id = g.group_id
	WHERE dp.group_id = p_group_id;
$BODY$;

ALTER FUNCTION core.get_document_by_group(integer)
    OWNER TO postgres;
