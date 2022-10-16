-- FUNCTION: core.get_document_by_user(integer)

-- DROP FUNCTION IF EXISTS core.get_document_by_user(integer);

CREATE OR REPLACE FUNCTION core.get_document_by_user(
	p_user_id integer)
    RETURNS TABLE(user_id integer, user_name character varying, login character varying, document_id integer, file_id integer, post_date date, name character varying, description character varying, category character varying, content_type character varying, size bigint) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	SELECT DISTINCT
		u.user_id, u.name as user_name, u.login,
		d.*
	FROM core.document d
	INNER JOIN core.document_permission dp ON dp.document_id = d.document_id
	LEFT JOIN core.user_group ug ON ug.group_id = dp.group_id
	LEFT JOIN core.user u ON dp.user_id = u.user_id OR ug.user_id = u.user_id	
	WHERE dp.user_id = p_user_id
	OR ug.user_id = p_user_id;
$BODY$;

ALTER FUNCTION core.get_document_by_user(integer)
    OWNER TO postgres;
