-- FUNCTION: core.get_file(integer)

-- DROP FUNCTION IF EXISTS core.get_file(integer);

CREATE OR REPLACE FUNCTION core.get_file(
	p_file_id integer)
    RETURNS SETOF core.file 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
    SELECT * 
	FROM core.file 
	WHERE file_id = p_file_id;
$BODY$;

ALTER FUNCTION core.get_file(integer)
    OWNER TO postgres;
