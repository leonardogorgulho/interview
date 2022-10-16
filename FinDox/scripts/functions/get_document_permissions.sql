-- FUNCTION: core.get_document_permissions(integer)

-- DROP FUNCTION IF EXISTS core.get_document_permissions(integer);

CREATE OR REPLACE FUNCTION core.get_document_permissions(
	p_document_id integer)
    RETURNS TABLE(document_id integer, file_id integer, post_date date, name character varying, description character varying, category character varying, content_type character varying, size bigint, group_id integer, group_name character varying, user_id integer, user_name character varying, login character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	select
		d.*, 
		g.group_id, g.name as group_name,
		u.user_id, u.name as user_name, u.login
	FROM core.document d
	INNER JOIN core.document_permission dp ON dp.document_id = d.document_id
	LEFT JOIN core.group g ON dp.group_id = g.group_id
	LEFT JOIN core.user u ON dp.user_id = u.user_id
	WHERE d.document_id = p_document_id;
$BODY$;

ALTER FUNCTION core.get_document_permissions(integer)
    OWNER TO postgres;
