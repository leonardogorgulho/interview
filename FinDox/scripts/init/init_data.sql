TRUNCATE TABLE core.document_permission CASCADE;
TRUNCATE TABLE core.user_group CASCADE;
TRUNCATE TABLE core."document" CASCADE;
TRUNCATE TABLE core.file CASCADE;
TRUNCATE TABLE core.user CASCADE;
TRUNCATE TABLE core.group CASCADE;

ALTER SEQUENCE core.document_id_seq RESTART;
ALTER SEQUENCE core.document_permission_id_seq RESTART;
ALTER SEQUENCE core.file_id_seq RESTART;
ALTER SEQUENCE core.group_id_seq RESTART;
ALTER SEQUENCE core.user_id_seq RESTART;

insert into core.user (login, "name", "password", "role") values ('admin', 'admin', '202CB962AC59075B964B07152D234B70', 'A');
insert into core.user (login, "name", "password", "role") values ('manager', 'manager', '202CB962AC59075B964B07152D234B70', 'M');
insert into core.user (login, "name", "password", "role") values ('regular', 'regular', '202CB962AC59075B964B07152D234B70', 'R');