-- FUNCTION: core.get_users(character varying, character varying, integer, integer)

-- DROP FUNCTION IF EXISTS core.get_users(character varying, character varying, integer, integer);

CREATE OR REPLACE FUNCTION core.get_users(
	p_name character varying,
	p_login character varying,
	p_offset integer,
	p_limit integer)
    RETURNS SETOF core."user" 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	SELECT u.* 
	FROM core.user u
	WHERE LOWER(u.name) LIKE LOWER(p_name)
	AND LOWER(u.login) LIKE LOWER(p_login)
	ORDER BY u.name
	LIMIT p_limit
	OFFSET p_offset;
$BODY$;

ALTER FUNCTION core.get_users(character varying, character varying, integer, integer)
    OWNER TO postgres;
